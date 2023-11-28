using Services;
using Microsoft.AspNetCore.Mvc;
using Models;

// Arquivo: CommunityController.cs
namespace Controllers;

/// <summary>
/// Classe gerenciadora de Comunidades no Whatsapp.
/// </summary>
/// <param name="campaignSettings">Configurações para criar uma campanha.</param>
public partial class CommunityController(ICampaignSettings campaignSettings) : Controller
{
    /// <summary>
    /// Configuração para realizar requisições REST.
    /// </summary>
    private readonly ICampaignSettings _campaignSettings = campaignSettings;

    /// <summary>
    /// Cria uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="cc">Dados para criar a comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("CreateCommunity")]
    public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunity cc) => await this._campaignSettings.CreateCommunity(cc);

    /// <summary>
    /// Liga grupos a uma comunidade existente no Whatsapp.
    /// </summary>
    /// <param name="lgc">Dados para ligar grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("LinkGroupCommunity")]
    public async Task<IActionResult> LinkGroupCommunity([FromBody] LinkGroupsCommunity lgc) => await this._campaignSettings.LinkGroupCommunity(lgc);

    /// <summary>
    /// Desfaz a ligação de grupos a uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="ulgc">Dados para desfazer a ligação de grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("UnLinkGroupCommunity")]
    public async Task<IActionResult> UnLinkGroupCommunity([FromBody] UnLinkGroupsCommunity ulgc) => await this._campaignSettings.UnLinkGroupCommunity(ulgc);

    /// <summary>
    /// Desativa uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade a ser desativada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpDelete("DesactiveCommunity/{IdCommunity}")]
    public async Task<IActionResult> DesactiveCommunity(string IdCommunity) => await this._campaignSettings.DesactiveCommunity(IdCommunity);

    /// <summary>
    /// Obtém os metadados de uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade.</param>
    /// <returns>ActionResult representando os metadados da comunidade.</returns>
    [HttpGet("GetCommunnitites/{IdCommunity}")]
    public async Task<IActionResult> GetCommunnitites(string IdCommunity) => await this._campaignSettings.GetCommunnitites(IdCommunity);
}
