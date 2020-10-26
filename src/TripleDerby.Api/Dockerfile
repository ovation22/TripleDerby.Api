#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/TripleDerby.Api/TripleDerby.Api.csproj", "src/TripleDerby.Api/"]
COPY ["src/TripleDerby.Core/TripleDerby.Core.csproj", "src/TripleDerby.Core/"]
COPY ["src/TripleDerby.Infrastructure/TripleDerby.Infrastructure.csproj", "src/TripleDerby.Infrastructure/"]
RUN dotnet restore "src/TripleDerby.Api/TripleDerby.Api.csproj"
COPY . .
WORKDIR "/src/src/TripleDerby.Api"
RUN dotnet build "TripleDerby.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TripleDerby.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN /bin/bash -c 'ls -la /wait; chmod +x /wait; ls -la /wait'

CMD /wait && dotnet TripleDerby.Api.dll
