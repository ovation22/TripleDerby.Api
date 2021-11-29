#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base
WORKDIR /

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN /bin/ash -c 'ls -la /wait; chmod +x /wait; ls -la /wait'

COPY ["tests/TripleDerby.Tests.Integration.DB/TripleDerby.Tests.Integration.DB.csproj", "tests/TripleDerby.Tests.Integration.DB/"]
RUN dotnet restore "tests/TripleDerby.Tests.Integration.DB/TripleDerby.Tests.Integration.DB.csproj"

FROM base AS build

COPY . .
WORKDIR "/tests/TripleDerby.Tests.Integration.DB"

CMD /wait && dotnet test --logger xunit --results-directory ./TestResults