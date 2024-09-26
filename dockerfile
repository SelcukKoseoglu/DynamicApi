FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY DynamicApi.sln ./DynamicApi.sln
COPY DynamicApi/DynamicApi.csproj ./DynamicApi/DynamicApi.csproj

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o /App/out /App/DynamicApi/DynamicApi.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "DynamicApi.dll","--urls", "http://0.0.0.0:4552"]