using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using RestSharp;
using Services;

namespace WppCampaign.Controllers;

/// <summary>
/// Controlador para manipulação de webhooks.
/// </summary>
/// <param name="restSettings">Configurações do cliente REST.</param>
/// <param name="wppDbContext">Contexto do banco de dados.</param>
/// <param name="httpContextAccessor">Acessor do contexto HTTP.</param>
/// <param name="configuration">Configurações appsettings.json</param>
[ApiController]
public class WebhookController(IRestSettings restSettings, WppDbContext wppDbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : Controller
{
    private readonly IRestSettings _restConfiguration = restSettings;

    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    /// <summary>
    /// DBContext utilizado para interação com o banco de dados.
    /// </summary>
    protected readonly WppDbContext _context = wppDbContext;

    /// <summary>
    /// Atualiza a URL do webhook para manipulação de status.
    /// </summary>
    /// <returns>ActionResult contendo a resposta do webhook.</returns>
    [HttpPut(nameof(UpdateInstanceWebHook))]
    public async Task<IActionResult> UpdateInstanceWebHook()
    {
        string currentHost = this._httpContextAccessor.HttpContext?.Request?.Host.Value!;

        string callbackUrl = $"{configuration["UrlWebhookCallback"]!}{nameof(Status)}";

        dynamic requestBody = new
        {
            value = callbackUrl
        };

        dynamic webhookResponse = await this._restConfiguration.ExecuteContent("update-webhook-message-status", Method.Put, JsonConvert.SerializeObject(requestBody));

        return webhookResponse;
    }

    /// <summary>
    /// Manipula as atualizações de status recebidas por meio do webhook.
    /// </summary>
    /// <param name="wd">Dados do webhook recebidos no formato JSON.</param>
    /// <returns>ActionResult indicando o status da operação.</returns>
    [HttpPost(nameof(Status))]
    public IActionResult Status([FromBody] WebhookData wd)
    {
        try
        {
            var log = this._context.LogData.FirstOrDefault(x => x.MessageID!.Equals(wd.Ids[0]));

            if(log is null) return this.NotFound("Não foi possível achar o log da mensagem.");

            log.IsReadMessage = wd.Status is "READ" or "PLAYED";
            log.IsReceivedMessage = wd.Status is "RECEIVED";

            this._context.SaveChanges();

            return this.Ok();
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
