#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/MessageStorage/MessageStorage.Api/MessageStorage.Api.csproj", "Services/MessageStorage/MessageStorage.Api/"]
RUN dotnet restore "Services/MessageStorage/MessageStorage.Api/MessageStorage.Api.csproj"
COPY . .
WORKDIR "/src/Services/MessageStorage/MessageStorage.Api"
RUN dotnet build "MessageStorage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageStorage.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageStorage.Api.dll"]