FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/syncinpos.Web.Host/syncinpos.Web.Host.csproj", "src/syncinpos.Web.Host/"]
COPY ["src/syncinpos.Web.Core/syncinpos.Web.Core.csproj", "src/syncinpos.Web.Core/"]
COPY ["src/syncinpos.Application/syncinpos.Application.csproj", "src/syncinpos.Application/"]
COPY ["src/syncinpos.Core/syncinpos.Core.csproj", "src/syncinpos.Core/"]
COPY ["src/syncinpos.EntityFrameworkCore/syncinpos.EntityFrameworkCore.csproj", "src/syncinpos.EntityFrameworkCore/"]
WORKDIR "/src/src/syncinpos.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/syncinpos.Web.Host", "src/syncinpos.Web.Host"]
COPY ["src/syncinpos.Web.Core", "src/syncinpos.Web.Core"]
COPY ["src/syncinpos.Application", "src/syncinpos.Application"]
COPY ["src/syncinpos.Core", "src/syncinpos.Core"]
COPY ["src/syncinpos.EntityFrameworkCore", "src/syncinpos.EntityFrameworkCore"]
WORKDIR "/src/src/syncinpos.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "syncinpos.Web.Host.dll"]
