using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

/// <summary>
/// Representa um grupo no WhatsApp.
/// </summary>
[Table(nameof(WppGroup), Schema = "WppCampaign")]
public class WppGroup
{
    /// <summary>
    /// Identificador único do grupo.
    /// </summary>
    [JsonIgnore]
    [Key]
    [Column(nameof(ID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    /// <summary>
    /// Identificador único do grupo no WhatsApp.
    /// </summary>
    [Column(nameof(GroupID), TypeName = "nvarchar(50)")]
    public string GroupID { get; set; } = null!;

    /// <summary>
    /// Nome do grupo.
    /// </summary>
    [Column(nameof(Name), TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Link de convite para o grupo.
    /// </summary>
    [Column(nameof(InvitationLink), TypeName = "nvarchar(150)")]
    public string InvitationLink { get; set; } = null!;

    /// <summary>
    /// Grupo de anúncio da comunidade.
    /// </summary>
    [JsonIgnore]
    [Column(nameof(IsGroupAnnouncement), TypeName = "bit")]
    public bool IsGroupAnnouncement { get; set; } = false;

    /// <summary>
    /// Identificador único do grupo no WhatsApp.
    /// </summary>
    [Column(nameof(CommunityID), TypeName = "int")]
    public int? CommunityID { get; set; }

    /// <summary>
    /// Membros do grupo.
    /// </summary>
    public List<MemberGroup> MemberGroup { get; set; } = []; // Relacionamento 1-para-muitos

    /// <summary>
    /// Grupo ao qual o membro pertence.
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    [ForeignKey(nameof(CommunityID))]
    public CommunityData CommunityData { get; set; } = null!;
}

/// <summary>
/// Representa um membro de um grupo.
/// </summary>
[Table(nameof(MemberGroup), Schema = "WppCampaign")]
public class MemberGroup
{
    /// <summary>
    /// Identificador único do membro.
    /// </summary>
    [JsonIgnore]
    [Key]
    [Column("MemberID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberID { get; set; }

    /// <summary>
    /// Grupo ao qual o membro pertence.
    /// </summary>
    [JsonIgnore]
    public WppGroup Group { get; set; } = null!;

    /// <summary>
    /// Identificador único do grupo ao qual o membro pertence.
    /// </summary>
    [JsonIgnore]
    [Column(nameof(GroupID), TypeName = "int")]
    [ForeignKey(nameof(WppGroup.GroupID))]
    public int GroupID { get; set; }

    /// <summary>
    /// Números de telefone dos participantes do grupo.
    /// </summary>
    [Column(nameof(Phones), TypeName = "nvarchar(100)")]
    public List<string> Phones { get; set; } = null!;

    /// <summary>
    /// Indica se o usuário pertence ao grupo em questão.
    /// </summary>
    [Column(nameof(IsMember), TypeName = "bit")]
    public bool IsMember { get; set; }
}

/// <summary>
/// Representa a estrutura de dados para criar um grupo.
/// </summary>
public class CreateGroup
{
    /// <summary>
    /// Foto do grupo.
    /// </summary>
    public string? profileImage { get; set; }

    /// <summary>
    /// Nome do grupo.
    /// </summary>
    public string groupName { get; set; } = null!;

    /// <summary>
    /// Números de telefone dos participantes do grupo.
    /// </summary>
    public List<string> phones { get; set; } = null!;
}

/// <summary>
/// Representa a estrutura de dados para adicionar membros a um grupo.
/// </summary>
public class AddMemberGroup
{
    /// <summary>
    /// Identificador único do grupo.
    /// </summary>
    public string groupId { get; set; } = null!;

    /// <summary>
    /// Números de telefone dos participantes a serem adicionados.
    /// </summary>
    public List<string> phones { get; set; } = null!;
}

/// <summary>
/// Representa a estrutura de dados para remover membros de um grupo.
/// </summary>
public class RemoveMemberGroup
{
    /// <summary>
    /// Identificador único do grupo.
    /// </summary>
    public string groupId { get; set; } = null!;

    /// <summary>
    /// Números de telefone dos participantes a serem removidos.
    /// </summary>
    public List<string> phones { get; set; } = null!;
}

/// <summary>
/// Representa a estrutura de dados para atualizar as configurações de um grupo.
/// </summary>
public class UpdateGroup
{
    /// <summary>
    /// Número de telefone do grupo.
    /// </summary>
    public string phone { get; set; } = null!;

    /// <summary>
    /// Indica se a mensagem é restrita a administradores.
    /// </summary>
    public bool adminOnlyMessage { get; set; }

    /// <summary>
    /// Indica se as configurações são restritas a administradores.
    /// </summary>
    public bool adminOnlySettings { get; set; }

    /// <summary>
    /// Indica se a aprovação do administrador é necessária.
    /// </summary>
    public bool requireAdminAproval { get; set; }
}

/// <summary>
/// Representa a estrutura de dados para atualizar a descrição de um grupo.
/// </summary>
public class UpdateGroupDescription
{
    /// <summary>
    /// Identificador único do grupo.
    /// </summary>
    public string groupId { get; set; } = null!;

    /// <summary>
    /// Nova descrição do grupo.
    /// </summary>
    public string groupDescription { get; set; } = null!;
}