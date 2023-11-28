using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

/// <summary>
/// 
/// </summary>
[Table(nameof(LogData), Schema = "WppCampaign")]
public class LogData
{
    /// <summary>
    /// 
    /// </summary>
    [Key]
    [Column(nameof(LogID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(MessageID), TypeName = "nvarchar(25)")]
    public string? MessageID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(MessageSent), TypeName = "nvarchar(4000)")]
    public string? MessageSent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(IsReceivedMessage), TypeName = "bit")]
    public bool IsReceivedMessage { get; set; } = false;

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(IsReadMessage), TypeName = "bit")]
    public bool IsReadMessage { get; set; } = false;

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(StatusCode), TypeName = "smallint")]
    public short? StatusCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(ResponseMessage), TypeName = "nvarchar(4000)")]
    public string? ResponseMessage { get; set; } = null!;
}