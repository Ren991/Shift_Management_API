# Etapa 1: Construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar dependencias
COPY *.sln ./
COPY src/*/*.csproj ./src/
RUN dotnet restore

# Copiar todo el código y construir
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Configurar para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto de la aplicación
EXPOSE 7039

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Shift_management.dll"]
