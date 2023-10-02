FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build
WORKDIR /src

COPY ["SCMSystem/SCMSystem.csproj", "SCMSystem/"]

COPY ["Repositories/Repositories.csproj", "Repositories/"]

COPY ["Services/Services.csproj", "Services/"]

COPY ["Core/Core.csproj", "Core/"]

COPY ["Data/Data.csproj", "Data/"]

RUN dotnet restore "SCMSystem/SCMSystem.csproj"

COPY . .

WORKDIR "/src/SCMSystem"

RUN dotnet build "SCMSystem.csproj" -c Release -o /app/build 

FROM build AS publish

RUN dotnet publish "SCMSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCMSystem.dll"]

