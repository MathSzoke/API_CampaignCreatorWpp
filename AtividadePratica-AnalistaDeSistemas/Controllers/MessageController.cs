using Data.Handlers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using RestSharp;
using Services;

// Arquivo: MessageController.cs
namespace Controllers;

/// <summary>
/// Controlador responsável por operações relacionadas a mensagens.
/// </summary>
/// <param name="messageSettings">Configuração para realizar requisições REST.</param>
public class MessageController(IMessageSettings messageSettings) : Controller
{
    /// <summary>
    /// Configuração para realizar requisições REST.
    /// </summary>
    private readonly IMessageSettings _messageSettings = messageSettings;

    /// <summary>
    /// Envia uma mensagem para o número de telefone especificado.
    /// </summary>
    /// <param name="sm">Objeto contendo informações da mensagem a ser enviada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessage sm) => await this._messageSettings.SendMessage(sm);

    /// <summary>
    /// Envia uma mensagem configurada com base no ID da mensagem e/ou número de telefone a ser enviado.
    /// <para>
    /// Preencha o campo "PhoneNumber" apenas se não houver um número setado ao configurar a mensagem.
    /// </para>
    /// </summary>
    /// <param name="MessageID">ID da mensagem a ser enviada. ATENÇÃO: o ID se encontra armazenado no banco de dados, na tabela "MessageSettigns".</param>
    /// <param name="PhoneNumber">Insira o número de telefone para qual a mensagem será enviada. ATENÇÃO: este número deverá ser incluso apenas se a mensagem cadastrada não tiver um número definido para ser enviado.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("SendMessageConfigurated")]
    public async Task<IActionResult> SendMessageConfigurated(int MessageID, string? PhoneNumber) => await this._messageSettings.SendMessageConfigurated(MessageID, PhoneNumber);

    /// <summary>
    /// Cria uma nova mensagem ou enquete.
    /// <para>
    /// É possivel remover a propriedade "phone" do json e assim, configurar uma mensagem sem um destinatário especifico. <br/>
    /// Sendo assim, a mensagem em questão estar disponivel para N números.
    /// </para>
    /// </summary>
    /// <param name="md">Objeto contendo informações da mensagem ou enquete a ser criada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("NewMessage")]
    public async Task<IActionResult> NewMessage([FromBody] MessageData md) => await this._messageSettings.NewMessage(md);

    /// <summary>
    /// Atualiza uma mensagem ou enquete existente com base no ID da mensagem.
    /// </summary>
    /// <param name="md">Objeto contendo informações atualizadas da mensagem ou enquete.</param>
    /// <param name="messageID">ID da mensagem a ser atualizada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPut("UpdateMessage")]
    public async Task<IActionResult> UpdateMessage([FromBody] MessageData md, int? messageID = null) => await this._messageSettings.UpdateMessage(md, messageID);

    /// <summary>
    /// Obtém a fila de mensagens.
    /// </summary>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpGet("GetQueue")]
    public async Task<IActionResult> GetQueue() => await this._messageSettings.GetQueue();

    /// <summary>
    /// Obtém o histórico de chat para o número de telefone especificado.
    /// </summary>
    /// <param name="phone">Número de telefone para o qual obter o histórico de chat.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpGet("GetChat")]
    public async Task<IActionResult> GetChat(string phone) => await this._messageSettings.GetChat(phone);
}
