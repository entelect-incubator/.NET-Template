dotnet tool install --global dotnet-reportgenerator-globaltool
dotnet test --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit*]\*" /p:CoverletOutput="./TestResults/"
reportgenerator "-reports:.\Test\TestResults\coverage.cobertura.xml" "-targetdir:.\Test\TestResults\html" -reporttypes:HTML;
