using Services;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Controllers;

/// <summary>
/// Cria uma instância para conectar-se á API z-api.io
/// </summary>
/// <param name="restConfiguration"></param>
[ApiController]
public partial class InstanceController(IRestSettings restConfiguration) : Controller
{
    private readonly IRestSettings _restConfiguration = restConfiguration;

    /// <summary>
    /// Retorna o Base64 da imagem do QRCode do Whatsapp.
    /// </summary>
    /// <returns>Retorna o status de conectado True caso esteja conectado, caso não esteja conectado retornará o Base64 da imagem QR Code do Wpp.</returns>
    [HttpGet("GetQRCodeImage")]
    public async Task<IActionResult> GetQRCodeImage() => await this._restConfiguration.ExecuteContent("qr-code/image", Method.Get, string.Empty);

    /// <summary>
    /// Reinicie sua instância.
    /// </summary>
    /// <returns>Status http 200 informando que foi reiniciado com sucesso.</returns>
    [HttpGet("RestartInstance")]
    public async Task<IActionResult> RestartInstance() => await this._restConfiguration.ExecuteContent("restart", Method.Get, string.Empty);

    /// <summary>
    /// Disconectar conta Whatsapp de sua instância.
    /// </summary>
    /// <returns>Status http 200 informando que foi disconectado com sucesso.</returns>
    [HttpGet("DisconnectInstance")]
    public async Task<IActionResult> DisconnectInstance() => await this._restConfiguration.ExecuteContent("disconnect", Method.Get, string.Empty);
}