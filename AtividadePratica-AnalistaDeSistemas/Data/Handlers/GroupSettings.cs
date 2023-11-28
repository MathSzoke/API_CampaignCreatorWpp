using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using Services;
using Microsoft.EntityFrameworkCore;

namespace Data.Handlers;

/// <summary>
/// Arquivo GroupSettings.cs
/// </summary>
public class GroupSettings : Controller, IGroupSettings
{
    private readonly IRestSettings _restConfiguration;
    /// <summary>
    /// DBContext utilizado para interação com o banco de dados.
    /// </summary>
    protected readonly WppDbContext _context;

    /// <summary>
    /// Construtor da classe GroupSettings.
    /// </summary>
    public GroupSettings(IRestSettings restConfiguration, WppDbContext wppDbContext)
    {
        _restConfiguration = restConfiguration;
        _context = wppDbContext;
    }

    /// <summary>
    /// Cria um grupo no WhatsApp, definindo um nome e números que serão adicionados na hora da criação.
    /// </summary>
    /// <param name="g">Objeto contendo informações para criar um grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> CreateGroup(CreateGroup g)
    {
        g.phones = WppSettings.CleanPhones(g.phones);

        dynamic a = await this._restConfiguration.ExecuteContent("create-group", Method.Post, JsonConvert.SerializeObject(g));

        if (a.StatusCode is 400) return this.BadRequest(a);
        else if (a.StatusCode is 500) return this.Conflict(a);

        JToken token = JToken.Parse(a.Value.ToString());
        var groupWpp = WppSettings.MapToGroupWpp(token, g);

        this._context.GroupWppData.Add(groupWpp);
        await this._context.SaveChangesAsync();

        foreach (var phone in g.phones)
        {
            var memberGroup = new MemberGroup
            {
                GroupID = groupWpp.ID,
                Phones = [phone],
                IsMember = true
            };

            this._context.MemberGroupData.Add(memberGroup);
        }

        await this._context.SaveChangesAsync();

        return this.Ok();
    }

    /// <summary>
    /// Adiciona novos membros ao grupo e participar da campanha!
    /// </summary>
    /// <param name="amg">Objeto contendo informações para adicionar um membro no grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> AddMember(AddMemberGroup amg)
    {
        amg.phones = WppSettings.CleanPhones(amg.phones);

        bool isMember = await this._context.MemberGroupData.AnyAsync(x => x.Phones.Equals(amg.phones) && x.IsMember);

        if (isMember) return this.BadRequest("O usuário já é um membro do grupo.");

        foreach (var phone in amg.phones)
        {
            var memberGroup = new MemberGroup
            {
                GroupID = this._context.GroupWppData.Where(x => x.GroupID.Equals(amg.groupId)).Select(x => x.ID).FirstOrDefault(),
                Phones = [WppSettings.CleanPhone(phone)],
                IsMember = true
            };
            this._context.MemberGroupData.Add(memberGroup);
        }

        await this._context.SaveChangesAsync();

        return await this._restConfiguration.ExecuteContent("add-participant", Method.Post, JsonConvert.SerializeObject(amg));
    }

    /// <summary>
    /// Remove membros do grupo.
    /// </summary>
    /// <param name="rmg">Objeto contendo informações para remover um membro do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> RemoveMember(RemoveMemberGroup rmg)
    {
        rmg.phones = WppSettings.CleanPhones(rmg.phones);

        var getGroupID = this._context.GroupWppData.Where(x => x.GroupID.Equals(rmg.groupId)).Select(x => x.ID).FirstOrDefault();

        var existingMember = this._context.MemberGroupData.Where(x => x.Phones.Equals(rmg.phones)).FirstOrDefault(x => x.GroupID == getGroupID);
        if (existingMember is not null)
        {
            existingMember.IsMember = false;
            await this._context.SaveChangesAsync();
        }

        return await this._restConfiguration.ExecuteContent("remove-participant", Method.Post, JsonConvert.SerializeObject(rmg));
    }

    /// <summary>
    /// Atualiza as configurações de um grupo.
    /// </summary>
    /// <param name="ug">Objeto contendo informações para atualizar as configurações do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> UpdateGroupSettings(UpdateGroup ug) => await this._restConfiguration.ExecuteContent("update-group-settings", Method.Post, JsonConvert.SerializeObject(ug));

    /// <summary>
    /// Atualiza a descrição de um grupo.
    /// </summary>
    /// <param name="ugd">Objeto contendo informações para atualizar a descrição do grupo.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public async Task<IActionResult> UpdateGroupDescription(UpdateGroupDescription ugd) => await this._restConfiguration.ExecuteContent("update-group-description", Method.Post, JsonConvert.SerializeObject(ugd));


    /// <summary>
    /// Retorna o link de convite do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o link de convite.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public string GetInvitationLinkByName(string name) => this._context.GroupWppData.Where(x => x.Name.Equals(name)).Select(x => x.InvitationLink).FirstOrDefault()!;

    /// <summary>
    /// Retorna o ID do grupo do Whatsapp na qual foi criada pelo usuário da API.
    /// </summary>
    /// <param name="name">Nome do grupo para o qual obter o ID.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    public List<string> GetGroupIDByName(string name) => this._context.GroupWppData.Where(x => x.Name.Equals(name)).Select(x => x.GroupID).ToList();
}
