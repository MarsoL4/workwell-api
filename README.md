# WorkWell API

WorkWell API é uma solução RESTful desenvolvida em .NET para gerenciamento de bem-estar corporativo, saúde mental e clima organizacional no ambiente empresarial. O sistema permite a administração de funcionários, psicólogos, empresas, enquetes, denúncias anônimas (OmbudMind), atividades de bem-estar, avaliações emocionais profundas, notificações inteligentes, agenda diária, consultas psicológicas, registros SOS/emergência e muito mais. A arquitetura adota os princípios REST, uso de DTOs, controllers versionados, paginação, HATEOAS, e validação estruturada de dados.

---

## 👥 Integrantes

- **Enzo Giuseppe Marsola** – RM: 556310, Turma: 2TDSPK  
- **Rafael de Souza Pinto** – RM: 555130, Turma: 2TDSPY  
- **Luiz Paulo F. Fernandes** – RM: 555497, Turma: 2TDSPF
---

## 🏗 Justificativa do Domínio e Arquitetura

O domínio foi escolhido visando fomentar um ambiente de trabalho mais saudável, transparente e conectado com o bem-estar emocional de colaboradores. O sistema facilita a comunicação segura (inclusive anônima), promove o acompanhamento ativo de humor, realiza triagens psicológicas, incentiva a participação em eventos de qualidade de vida e oferece ferramentas de análise para gestores de RH.  
A arquitetura segue boas práticas REST: uso de DTOs, controllers organizados por domínio, separação em camadas Service/Repository, autenticação por API Key, versionamento de APIs, paginação e HATEOAS nos endpoints paginados.

---

## 🚀 Instruções de Execução

1. **Clonar o Repositório:**
   ```bash
   git clone https://github.com/MarsoL4/workwell-api.git
   cd workwell-api
   ```

2. **Configurar o Banco de Dados:**  
   Edite o arquivo `WorkWell.API/appsettings.json` com sua string de conexão Oracle em `"ConnectionStrings:Oracle"`.

3. **Restaurar os Pacotes e Compilar:**  
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Aplicar Migrations ou inicializar o banco de dados:**
   ```bash
   dotnet ef database update --project WorkWell.Infrastructure --startup-project WorkWell.API
   ```

5. **Executar a API:**  
   ```bash
   dotnet run --project WorkWell.API
   ```
   Acesse a documentação Swagger em:  
   `http://localhost:5152/swagger` ou `https://localhost:7096/swagger`

6. **Rodar Testes Automatizados:**  
   ```bash
   dotnet test
   ```

---

## 🔑 Segurança

- Todos os endpoints (exceto `/swagger` e `/health`) requerem autenticação via header de API Key:  
  `X-API-KEY: <sua-chave>`  
  As chaves são definidas em `WorkWell.API/appsettings.json`, por padrão:
  - Admin: `admin-api-key`
  - RH: `rh-api-key`
  - Psicologo: `psicologo-api-key`
  - Funcionario: `funcionario-api-key`
  - SuperApiKey: `super-api-key` (acesso irrestrito, cuidado!)
- Troque as chaves no arquivo de configuração para produção e nunca exponha suas chaves.

---

## 🩺 Health Checks

- Endpoint de health check disponível em:  
  ```
  GET /health
  ```
- Resposta em JSON exibindo status da aplicação e do banco de dados Oracle.
- Exemplo:
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.9800000",
  "entries": {
    "Database": {
      "status": "Healthy",
      "duration": "00:00:00.9300000",
      "tags": []
    }
  }
}
```

---

## 🔄 Versionamento de API

- Todos os endpoints são versionados por URL no padrão:  
  ```
  /api/v1/[controller]
  ```

---

## 🔑 Principais Entidades

- **Empresa:** Dados institucionais, políticas e identidade visual etc.
- **Setor:** Segmentação dos times/departamentos da empresa.
- **Funcionario:** Pessoas cadastradas, associadas a um setor, com diferentes cargos (Admin, RH, Psicólogo, Funcionário).
- **Psicologo:** Funcionários com papel de Psicólogo e registro CRP.
- **Agenda:** Planejamento e registro diário de atividades do colaborador.
- **AtividadeBemEstar:** Eventos/palestras/ações voltadas a saúde e integração.
- **ConsultaPsicologica:** Agendamento, acompanhamento e histórico de consultas com psicólogos.
- **ChatAnonimo:** Canal de apoio emocional totalmente sigiloso entre funcionário e psicólogo.
- **SOSemergencia:** Registro de acionamentos de emergência por colaboradores.
- **MoodCheck:** Autoavaliação diária do humor.
- **AvaliacaoProfunda:** Triagens psicológicas mais detalhadas (ex: GAD-7, PHQ-9).
- **PerfilEmocional:** Resumo do perfil emocional do colaborador na empresa.
- **RiscoPsicossocial:** Registros de detecção de riscos psicossociais.
- **Enquete:** Perguntas rápidas de clima organizacional.
- **Denuncia (OmbudMind):** Relatos anônimos de assédio, condutas antiéticas etc.
- **Notificacao:** Alertas dinâmicos enviados para funcionários/app.

---

## 📑 Endpoints e Exemplos de Uso

### 👤 FuncionarioController

#### Lista paginada de funcionários
- **GET** `/api/v1/funcionario?page=1&pageSize=10`
- **Resposta:**
    ```json
    {
      "items": [
        {
          "id": 1,
          "nome": "Carlos Silva",
          "email": "carlos@futurework.com",
          "senha": "func123",
          "tokenEmpresa": "token-ftw-001",
          "cargo": 2,
          "ativo": true,
          "setorId": 2
        }
      ],
      "totalCount": 1,
      "page": 1,
      "pageSize": 10,
      "links": [
        { "rel": "self", "method": "GET", "href": "/api/v1/funcionario?page=1&pageSize=10" }
      ]
    }
    ```

#### Buscar funcionário por ID
- **GET** `/api/v1/funcionario/{id}`

#### Criar funcionário
- **POST** `/api/v1/funcionario`
- **Payload de exemplo:**
    ```json
    {
      "nome": "Carlos Silva",
      "email": "carlos@futurework.com",
      "senha": "func123",
      "tokenEmpresa": "token-ftw-001",
      "cargo": 2,
      "ativo": true,
      "setorId": 2
    }
    ```

#### Atualizar funcionário
- **PUT** `/api/v1/funcionario/{id}`

#### Remover funcionário
- **DELETE** `/api/v1/funcionario/{id}`

---

### 🧑‍⚕️ PsicologoController

#### Lista paginada de psicólogos
- **GET** `/api/v1/psicologo?page=1&pageSize=10`

#### Buscar psicólogo por ID
- **GET** `/api/v1/psicologo/{id}`

#### Criar psicólogo
- **POST** `/api/v1/psicologo`
- **Payload exemplo:**
    ```json
    {
      "nome": "Dra. Helena Alves",
      "email": "helena.alves@futurework.com",
      "senha": "psic123",
      "tokenEmpresa": "token-ftw-001",
      "crp": "06/123456",
      "ativo": true,
      "setorId": 1
    }
    ```

---

### 🗓️ AgendaFuncionarioController

#### Listar agendas
- **GET** `/api/v1/agendafuncionario`

#### Buscar agenda por ID
- **GET** `/api/v1/agendafuncionario/{id}`

#### Criar agenda
- **POST** `/api/v1/agendafuncionario`
- **Payload exemplo:**
    ```json
    {
      "funcionarioId": 4,
      "data": "2025-11-21T00:00:00",
      "itens": [
        {
          "tipo": "atividade",
          "titulo": "Participação em palestra",
          "horario": "2025-11-21T10:00:00"
        }
      ]
    }
    ```

#### Adicionar item à agenda
- **POST** `/api/v1/agendafuncionario/{agendaId}/itens`
- **Payload:**  
    ```json
    {
      "tipo": "atividade",
      "titulo": "Participação em palestra",
      "horario": "2025-11-21T10:00:00"
    }
    ```

---

### 🧩 EnqueteController

#### Listar enquetes (paginação)
- **GET** `/api/v1/enquete?page=1&pageSize=10`

#### Buscar enquete por ID
- **GET** `/api/v1/enquete/{id}`

#### Criar enquete
- **POST** `/api/v1/enquete`
- **Payload:**
    ```json
    {
      "empresaId": 1,
      "pergunta": "Você está satisfeito com as condições de trabalho?",
      "ativa": true
    }
    ```

#### Responder enquete
- **POST** `/api/v1/enquete/{enqueteId}/respostas`
- **Payload:**
    ```json
    {
      "funcionarioId": 4,
      "resposta": "Sim"
    }
    ```

---

### 🚨 SOSemergenciaController

#### Listar registros SOS
- **GET** `/api/v1/sosemergencia`

#### Criar registro SOS
- **POST** `/api/v1/sosemergencia`
- **Payload:**
    ```json
    {
      "funcionarioId": 4,
      "tipo": "Crise de ansiedade"
    }
    ```

---

### 🕵️ DenunciaController (OmbudMind)

#### Listar denúncias  
- **GET** `/api/v1/denuncia`

#### Buscar denúncia por código de rastreamento
- **GET** `/api/v1/denuncia/codigo/{codigo}`

#### Criar denúncia ética anônima
- **POST** `/api/v1/denuncia`
- **Payload:**
    ```json
    {
      "funcionarioDenuncianteId": 4,
      "empresaId": 1,
      "tipo": 0,
      "descricao": "Relato de assédio moral pelo gestor.",
      "status": 0,
      "codigoRastreamento": "WW-2024-0001"
    }
    ```

---

### 🧬 MoodCheckController

#### Listar autoavaliações de humor
- **GET** `/api/v1/moodcheck`

#### Criar avaliação de humor diária
- **POST** `/api/v1/moodcheck`
- **Payload:**
    ```json
    {
      "funcionarioId": 4,
      "humor": 4,
      "produtivo": true,
      "estressado": false,
      "dormiuBem": true
    }
    ```

---

### 📣 NotificacaoController

#### Listar notificações paginadas
- **GET** `/api/v1/notificacao?page=1&pageSize=10`

#### Listar notificações por funcionário
- **GET** `/api/v1/notificacao/funcionario/{funcionarioId}`

---

Demais endpoints - como AtividadesBemEstar, IndicadoresEmpresa, PerfilEmocional, Setor, ConsultaPsicologica etc. - seguem o mesmo padrão Rest, sempre com exemplos no Swagger UI que podem ser consultados em [SwaggerExamples](WorkWell.API/SwaggerExamples)

---

## 🧩 Swagger/OpenAPI

- Todos os endpoints possuem documentação detalhada, exemplos de payload e modelos de dados.
- Acesse `/swagger` para explorar e testar a API.
