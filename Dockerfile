FROM microsoft/dotnet:2.2-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./src/fn/*.csproj ./
RUN dotnet restore

# copy and build everything else
COPY ./src/fn ./
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/fn.dll"]
