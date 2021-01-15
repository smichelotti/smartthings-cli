dotnet tool uninstall -g st

dotnet pack 'SmartThings.Cli.csproj'

dotnet tool install -g st --add-source ./nupkg --version 1.0.0.0