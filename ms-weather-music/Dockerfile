# Usar a imagem base do .NET 8 SDK para a construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definir o diretório de trabalho
WORKDIR /app

# Copiar o arquivo .csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e construir
COPY . ./
RUN dotnet publish -c Release -o out

# Gerar a imagem final usando a imagem do .NET 8 ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Definir o diretório de trabalho
WORKDIR /app

# Copiar os arquivos construídos do estágio de build
COPY --from=build /app/out .

# Expor a porta que a aplicação vai usar
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "ms-weather-music.dll"]