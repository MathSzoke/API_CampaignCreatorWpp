using Data;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Services;

namespace Controllers;

/// <summary>
/// Classe gerenciadora de contatos no Whatsapp.
/// </summary>
/// <param name="restConfiguration"></param>
/// <param name="wppDbContext"></param>
public class ContactsController(IRestSettings restConfiguration, WppDbContext wppDbContext) : Controller
{
    private readonly IRestSettings _restConfiguration = restConfiguration;

    /// <summary>
    /// DBContext
    /// </summary>
    protected readonly WppDbContext _context = wppDbContext;

    /// <summary>
    /// Retorna todos os contatos salvos.
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetContacts")]
    public async Task<IActionResult> GetContacts() => await this._restConfiguration.ExecuteContent("contacts", Method.Get, string.Empty);
}
