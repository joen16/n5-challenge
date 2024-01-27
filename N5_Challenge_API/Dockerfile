FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . ./
RUN dotnet restore "./N5_Challenge_API.csproj" --disable-parallel
RUN dotnet publish "./N5_Challenge_API.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal 
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000

ENTRYPOINT ["dotnet","N5_Challenge_API.dll"]