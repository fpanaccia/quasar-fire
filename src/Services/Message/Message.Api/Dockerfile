#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Message/Message.Api/Message.Api.csproj", "Services/Message/Message.Api/"]
RUN dotnet restore "Services/Message/Message.Api/Message.Api.csproj"
COPY . .
WORKDIR "/src/Services/Message/Message.Api"
RUN dotnet build "Message.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Message.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Message.Api.dll"]