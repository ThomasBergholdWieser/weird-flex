#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
# Restore diagnostic tools via SDKs
RUN mkdir /root/.dotnet/tools
RUN dotnet tool install dotnet-counters --global
RUN dotnet tool install dotnet-trace --global
RUN dotnet tool install dotnet-dump --global
RUN dotnet tool install dotnet-gcdump --global

WORKDIR /src
COPY ["*.props", "./"]
COPY ["*.targets", "./"]
COPY ["ci/*.targets", "ci/"]
COPY ["ci/*.dll", "ci/"]
COPY ["version*.json", "./"]
COPY ["src/WeirdFlex.Api/WeirdFlex.Api.csproj", "src/Tieto.Api/"]
COPY ["src/WeirdFlex.Common/WeirdFlex.Common.csproj", "src/WeirdFlex.Common/"]
COPY ["src/WeirdFlex.Business/WeirdFlex.Business.csproj", "src/WeirdFlex.Business/"]
COPY ["src/WeirdFlex.Data/WeirdFlex.Data.csproj", "src/WeirdFlex.Data/"]
COPY ["src/WeirdFlex.Data.EF/WeirdFlex.Data.EF.csproj", "src/WeirdFlex.Data.EF/"]
RUN dotnet restore -p:Configuration=Release "src/WeirdFlex.Api/WeirdFlex.Api.csproj"
COPY . .
WORKDIR "/src/src/WeirdFlex.Api"
RUN dotnet build "WeirdFlex.Api.csproj" --no-restore -c Release

FROM build AS publish
RUN dotnet publish "WeirdFlex.Api.csproj" --no-restore --no-build -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/tools /root/.dotnet/tools
ENV PATH="/root/.dotnet/tools:${PATH}"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeirdFlex.Api.dll"]