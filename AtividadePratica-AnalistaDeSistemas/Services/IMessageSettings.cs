using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services;

/// <summary>
/// Interface obtendo as configurações das mensagens.
/// </summary>
public interface IMessageSettings
{
    /// <summary>
    /// Envia uma mensagem para o número de telefone especificado.
    /// </summary>
    /// <param name="sm">Objeto contendo informações da mensagem a ser enviada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> SendMessage(SendMessage sm);
    /// <summary>
    /// Envia uma mensagem configurada com base no ID da mensagem e/ou número de telefone a ser enviado.
    /// <para>
    /// Preencha o campo "PhoneNumber" apenas se não houver um número setado ao configurar a mensagem.
    /// </para>
    /// </summary>
    /// <param name="MessageID">ID da mensagem a ser enviada. ATENÇÃO: o ID se encontra armazenado no banco de dados, na tabela "MessageSettigns".</param>
    /// <param name="PhoneNumber">Insira o número de telefone para qual a mensagem será enviada. ATENÇÃO: este número deverá ser incluso apenas se a mensagem cadastrada não tiver um número definido para ser enviado.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> SendMessageConfigurated(int MessageID, string? PhoneNumber);
    /// <summary>
    /// Cria uma nova mensagem ou enquete.
    /// <para>
    /// É possivel remover a propriedade "phone" do json e assim, configurar uma mensagem sem um destinatário especifico. <br/>
    /// Sendo assim, a mensagem em questão estar disponivel para N números.
    /// </para>
    /// </summary>
    /// <param name="ms">Objeto contendo informações da mensagem ou enquete a ser criada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> NewMessage(MessageData ms);
    /// <summary>
    /// Atualiza uma mensagem ou enquete existente com base no ID da mensagem.
    /// </summary>
    /// <param name="ms">Objeto contendo informações atualizadas da mensagem ou enquete.</param>
    /// <param name="messageID">ID da mensagem a ser atualizada.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> UpdateMessage(MessageData ms, int? messageID = null);
    /// <summary>
    /// Obtém a fila de mensagens.
    /// </summary>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> GetQueue();
    /// <summary>
    /// Obtém o histórico de chat para o número de telefone especificado.
    /// </summary>
    /// <param name="phone">Número de telefone para o qual obter o histórico de chat.</param>
    /// <returns>ActionResult representando o resultado da operação.</returns>
    Task<IActionResult> GetChat(string phone);
}
