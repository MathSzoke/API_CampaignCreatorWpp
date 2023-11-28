using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using Services;

namespace Data.Handlers;

/// <summary>
/// Arquivo CampaignSettings.cs
/// </summary>
public class CampaignSettings : Controller, ICampaignSettings
{
    private readonly IRestSettings _restConfiguration;
    /// <summary>
    /// DBContext utilizado para interação com o banco de dados.
    /// </summary>
    protected readonly WppDbContext _context;

    /// <summary>
    /// Construtor da classe CampaignSettings.
    /// </summary>
    public CampaignSettings(IRestSettings restSettings, WppDbContext wppDbContext)
    {
        _restConfiguration = restSettings;
        _context = wppDbContext;
    }

    /// <summary>
    /// Cria uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="cc">Dados para criar a comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> CreateCommunity(CreateCommunity cc)
    {
        dynamic a = await this._restConfiguration.ExecuteContent("communities", Method.Post, JsonConvert.SerializeObject(cc));

        if (a.StatusCode is 400) return this.BadRequest(a);
        else if (a.StatusCode is 500) return this.Conflict(a);

        JToken token = JToken.Parse(a.Value.ToString());
        var (cd, wppGroups) = await new WppSettings(this._restConfiguration, this._context).MapToCommunityWpp(token, cc);

        this._context.CommunityData.Add(cd);

        if (wppGroups.Any())
        {
            foreach (var wg in wppGroups)
            {
                cd.WppGroups.Add(wg);
                this._context.GroupWppData.Add(wg);
            }
        }

        await this._context.SaveChangesAsync();

        return this.Ok();
    }

    /// <summary>
    /// Liga grupos a uma comunidade existente no Whatsapp.
    /// </summary>
    /// <param name="lgc">Dados para ligar grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> LinkGroupCommunity(LinkGroupsCommunity lgc)
    {
        var community = await this._context.CommunityData.FirstOrDefaultAsync(c => c.CommunityID == lgc.communityId);
        if (community is null) return this.NotFound($"Não foi possível encontrar a comunidade com o ID: {lgc.communityId}.");

        var groupsToUpdate = await this._context.GroupWppData.Where(g => lgc.groupsPhones.Contains(g.GroupID)).ToListAsync();

        foreach (var group in groupsToUpdate)
        {
            group.CommunityID = community.ID;
        }

        await this._context.SaveChangesAsync();
        return await this._restConfiguration.ExecuteContent("communities/link", Method.Post, JsonConvert.SerializeObject(lgc));
    }

    /// <summary>
    /// Desfaz a ligação de grupos a uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="ulgc">Dados para desfazer a ligação de grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> UnLinkGroupCommunity(UnLinkGroupsCommunity ulgc)
    {
        var community = await this._context.CommunityData.FirstOrDefaultAsync(c => c.CommunityID == ulgc.communityId);
        if (community is null) return this.NotFound($"Não foi possível encontrar a comunidade com o ID: {ulgc.communityId}.");

        var groupsToUpdate = await this._context.GroupWppData.Where(g => ulgc.groupsPhones.Contains(g.GroupID)).ToListAsync();

        foreach (var group in groupsToUpdate)
        {
            group.CommunityID = null;
        }

        await this._context.SaveChangesAsync();
        return await this._restConfiguration.ExecuteContent("communities/unlink", Method.Post, JsonConvert.SerializeObject(ulgc));
    }

    /// <summary>
    /// Desativa uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade a ser desativada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> DesactiveCommunity(string IdCommunity)
    {
        try
        {
            var desactive = this._context.CommunityData.FirstOrDefault(x => x.CommunityID.Equals(IdCommunity));

            if(desactive is null) return this.NotFound("Não foi possível encontrar a comunidade para ser desativada.");

            desactive.IsActive = false;

            await this._context.SaveChangesAsync();

            return await this._restConfiguration.ExecuteContent($"communities/{IdCommunity}", Method.Delete, string.Empty);
        }
        catch(Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém os metadados de uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade.</param>
    /// <returns>ActionResult representando os metadados da comunidade.</returns>
    public async Task<IActionResult> GetCommunnitites(string IdCommunity) => await this._restConfiguration.ExecuteContent($"communities-metadata/{IdCommunity}", Method.Get, string.Empty);
}