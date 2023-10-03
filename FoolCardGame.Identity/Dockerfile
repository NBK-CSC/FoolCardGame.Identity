FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FoolCardGame.Identity/FoolCardGame.Identity.csproj", "FoolCardGame.Identity/"]
RUN dotnet restore "FoolCardGame.Identity/FoolCardGame.Identity.csproj"
COPY . .
WORKDIR "/src/FoolCardGame.Identity"
RUN dotnet build "FoolCardGame.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoolCardGame.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoolCardGame.Identity.dll"]
