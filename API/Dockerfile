#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./infoTrack.SearchResult.Api/infoTrack.SearchResult.Api.csproj"
RUN dotnet publish "./infoTrack.SearchResult.Api/infoTrack.SearchResult.Api.csproj" -c release -o /api --no-restore
#Copy SQLite db file
COPY infoTrack.SearchResult.Api/infoTrack.db /api


#Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /api
COPY --from=build /api ./

EXPOSE 5100

ENTRYPOINT ["dotnet", "infoTrack.SearchResult.Api.dll"]