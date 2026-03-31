# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore first for layer caching
COPY *.sln .
COPY */*.csproj ./
RUN for f in $(ls -d */); do if [ -f "$f/*.csproj" ]; then :; fi; done
RUN dotnet restore

# copy everything and publish
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set a default fallback port; Render provides PORT at runtime.
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

COPY --from=build /app ./

EXPOSE 8080

# Start and let Program.cs switch to Render's $PORT if present
ENTRYPOINT ["sh", "-c", "dotnet ECommerceApp.dll --urls http://0.0.0.0:${PORT:-8080}"]