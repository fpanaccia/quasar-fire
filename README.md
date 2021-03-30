# Operation Quasar Fire
Meli Challenge

[Download Requirement](https://github.com/fpanaccia/quasar-fire/raw/main/.resources/Operacion%20Fuego%20de%20Quasar%20v1.1.pdf) 


## Getting Started
Make sure you have Docker and Docker Compose [installed](https://docs.docker.com/get-docker/) in your environment. After that, you can run the below commands from the **/src/** directory and get started with the `Operation Quasar Fire` immediately

```
docker-compose build
docker-compose up
```

If you are using the last version of Visual Studio, make sure to have the last stable version of [.Net Core SDK](https://dotnet.microsoft.com/download/dotnet/5.0), you will also need [Docker Desktop](https://docs.docker.com/desktop/), for better performance on windows is recommended to use WSL 2

# Demo

[http://radio-quasar.azurewebsites.net/](http://radio-quasar.azurewebsites.net/)

> **Note:** The demo site is deployed in a Free tier of Azure App Service, so the first request will be slow until all the services are woken up

# Architecture overview

The architecture proposes a microservice oriented architecture implementation with multiple autonomous microservices owning its own data/db when needed, also implements an api aggregator to offer an unified entry point for the microservices.
  
![](.resources/Architecture.png)

Every endpoint has it swagger with examples.

# Test

Having the last version of .Net Core SDK, Docker and Docker Compose installed, run the following commands in the directory **/src/**

```
docker-compose build
docker-compose up -d
dotnet test QuasarFire.sln
```
