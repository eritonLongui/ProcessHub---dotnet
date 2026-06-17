# ProcessHub

ProcessHub é uma aplicação web para gestão de processos internos, clientes, usuários e documentos, com foco em organização, rastreabilidade e controle de status.

## Objetivo

Centralizar o acompanhamento de processos, seus responsáveis, histórico de alterações e documentos vinculados, em uma API backend preparada para integração com um frontend em React + TypeScript.

## Stack

* .NET 9
* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swashbuckle / Swagger
* FluentValidation
* JWT Authentication

## Funcionalidades já estruturadas

* CRUD de Clients
* CRUD de Users
* CRUD de Documents
* CRUD de Processes
* Relacionamentos entre entidades
* Histórico de alterações de status do processo
* Paginação e filtros para processos
* Validação de entrada
* Tratamento global de exceções
* Autenticação via JWT
* Autorização por roles
* Seed inicial de usuários

## Estrutura do backend

* Entities e Enums
* DbContext com Fluent API
* Repositories
* Services
* DTOs
* Validators
* Middleware global de erro
* Auth / Token service
* Seed de dados

## Status do projeto

Backend funcional e em evolução. O próximo passo é o desenvolvimento do frontend com React + TypeScript para consumir a API.

## Próximos passos

* Criar frontend em React + TypeScript
* Implementar login e persistência do token
* Criar telas de listagem, cadastro e edição
* Consumir endpoints protegidos com JWT
* Construir dashboard e navegação principal

## Observações

Este README está em versão inicial e será expandido ao longo do desenvolvimento do projeto.
