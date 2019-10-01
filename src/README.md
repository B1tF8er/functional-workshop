# Samples of functional programming concepts #

## How to build and run using `dotnet cli` ##

```pwsh
dotnet build fn -c Release
dotnet run -p fn -c Release
```

## How to build and run using `docker cli` ##

```pwsh
docker build -t fn-image fn
docker run --rm fn-image
```
