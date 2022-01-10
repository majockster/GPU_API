FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 5166
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GPU_api.dll", "--urls", "http://*5166"]