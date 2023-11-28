<h1>Criador de campanhas para Whatsapp</h1>

<p>Esta API tem como objetivo criar campanhas no Whatsapp. O que significa que temos algumas funcionalidades especificas do projeto, tais como:</p> <br>

<ul>
  <li>
    <strong><b>MessageController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "SendMessage()":</b></i></code>
        <p><small>Este método será responsável por enviar mensagens diretas no whatsapp, significando que: você poderá enviar qualquer tipo de mensagem de texto para qualquer número.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "SendMessageConfigurated()":</b></i></code>
        <p><small>Envia uma mensagem configurada com base no ID da mensagem e/ou número de telefone a ser enviado. <mark>Preencha o campo "PhoneNumber" apenas se não houver um número setado ao configurar a mensagem.</mark></small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "NewMessage()":</b></i></code>
        <p><small>Este método será responsável por configurar novas mensagens. Você poderá criar tanto uma mensagem simples de texto quanto uma enquete neste método. Passando true ou false para o valor "IsPollMessage", indicando se é ou não uma enquete. Poderá também criar uma mensagem sem um número de telefone como preferência.</small></p>
      </li>
      <li>
        <code><http-method method="PUT"></http-method><i><b style="color: brown"> "UpdateMessage()":</b></i></code>
        <p><small>Atualiza uma mensagem ou enquete existente com base no ID da mensagem..</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetQueue()":</b></i></code>
        <p><small>Obtém a fila das mensagens no Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "GetChat()":</b></i></code>
        <p><small>Obtém o histórico de chat de um número em específico.</small></p>
      </li>
    </ul>    
  </li>
  
  <br>
  
  <li>
    <strong><b>CommunityController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "CreateCommunity()":</b></i></code>
        <p><small>Cria uma comunidade no Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "LinkGroupCommunity()":</b></i></code>
        <p><small>Liga grupos a uma comunidade existente no Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "UnLinkGroupCommunity()":</b></i></code>
        <p><small>Desfaz a ligação de grupos a uma comunidade no Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="DELETE"></http-method><i><b style="color: brown"> "DesactiveCommunity()":</b></i></code>
        <p><small>Desativa uma comunidade no Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetCommunnitites()":</b></i></code>
        <p><small>Obtém os metadados de uma comunidade no Whatsapp.</small></p>
      </li>
    </ul>    
  </li>
  
  <br>
  
  <li>
    <strong><b>GroupController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "CreateGroup()":</b></i></code>
        <p><small>Cria um grupo no WhatsApp, definindo um nome e números que serão adicionados na hora da criação.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "AddMember()":</b></i></code>
        <p><small>Adiciona novos membros ao grupo.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "RemoveMember()":</b></i></code>
        <p><small>Remove membros do grupo.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "UpdateGroupSettings()":</b></i></code>
        <p><small>Atualiza as configurações de um grupo.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "UpdateGroupDescription()":</b></i></code>
        <p><small>Atualiza a descrição de um grupo.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetInvitationLinkByName()":</b></i></code>
        <p><small>Retorna o link de convite do grupo do Whatsapp na qual foi criada pelo usuário da API.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetGroupIDByName()":</b></i></code>
        <p><small>Retorna o ID do grupo do Whatsapp na qual foi criada pelo usuário da API.</small></p>
      </li>
    </ul>    
  </li>
  
  <br>
  
  <li>
    <strong><b>ContactsController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetContacts()":</b></i></code>
        <p><small>Retorna todos os contatos salvos.</small></p>
      </li>
    </ul>    
  </li>
  
  <br>
  
  <li>
    <strong><b>InstanceController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "GetQRCodeImage()":</b></i></code>
        <p><small>Retorna o Base64 da imagem do QRCode do Whatsapp.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "RestartInstance()":</b></i></code>
        <p><small>Reinicie sua instância.</small></p>
      </li>
      <li>
        <code><http-method method="GET"></http-method><i><b style="color: brown"> "DisconnectInstance()":</b></i></code>
        <p><small>Disconectar conta Whatsapp de sua instância.</small></p>
      </li>
    </ul>    
  </li>
  
  <br>
  
  <li>
    <strong><b>WebhookController:</b></strong>
    <br><br>
    <ul>
      <li>
        <code><http-method method="PUT"></http-method><i><b style="color: brown"> "UpdateInstanceWebHook()":</b></i></code>
        <p><small>Atualiza a URL do webhook para manipulação de status.</small></p>
      </li>
      <li>
        <code><http-method method="POST"></http-method><i><b style="color: brown"> "Status()":</b></i></code>
        <p><small>Manipula as atualizações de status recebidas por meio do webhook.</small></p>
      </li>
    </ul>    
  </li>
</ul>
