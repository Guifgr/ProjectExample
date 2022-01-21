FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Project.WebApi/Project.WebApi.csproj", "Project.WebApi/"]
COPY ["Project.Application/Project.Application.csproj", "Project.Application/"]
COPY ["Project.Domain/Project.Domain.csproj", "Project.Domain/"]
COPY ["Project.Infrastructure/Project.Infrastructure.csproj", "Project.Infrastructure/"]
RUN dotnet restore "Project.WebApi/Project.WebApi.csproj"

COPY . ./
WORKDIR "/src/Project.WebApi"
RUN dotnet publish -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Project.WebApi.dll", "--environment=Development"]
