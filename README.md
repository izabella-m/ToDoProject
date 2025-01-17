Documentação - Desafio Técnico

O projeto é uma aplicação de To-Do List que permite criar, visualizar, filtrar e apagar tarefas. Desenvolvido em .NET com arquitetura baseada em Repository Pattern, utiliza Entity Framework Core para persistência de dados e SQLite como banco de dados. A API RESTful disponibiliza endpoints para gerenciamento das tasks, seguindo boas práticas de desenvolvimento. No front-end, o projeto foi desenvolvido com Vue.js, com funcionalidades como adicionar, visualizar, filtrar e apagar tarefas diretamente na interface.
Foram implementados diálogos modais para adicionar tarefas. A comunicação com a API é feita via HTTP para garantir a integração eficiente entre o front-end e o back-end.

1. Instalação e Configuração

   1.1 Requisitos

   Antes de começar, certifique-se de ter os seguintes requisitos instalados no seu sistema:
   .NET SDK (Recomendado: versão 7 ou superior)
   Node.js (Recomendado: versão 16 ou superior)

    1.2 Instalação do Backend
        
        1.1 Clone o repositório:
        git clone https://github.com/izabella-m/ToDoProject.git
        cd Backend ou cd Frontend

    1.2 Instale as dependências: Para o backend, as dependências já estarão configuradas no arquivo .csproj ou em outros arquivos de configuração.
Execute o seguinte comando para restaurar pacotes:

        dotnet restore

    Criação do Banco de Dados: Como o projeto utiliza a abordagem Code First com Entity Framework, execute os seguintes comandos para criar as migrações e aplicar ao banco de dados:

        dotnet ef migrations add CreateDatabase
        dotnet ef database update

    1.3 Instalação do Frontend
    
    Navegue até a pasta do frontend:

        cd frontend
    Instale as dependências do frontend:
    
        npm install



   1.4 Rodando a Aplicação
    Backend: Execute o backend com o seguinte comando:
            
    dotnet run

    
Frontend: Para rodar o frontend execute:
        
    npm run serve


2. Endpoints da API REST
    
   A API é construída utilizando ASP.NET Core com o Repository Pattern. A seguir, a documentação dos principais endpoints da aplicação.
   
2.1 Endpoint:
            
    GET /GetTasks
  - Descrição: Retorna uma lista de todas as tarefas.
  - Requisição:
  - Método: GET
  - URL: /tasks
  - Resposta:
  - Código de sucesso: 200 OK

2.2 Endpoint:

    GET /GetTasksById/{idTask}
 - Descrição: Retorna os detalhes de uma tarefa específica.
- Requisição:
- Método: GET
- URL: /tasks/{id}
- Parâmetros: id: ID da tarefa.
- Resposta:
- Código de sucesso: 200 OK


2.3 Endpoint: 
        
    POST /Createtask
- Descrição: Cria uma nova tarefa.
- Requisição:
- Método: POST
- URL: /tasks
- Resposta:
- Código de sucesso: 201 Created

2.4 Endpoint:
    
    PUT /UpdateTask
- Descrição: Atualiza os dados de uma tarefa existente (Não utilizado na aplicação)
- Requisição:
- Método: PUT
- URL: /tasks/{id}
- Resposta:
- Código de sucesso: 200 OK

2.5 Endpoint:
    
    DELETE  /DeleteTask
- Descrição: Exclui uma tarefa existente.
- Requisição:
- Método: DELETE
- URL: /tasks/{id}
- Resposta:
- Código de sucesso: 204 No Content


3. Estrutura do Frontend

   3.1 Dependências de Ícones
   Instale as dependências para a biblioteca de ícones:

        npm install @mdi/js @jamescoyle/vue-icon

    3.2 Estrutura de Pastas:
- src/services: Contém a importação da api, bem como chamada dos endpoints.
- src/views: Contém as páginas da aplicação.
- src/components: Contém componentes reutilizáveis. 

    3.3 Páginas e Componentes
- Página Principal (MainPage.vue): Exibe a lista de tarefas e inclui um botão para adicionar novas tarefas, que abre um dialog com 3 inputs: título, descrição e status.
