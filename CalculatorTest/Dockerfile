FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CalculatorTest/CalculatorTest.csproj", "CalculatorTest/"]
RUN dotnet restore "CalculatorTest/CalculatorTest.csproj"
COPY . .
WORKDIR "/src/CalculatorTest"
RUN dotnet build "CalculatorTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculatorTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculatorTest.dll"]
