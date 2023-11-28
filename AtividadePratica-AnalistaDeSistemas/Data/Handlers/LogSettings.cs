using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Handlers;

/// <summary>
/// Arquivo LogSettings.cs
/// </summary>
public class LogSettings
{
    /// <summary>
    /// 
    /// </summary>
    private readonly WppDbContext? _context;

    /// <summary>
    /// Construtor da classe LogSettings.
    /// </summary>
    /// <param name="context"></param>
    public LogSettings(WppDbContext context) => this._context = context;

    /// <summary>
    /// Texto da resposta da API
    /// </summary>
    public static string ResponseText { get; set; } = null!;
    /// <summary>
    /// Status da respossta da API
    /// </summary>
    public static int ResponseStatus { get; set; }

    /// <summary>
    /// Registra uma mensagem no log.
    /// </summary>
    /// <param name="ObjMessageSent">ID da mensagem.</param>
    /// <param name="isReceived">Recebeu a mensagem ou não</param>
    /// <param name="isRead">Leu a mensagem ou não</param>
    /// <param name="messageId">ID do retorno da mensagem enviada. (Não é o messaageId é o próprio Id)</param>
    /// <returns>Task representando a execução assíncrona.</returns>
    public async Task LogMessage(object ObjMessageSent, bool isReceived, bool isRead, string messageId)
    {
        var getMessage = new MessageData();
        if (ObjMessageSent is int)
        {
            getMessage = await this._context!.MessageData.FirstOrDefaultAsync(x => x.ID.Equals(ObjMessageSent));
            if (getMessage is null) return;
        }

        var log = new LogData
        {
            MessageSent = ObjMessageSent is string ? ObjMessageSent.ToString() : getMessage.MessageText,
            IsReceivedMessage = isReceived,
            IsReadMessage = isRead,
            StatusCode = (short)ResponseStatus,
            ResponseMessage = ResponseText,
            MessageID = messageId
        };

        this._context!.LogData.Add(log);
        await this._context.SaveChangesAsync();
    }
}