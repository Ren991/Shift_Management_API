# Etapa 1: Construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivo de solución y proyectos
COPY shift_management.sln ./shift_management.sln
COPY src/Application/Application.csproj ./src/Application/
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Infrastucture/Infrastucture.csproj ./src/Infrastucture/
COPY src/Web/Web.csproj ./src/Web/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código y construir
COPY . ./

COPY ["src/Web/barbershop.db" , "src/Web/"]
RUN dotnet publish src/Web/Web.csproj -c Release -o /app/out

# Etapa 2: Configurar para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar la salida de la etapa de construcción
COPY --from=build /app/out .

# Exponer el puerto para Render
EXPOSE 8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Web.dll"]

