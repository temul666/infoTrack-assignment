#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./infoTrack.ScrapeJob/infoTrack.ScrapeJob.csproj"
RUN dotnet publish "./infoTrack.ScrapeJob/infoTrack.ScrapeJob.csproj" -c release -o /scrapeJob --no-restore

#Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /scrapeJob
COPY --from=build /scrapeJob ./

ENTRYPOINT ["dotnet", "infoTrack.ScrapeJob.dll"]