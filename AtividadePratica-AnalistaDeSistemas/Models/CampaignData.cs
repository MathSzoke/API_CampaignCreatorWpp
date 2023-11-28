using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

/// <summary>
/// 
/// </summary>
[Table(nameof(CommunityData), Schema = "WppCampaign")]
public class CommunityData
{
    /// <summary>
    /// 
    /// </summary>
    [Key]
    [Column(nameof(ID), TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(CommunityID), TypeName = "nvarchar(25)")]
    public string CommunityID { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(Name), TypeName = "nvarchar(250)")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [Column(nameof(IsActive), TypeName = "bit")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    public List<WppGroup> WppGroups { get; set; } = []; // Relacionamento 1-para-muitos
}

/// <summary>
/// 
/// </summary>
public class CreateCommunity
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; } = null!;
}

/// <summary>
/// 
/// </summary>
public class LinkGroupsCommunity
{
    /// <summary>
    /// 
    /// </summary>
    public string communityId { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public List<string> groupsPhones { get; set; } = [];
}

/// <summary>
/// 
/// </summary>
public class UnLinkGroupsCommunity
{
    /// <summary>
    /// 
    /// </summary>
    public string communityId { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public List<string> groupsPhones { get; set; } = [];
}