FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN export DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/MyBlogWeb
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/MyBlogWeb/out ./
ENTRYPOINT ["dotnet", "MyBlogWeb.dll", "--server.urls", "http://*:60000"]