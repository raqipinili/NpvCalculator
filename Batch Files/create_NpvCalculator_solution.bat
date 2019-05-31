@set SolutionName=NpvCalculator
@set ProjectApi=%SolutionName%.Api
@set ProjectCore=%SolutionName%.Core
@set ProjectXunit=%SolutionName%.Tests

@REM -- create solution directory --
@mkdir %SolutionName%
@pushd %SolutionName%

@REM -- create solution file --
dotnet new sln --name %SolutionName%

@REM -- create webapi project --
dotnet new webapi --name %ProjectApi% --output %ProjectApi%

@REM -- add webapi project to solution --
dotnet sln add .\%ProjectApi%\%ProjectApi%.csproj

@REM -- create classlib project --
dotnet new classlib --name %ProjectCore% --output %ProjectCore%

@REM -- add classlib project to solution --
dotnet sln add .\%ProjectCore%\%ProjectCore%.csproj

@REM -- create xunit project --
dotnet new xunit --name %ProjectXunit% --output %ProjectXunit%

@REM -- add xunit project to solution --
dotnet sln add .\%ProjectXunit%\%ProjectXunit%.csproj

ng new NpvCalculator-Angular
@popd

@set SolutionName=
@set ProjectApi=
@set ProjectCore=
@set ProjectXunit=

pause
