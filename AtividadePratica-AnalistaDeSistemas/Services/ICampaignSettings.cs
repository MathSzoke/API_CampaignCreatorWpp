using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services;

/// <summary>
/// Interface obtendo as configurações das campanhas.
/// </summary>
public interface ICampaignSettings
{
    /// <summary>
    /// Cria uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="cc">Dados para criar a comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> CreateCommunity(CreateCommunity cc);
    /// <summary>
    /// Liga grupos a uma comunidade existente no Whatsapp.
    /// </summary>
    /// <param name="lgc">Dados para ligar grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> LinkGroupCommunity(LinkGroupsCommunity lgc);
    /// <summary>
    /// Desfaz a ligação de grupos a uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="ulgc">Dados para desfazer a ligação de grupos a uma comunidade.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> UnLinkGroupCommunity(UnLinkGroupsCommunity ulgc);
    /// <summary>
    /// Desativa uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade a ser desativada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> DesactiveCommunity(string IdCommunity);
    /// <summary>
    /// Obtém os metadados de uma comunidade no Whatsapp.
    /// </summary>
    /// <param name="IdCommunity">ID da comunidade.</param>
    /// <returns>ActionResult representando os metadados da comunidade.</returns>
    Task<IActionResult> GetCommunnitites(string IdCommunity);
}
