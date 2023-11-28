// Arquivo: WebhookData.cs
namespace Models;


/// <summary>
/// Representa um participante de um grupo.
/// </summary>
public class WebhookData
{
    public string InstanceId { get; set; }
    public string Status { get; set; }
    public List<string> Ids { get; set; }
    public long Momment { get; set; }
    public int PhoneDevice { get; set; }
    public string Phone { get; set; }
    public string Type { get; set; }
    public bool IsGroup { get; set; }
}