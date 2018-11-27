FROM microsoft/dotnet:sdk AS build-env
ARG buildConfiguration=Release
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c $buildConfiguration -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AspNetCoreAuthentication.dll"]



#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 51467
#EXPOSE 44324
#
#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /src
#COPY ["AspNetCoreAuthentication.csproj", "AspNetCoreAuthentication/"]
#RUN dotnet restore "AspNetCoreAuthentication/AspNetCoreAuthentication.csproj"
#COPY . .
#WORKDIR "/src/AspNetCoreAuthentication"
#RUN dotnet build "AspNetCoreAuthentication.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "AspNetCoreAuthentication.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "AspNetCoreAuthentication.dll"]