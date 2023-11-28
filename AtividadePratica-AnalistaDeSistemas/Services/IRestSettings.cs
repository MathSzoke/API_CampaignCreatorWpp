using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Services;

/// <summary>
/// Interface obtendo as configurações do RestSharp.
/// </summary>
public interface IRestSettings
{
    /// <summary>
    /// Cria um <see cref="RestRequest"/> no RestSharp, adicionando Headers na conexão.
    /// </summary>
    /// <param name="resource">Recurso da API.</param>
    /// <param name="method">Método HTTP.</param>
    /// <returns>Instância do <see cref="RestRequest"/>.</returns>
    RestRequest CreateRestRequest(string resource, Method method);

    /// <summary>
    /// Executa a chamada de API.
    /// </summary>
    /// <param name="resource">Recurso da API.</param>
    /// <param name="method">Método HTTP.</param>
    /// <param name="parameter">Parâmetro da chamada.</param>
    /// <returns><see cref="IActionResult"/> contendo a resposta da API.</returns>
    Task<IActionResult> ExecuteContent(string resource, Method method, string parameter);
}
