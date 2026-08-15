FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder

WORKDIR /src
COPY Bookmarks.slnx ./
COPY Source ./Source

RUN dotnet publish Source/ -c Release -o /app/out vlr


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "Bookmarks.dll"]