using Services;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Data.Handlers;

/// <summary>
/// Configuração para realizar chamadas à API usando RestSharp.
/// </summary>
/// <param name="configuration">Configuração do aplicativo.</param>
public class RestSettings(IConfiguration configuration) : ControllerBase, IRestSettings
{
    /// <summary>
    /// Conecta ao URL da API.
    /// </summary>
    private RestClient Client => new(configuration["UrlAPI"]!);

    /// <summary>
    /// Cria um <see cref="RestRequest"/> no RestSharp, adicionando Headers na conexão.
    /// </summary>
    /// <param name="resource">Recurso da API.</param>
    /// <param name="method">Método HTTP.</param>
    /// <returns>Instância do <see cref="RestRequest"/>.</returns>
    public RestRequest CreateRestRequest(string resource, Method method)
    {
        var request = new RestRequest(resource, method);
        request.AddHeader("content-type", "application/json");
        request.AddHeader("client-token", configuration["ClientToken"]!);

        return request;
    }

    /// <summary>
    /// Executa a chamada de API.
    /// </summary>
    /// <param name="resource">Recurso da API.</param>
    /// <param name="method">Método HTTP.</param>
    /// <param name="parameter">Parâmetro da chamada.</param>
    /// <returns><see cref="IActionResult"/> contendo a resposta da API.</returns>
    public async Task<IActionResult> ExecuteContent(string resource, Method method, string parameter)
    {
        try
        {
            var request = this.CreateRestRequest(resource, method);

            if (!string.IsNullOrEmpty(parameter) || !string.IsNullOrWhiteSpace(parameter)) request.AddParameter("application/json", parameter, ParameterType.RequestBody);

            RestResponse r = await Client.ExecuteAsync(request);

            if (r.IsSuccessful)
            {
                return this.Ok(r.Content);
            }
            else
            {
                return this.BadRequest($"Erro na chamada à API. Código de status: {r.StatusCode}.\n\n Detalhes: {r.Content}\n\n O erro ocorreu na chamada do método: {resource}");
            }
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, $"Ocorreu um erro: {ex.Message}");
        }
    }
}


