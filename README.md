# Clínica Online 


### Pré-requisitos

- .Net 5 
- Docker  
- Docker-Compose 

### Execução do projeto

- Executar na raiz do projeto:
```bash
docker-compose up -d
```

- Acessar `http://localhost:5005/index.html` e a Api deverá está rodando corretamente!

### Usuário padrão

- Usuario: contato<span>@</span>builtcode.com.br Senha: 123456

### Logs

- A aplicação registra logs no Elastic Search que podem ser consultados no Kibana. Url: `http://localhost:5601/`

### Testes

- Testes podem ser executados com o comando 
```bash
dotnet test
```

### Próximos passos

- Deploy AWS com CI/CD
