#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Imagine_todo_api/Imagine_todo_api.csproj", "Imagine_todo_api/"]
COPY ["Imagine_todo.application/Imagine_todo.application.csproj", "Imagine_todo.application/"]
COPY ["Imagine_todo.domain/Imagine_todo.domain.csproj", "Imagine_todo.domain/"]
COPY ["Imagine_todo.Identity/Imagine_todo.Identity.csproj", "Imagine_todo.Identity/"]
COPY ["Imagine_todo.Persistence/Imagine_todo.Persistence.csproj", "Imagine_todo.Persistence/"]
RUN dotnet restore "./Imagine_todo_api/./Imagine_todo_api.csproj"
COPY . .
WORKDIR "/src/Imagine_todo_api"
RUN dotnet build "./Imagine_todo_api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Imagine_todo_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagine_todo_api.dll"]