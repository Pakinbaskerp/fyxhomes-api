# Use the official ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["YourProjectName.csproj", "./"]

# Restore dependencies
RUN dotnet restore "YourProjectName.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src"
RUN dotnet build "YourProjectName.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "YourProjectName.csproj" -c Release -o /app/publish

# Final stage: use the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YourProjectName.dll"]