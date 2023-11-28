using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Services;

namespace Data.Handlers;

/// <summary>
/// Arquivo MessageSettings.cs
/// </summary>
public class MessageSettings : Controller, IMessageSettings
{
    private readonly IRestSettings _restConfiguration;

    /// <summary>
    /// DBContext utilizado para interação com o banco de dados.
    /// </summary>
    protected readonly WppDbContext _context;

    /// <summary>
    /// Construtor da classe MessageSettings.
    /// </summary>
    public MessageSettings(IRestSettings restSettings, WppDbContext wppDbContext)
    {
        this._restConfiguration = restSettings;
        this._context = wppDbContext;
    }

    /// <summary>
    /// Envia uma mensagem para o número de telefone especificado.
    /// </summary>
    /// <param name="sm">Objeto contendo informações da mensagem a ser enviada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> SendMessage(SendMessage sm)
    {
        sm.phone = WppSettings.CleanPhone(sm.phone);

        var Message = this._context.MessageData.FirstOrDefault(x => x.MessageText.Equals(sm.message));

        dynamic response = await this._restConfiguration.ExecuteContent("send-text", Method.Post, JsonConvert.SerializeObject(sm));

        LogSettings newLog = new(this._context);

        JToken token = JToken.Parse(response.Value.ToString());

        LogSettings.ResponseStatus = response.StatusCode;
        LogSettings.ResponseText = response.Value.ToString();

        await newLog.LogMessage(ObjMessageSent: Message is null ? sm.message : Message.ID, isReceived: false, isRead: false, messageId: (string)token.SelectToken("id")!);

        return response;
    }

    /// <summary>
    /// Envia uma mensagem configurada com base no ID da mensagem e/ou número de telefone a ser enviado.
    /// <para>
    /// Preencha o campo "PhoneNumber" apenas se não houver um número setado ao configurar a mensagem.
    /// </para>
    /// </summary>
    /// <param name="MessageID">ID da mensagem a ser enviada. ATENÇÃO: o ID se encontra armazenado no banco de dados, na tabela "MessageSettigns".</param>
    /// <param name="PhoneNumber">Insira o número de telefone para qual a mensagem será enviada. ATENÇÃO: este número deverá ser incluso apenas se a mensagem cadastrada não tiver um número definido para ser enviado.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> SendMessageConfigurated(int MessageID, string? PhoneNumber)
    {
        bool isActive = await this._context.MessageData
            .Where(x => x.ID == MessageID)
            .Select(x => x.IsActive)
            .FirstOrDefaultAsync();

        if (!isActive) return this.Conflict("Por favor, atualize sua mensagem para ativo!");

        var md = await this._context.MessageData.FirstOrDefaultAsync(x => x.ID.Equals(MessageID));
        var sp = await this._context.SendPoll.FirstOrDefaultAsync(x => x.MessageID.Equals(MessageID));

        if (md is null || sp is null) return this.NotFound("Não foi possível encontrar os dados das mensagens salvas.");

        var pollContent = new
        {
            phone = sp.phone is null ? WppSettings.CleanPhone(PhoneNumber) : sp.phone,
            message = md.MessageText,
            pollMaxOptions = sp.pollMaxOptions is not 0 ? sp.pollMaxOptions : null,
            poll = sp.PollOptions is not null ? this._context.PollOption.Where(po => po.PollID.Equals(sp.PollID)).Select(po => new { po.name }).ToList() : null,
        };

        if (!string.IsNullOrEmpty(PhoneNumber) && pollContent.poll is null)
        {
            var content = new SendMessage
            {
                phone = WppSettings.CleanPhone(PhoneNumber),
                message = pollContent.message
            };

            return await this.SendMessage(content);
        }

        dynamic response = await this._restConfiguration.ExecuteContent("send-poll", Method.Post, JsonConvert.SerializeObject(pollContent));

        LogSettings newLog = new(this._context);

        JToken token = JToken.Parse(response.Value.ToString());

        LogSettings.ResponseStatus = response.StatusCode;
        LogSettings.ResponseText = response.Value.ToString();

        await newLog.LogMessage(ObjMessageSent: MessageID, isReceived: false, isRead: false, messageId:(string)token.SelectToken("id")!);

        return response;
    }

    /// <summary>
    /// Cria uma nova mensagem ou enquete.
    /// <para>
    /// É possivel remover a propriedade "phone" do json e assim, configurar uma mensagem sem um destinatário especifico. <br/>
    /// Sendo assim, a mensagem em questão estar disponivel para N números.
    /// </para>
    /// </summary>
    /// <param name="ms">Objeto contendo informações da mensagem ou enquete a ser criada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> NewMessage(MessageData ms)
    {
        if (!ms.IsPollMessage)
        {
            var msNew = new MessageData
            {
                MessageText = ms.MessageText,
                IsActive = ms.IsActive,
                IsPollMessage = ms.IsPollMessage,
            };

            this._context.MessageData.Add(msNew);
            await this._context.SaveChangesAsync();
        }
        else
        {
            if (!string.IsNullOrEmpty(ms.SendPoll!.phone)) ms.SendPoll.phone = WppSettings.CleanPhone(ms.SendPoll.phone);

            this._context.MessageData.Add(ms);
            await this._context.SaveChangesAsync();
        }
        return this.Ok("Mensagem/Enquete criado com sucesso.");
    }

    /// <summary>
    /// Atualiza uma mensagem ou enquete existente com base no ID da mensagem.
    /// </summary>
    /// <param name="ms">Objeto contendo informações atualizadas da mensagem ou enquete.</param>
    /// <param name="messageID">ID da mensagem a ser atualizada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> UpdateMessage(MessageData ms, int? messageID = null)
    {
        if (messageID.HasValue && messageID.Value != 0)
        {
            var existingMessage = this._context.MessageData.FirstOrDefault(x => x.ID == messageID);
            if (existingMessage != null)
            {
                existingMessage = ms;

                await this._context.SaveChangesAsync();

                return this.Ok("Mensagem/Enquete alterada com sucesso.");
            }
            else
            {
                return this.NotFound("Mensagem não encontrada.");
            }
        }
        else
        {
            return this.BadRequest("messageID é inválido.");
        }
    }

    /// <summary>
    /// Obtém a fila de mensagens.
    /// </summary>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> GetQueue() => await this._restConfiguration.ExecuteContent("queue", Method.Get, string.Empty);

    /// <summary>
    /// Obtém o histórico de chat para o número de telefone especificado.
    /// </summary>
    /// <param name="phone">Número de telefone para o qual obter o histórico de chat.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> GetChat(string phone)
    {
        phone = WppSettings.CleanPhone(phone);

        return await this._restConfiguration.ExecuteContent($"chats/{phone}", Method.Get, string.Empty);
    }
}
