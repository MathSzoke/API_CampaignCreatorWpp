using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// Arquivo: MessageSettings.cs
namespace Models;

/// <summary>
/// Configurações relacionadas a mensagens no WhatsApp.
/// </summary>
[Table(nameof(MessageData), Schema = "WppCampaign")]
public class MessageData
{
    /// <summary>
    /// Identificador único das configurações de mensagem.
    /// </summary>
    [Key]
    [Column(nameof(ID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
    [JsonIgnore]
    public int ID { get; set; }

    /// <summary>
    /// Texto da mensagem.
    /// </summary>
    [Column(nameof(MessageText), TypeName = "nvarchar(4000)")]
    public string MessageText { get; set; } = null!;

    /// <summary>
    /// Indica se as configurações de mensagem estão ativas.
    /// </summary>
    [Column(nameof(IsActive), TypeName = "bit")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Indica se a mensagem é uma enquete.
    /// </summary>
    [Column(nameof(IsPollMessage), TypeName = "bit")]
    public bool IsPollMessage { get; set; }

    /// <summary>
    /// Enquete associada às configurações de mensagem (Relacionamento 1-para-1).
    /// </summary>
    [NotMapped]
    public SendPoll? SendPoll { get; set; }
}

/// <summary>
/// Representa uma mensagem a ser enviada.
/// </summary>
public class SendMessage
{
    /// <summary>
    /// Número de telefone do destinatário.
    /// </summary>
    public string phone { get; set; } = null!;

    /// <summary>
    /// Conteúdo da mensagem a ser enviada.
    /// </summary>
    public string message { get; set; } = null!;
}

/// <summary>
/// Representa uma enquete a ser enviada no WhatsApp.
/// </summary>
[Table(nameof(SendPoll), Schema = "WppCampaign")]
public class SendPoll
{
    /// <summary>
    /// Identificador único da enquete.
    /// </summary>
    [JsonIgnore]
    [Key]
    [Column(nameof(PollID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
    public int PollID { get; set; }

    /// <summary>
    /// Identificador único das configurações de mensagem associadas à enquete.
    /// </summary>
    [ForeignKey(nameof(MessageSettings.ID))]
    [Column(nameof(MessageID), TypeName = "int")]
    [JsonIgnore]
    public int MessageID { get; set; }

    /// <summary>
    /// Número de telefone associado à enquete.
    /// </summary>
    [Column("Phone", TypeName = "nvarchar(50)")]
    public string? phone { get; set; }

    /// <summary>
    /// Número máximo de opções na enquete.
    /// </summary>
    [Column("PollMaxOptions", TypeName = "int")]
    public int? pollMaxOptions { get; set; }

    /// <summary>
    /// Texto da mensagem associada à enquete.
    /// </summary>
    public string message => new MessageData().MessageText;

    /// <summary>
    /// Opções da enquete (Relacionamento 1-para-muitos).
    /// </summary>
    public List<PollOptions> PollOptions { get; set; } = [];

    /// <summary>
    /// Configurações de mensagem associadas à enquete (Relacionamento 1-para-1).
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public MessageData MessageSettings { get; set; } = null!;
}

/// <summary>
/// Representa uma opção de enquete.
/// </summary>
[Table(nameof(PollOptions), Schema = "WppCampaign")]
public class PollOptions
{
    /// <summary>
    /// Identificador único da opção de enquete.
    /// </summary>
    [JsonIgnore]
    [Key]
    [Column(nameof(PollOptionID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PollOptionID { get; set; }

    /// <summary>
    /// Identificador único da enquete associada à opção.
    /// </summary>
    [JsonIgnore]
    [ForeignKey(nameof(SendPoll.PollID))]
    [Column(nameof(PollID), TypeName = "int")]
    public int PollID { get; set; }

    /// <summary>
    /// Nome da opção de enquete.
    /// </summary>
    [Column("OptionName", TypeName = "nvarchar(150)")]
    public string name { get; set; } = null!;
}


/// <summary>
/// Classe para desserializar a resposta do método "chat-messages/{phone}" 
/// </summary>
public class ReceivedMessage
{
    /// <summary>
    /// 
    /// </summary>
    public string messageId { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string status { get; set; } = null!;
}

/// <summary>
/// Classe para desserializar a resposta do método "chats/{phone}"
/// </summary>
public class ChatInfo
{
    /// <summary>
    /// 
    /// </summary>
    public int messagesUnread { get; set; }
}