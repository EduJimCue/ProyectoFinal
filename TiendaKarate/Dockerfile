# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copiar todo el contenido al contenedor
COPY . ./
# Restaurar las dependencias
RUN dotnet restore
# Compilar y publicar una versión release
RUN dotnet publish -c Release -o out

# Construir la imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV variable=valorAdjudicado
WORKDIR /app
EXPOSE 5224
COPY --from=build-env /app/out .
VOLUME [ "/volume" ]
ENTRYPOINT ["dotnet", "TiendaKarate.dll"]