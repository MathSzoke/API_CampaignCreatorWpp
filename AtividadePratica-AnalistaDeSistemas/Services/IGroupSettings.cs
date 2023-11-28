using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services;

/// <summary>
/// Interface obtendo as configurações dos grupos.
/// </summary>
public interface IGroupSettings
{
    /// <summary>
    /// Cria um grupo no WhatsApp, definindo um nome e números que serão adicionados na hora da criação.
    /// </summary>
    /// <param name="g">Objeto contendo informações para criar um grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> CreateGroup(CreateGroup g);
    /// <summary>
    /// Adiciona novos membros ao grupo e participar da campanha!
    /// </summary>
    /// <param name="amg">Objeto contendo informações para adicionar um membro no grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> AddMember(AddMemberGroup amg);
    /// <summary>
    /// Remove membros do grupo.
    /// </summary>
    /// <param name="rmg">Objeto contendo informações para remover um membro do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> RemoveMember(RemoveMemberGroup rmg);
    /// <summary>
    /// Atualiza as configurações de um grupo.
    /// </summary>
    /// <param name="ug">Objeto contendo informações para atualizar as configurações do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> UpdateGroupSettings(UpdateGroup ug);
    /// <summary>
    /// Atualiza a descrição de um grupo.
    /// </summary>
    /// <param name="ugd">Objeto contendo informações para atualizar a descrição do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> UpdateGroupDescription(UpdateGroupDescription ugd);
    /// <summary>
    /// Retorna o link de convite do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o link de convite.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    string GetInvitationLinkByName(string name);
    /// <summary>
    /// Retorna o ID do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o ID.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    List<string> GetGroupIDByName(string name);
}
