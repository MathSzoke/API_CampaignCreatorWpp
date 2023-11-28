using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

/// <summary>
/// Controlador responsável por operações relacionadas a configurações de grupos.
/// </summary>
/// <param name="groupSettings">Configuração para criar um grupo.</param>
public class GroupController(IGroupSettings groupSettings) : Controller
{
    /// <summary>
    /// Configuração para realizar requisições REST.
    /// </summary>
    private readonly IGroupSettings _groupSettings = groupSettings;

    /// <summary>
    /// Cria um grupo no WhatsApp, definindo um nome e números que serão adicionados na hora da criação.
    /// </summary>
    /// <param name="g">Objeto contendo informações para criar um grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("CreateGroup")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroup g) => await this._groupSettings.CreateGroup(g);

    /// <summary>
    /// Adiciona novos membros ao grupo e participar da campanha!
    /// </summary>
    /// <param name="amg">Objeto contendo informações para adicionar um membro no grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("AddMember")]
    public async Task<IActionResult> AddMember([FromBody] AddMemberGroup amg) => await this._groupSettings.AddMember(amg);

    /// <summary>
    /// Remove membros do grupo.
    /// </summary>
    /// <param name="rmg">Objeto contendo informações para remover um membro do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("RemoveMember")]
    public async Task<IActionResult> RemoveMember([FromBody] RemoveMemberGroup rmg) => await this._groupSettings.RemoveMember(rmg);

    /// <summary>
    /// Atualiza as configurações de um grupo.
    /// </summary>
    /// <param name="ug">Objeto contendo informações para atualizar as configurações do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("UpdateGroupSettings")]
    public async Task<IActionResult> UpdateGroupSettings([FromBody] UpdateGroup ug) => await this._groupSettings.UpdateGroupSettings(ug);

    /// <summary>
    /// Atualiza a descrição de um grupo.
    /// </summary>
    /// <param name="ugd">Objeto contendo informações para atualizar a descrição do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpPost("UpdateGroupDescription")]
    public async Task<IActionResult> UpdateGroupDescription([FromBody] UpdateGroupDescription ugd) => await this._groupSettings.UpdateGroupDescription(ugd);


    /// <summary>
    /// Retorna o link de convite do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o link de convite.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpGet("GetInvitationLinkByName")]
    public string GetInvitationLinkByName(string name) => this._groupSettings.GetInvitationLinkByName(name);

    /// <summary>
    /// Retorna o ID do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o ID.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    [HttpGet("GetGroupIDByName")]
    public List<string> GetGroupIDByName(string name) => this._groupSettings.GetGroupIDByName(name);
}
