FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51467
EXPOSE 44324

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["AspNetCoreAuthentication/AspNetCoreAuthentication.csproj", "AspNetCoreAuthentication/"]
RUN dotnet restore "AspNetCoreAuthentication/AspNetCoreAuthentication.csproj"
COPY . .
WORKDIR "/src/AspNetCoreAuthentication"
RUN dotnet build "AspNetCoreAuthentication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCoreAuthentication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCoreAuthentication.dll"]