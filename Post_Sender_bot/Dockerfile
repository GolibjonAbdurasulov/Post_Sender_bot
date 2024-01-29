FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Post_Sender_bot/Post_Sender_bot.csproj", "Post_Sender_bot/"]
RUN dotnet restore "Post_Sender_bot/Post_Sender_bot.csproj"
COPY . .
WORKDIR "/src/Post_Sender_bot"
RUN dotnet build "Post_Sender_bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Post_Sender_bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Post_Sender_bot.dll"]
