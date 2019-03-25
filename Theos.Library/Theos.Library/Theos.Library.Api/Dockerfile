FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8090

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Theos.Library.Api/Theos.Library.Api.csproj", "Theos.Library.Api/"]
COPY ["Theos.Library.Api/Theos.Library.Api.xml", ""]
COPY ["Theos.Library.Core.Business/Theos.Library.Core.Business.csproj", "Theos.Library.Core.Business/"]
COPY ["Theos.Library.Core.Data/Theos.Library.Core.Data.csproj", "Theos.Library.Core.Data/"]
COPY ["Theos.Library.CrossCutting/Theos.Library.CrossCutting.csproj", "Theos.Library.CrossCutting/"]
COPY ["Theos.Library.Domain/Theos.Library.Domain.csproj", "Theos.Library.Domain/"]
RUN dotnet restore "Theos.Library.Api/Theos.Library.Api.csproj"
COPY . .
WORKDIR "/src/Theos.Library.Api"
RUN dotnet build "Theos.Library.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Theos.Library.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ["Theos.Library.Api/Theos.Library.Api.xml", ""]
ENTRYPOINT ["dotnet", "Theos.Library.Api.dll"]