# Clínica Online 


### Prerequisites

- .Net 5 
- Docker  
- Docker-Compose 

### Project execution

- Run the following command in the project root directory:
```bash
docker-compose up -d
```

- Access `http://localhost:5005/index.html` and the API should be running correctly!

### Default user

- User: contato<span>@</span>builtcode.com Password: 123456

### Logs

- The application registers logs in Elastic Search that can be consulted in Kibana. Url: `http://localhost:5601/`

### Tests

- Tests can be run with the command 
```bash
dotnet test
```
