# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
ADD Input/data.csv /App/Input/data.csv
ENTRYPOINT ["dotnet", "SC.DevChallenge.Api.dll"]
