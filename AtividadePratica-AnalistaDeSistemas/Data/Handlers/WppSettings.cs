using Models;
using Services;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using RestSharp;

namespace Data.Handlers;

/// <summary>
/// Arquivo WppSettings.cs
/// </summary>
public partial class WppSettings
{
    private readonly IRestSettings _restConfiguration;
    /// <summary>
    /// DBContext utilizado para interação com o banco de dados.
    /// </summary>
    protected readonly WppDbContext _context;

    /// <summary>
    /// Construtor da classe WppSettings.
    /// </summary>
    public WppSettings(IRestSettings restSettings, WppDbContext wppDbContext)
    {
        _restConfiguration = restSettings;
        _context = wppDbContext;
    }

    /// <summary>
    /// Limpa uma lista de números de telefone, removendo todos os simbolos e caracteres que não sejam apenas números.
    /// </summary>
    /// <param name="phones"></param>
    /// <returns></returns>
    public static List<string> CleanPhones(List<string> phones)
    {
        var cleanedPhones = new List<string>();

        foreach (var phone in phones)
        {
            string cleanedPhone = phone.Contains("-group") ? phone : RegexNumber().Replace(phone, "");
            cleanedPhones.Add(cleanedPhone);
        }

        return cleanedPhones;
    }

    /// <summary>
    /// Limpa um unico número de telefone, removendo todos os simbolos e caracteres que não sejam apenas números.
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public static string CleanPhone(string? phone)
    {
        string cleanedPhone = phone!.Contains("-group") ? phone : RegexNumber().Replace(phone, "");

        return cleanedPhone;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="innerToken"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static WppGroup MapToGroupWpp(JToken innerToken, CreateGroup request) => new()
    {
        GroupID = (string)innerToken.SelectToken("phone")!,
        InvitationLink = (string)innerToken.SelectToken("invitationLink")!,
        Name = request.groupName,
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="innerToken"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(CommunityData cd, List<WppGroup> wppGroups)> MapToCommunityWpp(JToken innerToken, CreateCommunity request)
    {
        var cd = new CommunityData
        {
            CommunityID = (string)innerToken.SelectToken("id")!,
            Name = request.name,
        };

        var subGroupsToken = innerToken.SelectToken("subGroups");

        var wppGroups = new List<WppGroup>();

        if (subGroupsToken != null && subGroupsToken.Any())
        {
            foreach (var subGroupToken in subGroupsToken)
            {
                string groupID = (string)subGroupToken.SelectToken("phone")!;

                dynamic invitationLink = await this._restConfiguration
                    .ExecuteContent($"group-metadata/{groupID}", Method.Get, string.Empty);

                JToken token = JToken.Parse(invitationLink.Value.ToString());

                var wg = new WppGroup
                {
                    GroupID = groupID,
                    InvitationLink = (string)token.SelectToken("invitationLink")!,
                    Name = (string)subGroupToken.SelectToken("name")!,
                    IsGroupAnnouncement = (bool)subGroupToken.SelectToken("isGroupAnnouncement")!,
                    CommunityID = cd.ID
                };

                wppGroups.Add(wg);
            }
        }

        return (cd, wppGroups);
    }

    [GeneratedRegex("[^0-9]")]
    public static partial Regex RegexNumber();
}
