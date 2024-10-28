# Projeto fullstack-trucks

## Pré-requisitos
- dotnet 7.0
- SQL Server

## Dependências do Projeto

### Backend.API
- Microsoft.AspNetCore.OpenApi                 7.0.13 
- Microsoft.EntityFrameworkCore                7.0.20
- Microsoft.EntityFrameworkCore.SqlServer      7.0.20
- Microsoft.EntityFrameworkCore.Tools          7.0.20
- Microsoft.Extensions.Configuration           7.0.0
- Microsoft.Extensions.Configuration.Json      7.0.0
- Swashbuckle.AspNetCore                       6.5.0

### Backend.API.Tests (Projeto de testes)
- coverlet.collector                          6.0.0
- Microsoft.EntityFrameworkCore.InMemory      7.0.20
- Microsoft.NET.Test.Sdk                      17.8.0
- xunit                                       2.5.3
- xunit.runner.visualstudio                   2.5.3

### FrontEnd

## Como instalar o Projeto do Backend

1. Clone o repositorio do git
   git clone https://github.com/esdrascostasilva/fullstack-trucks

2. Restaure as dependencias
   dotnet restore

3. Execute as migrations
   dotnet ef database update

4. Execute o projeto
   dotnet run
