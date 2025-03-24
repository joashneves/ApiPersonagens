# ApiPersonagens

Uma api que retonar personagens aleatorios


## Inicialização

- Docker 

```bash
docker compose -f infraestrutura/compose.yaml up -d
```

- Iniciar o projeto

```bash
dotnet watch --project apibotdiscord.csproj run -- --project apibotdiscord.csproj
```

# Migrations

```bash
dotnet ef migrations add "InicialFranquiaContext" --context FranquiaContext
dotnet ef database update --context FranquiaContext
dotnet ef migrations add "InicialPersonagemContext" --context PersonagemContext
dotnet ef database update --context PersonagemContext
dotnet ef migrations add "InicialContaContext" --context ContaContext
dotnet ef database update --context ContaContext
```