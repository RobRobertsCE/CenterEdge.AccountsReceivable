<#
.SYNOPSIS
Builds and tests this repository.

.DESCRIPTION
Builds the solution and runs any tests.

.PARAMETER Configuration
Configuration to build, defaults to "Release".

.PARAMETER TestType
Type of tests to run. Finds projects that end with this string and "Tests".  For example, "Unit" will run projects named "Something.UnitTests".

.PARAMETER NoBuild
Prevents a build, runs the tests with the current build outputs.

.EXAMPLE
.\Test.ps1

.EXAMPLE
.\Test.ps1 -TestType Integration -Configuration Debug

#>

Param(
  [string]
  $Configuration = "Release",
  
  [string]
  $TestType = "Unit",
  
  [switch]
  $NoBuild
)

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
$rootPath = Join-Path -Resolve $scriptPath ".."

$solutionFile = Get-ChildItem $rootPath\src\*.sln | select -First 1 -ExpandProperty FullName

$testProjects = & $scriptPath\ListProjects.ps1 $solutionFile |
	where { $_.ProjectName.EndsWith(".${TestType}Tests") }

if (-not $NoBuild) {
	dotnet build -c $Configuration $solutionFile
	if ($LASTEXITCODE -ne 0) {
	  exit $LASTEXITCODE
	}
}

$testProjects | % {
	dotnet test --no-build -l trx -c $Configuration $_.Path
}