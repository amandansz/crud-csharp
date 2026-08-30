# CRUD de Produtos em C#

Projeto desenvolvido para estudo de desenvolvimento backend utilizando C# e MySQL.

O objetivo é evoluir gradualmente uma aplicação console simples até uma aplicação completa com banco de dados, API REST e interface web, aplicando boas práticas de arquitetura ao longo do processo.

---

## Tecnologias

- C#
- .NET
- MySQL
- Git
- GitHub

---

## Funcionalidades atuais

- Cadastro de produtos
- Listagem de produtos
- Organização em camadas (Models, Services e Data)
- Conexão com banco de dados MySQL
- Leitura da senha através de variável de ambiente

---

## Estrutura do projeto

```
crud/
│
├── crud.csproj
│
├── Data/
│   ├── ConnectionFactory.cs
│   ├── IProdutoRepository.cs
│   └── ProdutoRepository.cs
│
├── Models/
│   └── Produto.cs
│
├── Services/
│   └── ProdutoService.cs
│
└── Program.cs
```

---

## Aprendizados

Durante este projeto estou estudando:

- Programação Orientada a Objetos
- Organização em camadas
- Manipulação de banco de dados
- SQL
- Conexão entre C# e MySQL
- Variáveis de ambiente
- Boas práticas de desenvolvimento backend

---

## Próximos passos

- [x] CRUD em memória
- [x] Organização do projeto
- [x] Conexão com MySQL
- [x] Inserir produtos no banco
- [x] Listar produtos do banco
- [x] Atualizar produtos
- [x] Remover produtos
- [ ] API REST com ASP.NET Core
  - [ ] Criar o projeto ASP.NET Core Web API
  - [ ] Configurar a injecao de dependencia para `ConnectionFactory`, repositorio e service
  - [ ] Criar endpoints `GET`, `POST`, `PUT` e `DELETE` para produtos
  - [ ] Criar DTOs e validar os dados recebidos pela API
  - [ ] Documentar e testar os endpoints com Swagger
- [ ] Interface Web
  - [ ] Definir a tecnologia da interface web
  - [ ] Criar a tela de listagem de produtos
  - [ ] Criar formulario para cadastrar e editar produtos
  - [ ] Adicionar acao para remover produtos
  - [ ] Integrar a interface com a API REST