<#
.SYNOPSIS
Builds and packages this repository.

.DESCRIPTION
Builds the solution and puts any Nuget packages in the "artifacts" directory.  Can use a supplied version number, or can calculate one automatically based on Git tags.

.PARAMETER Version
Version number to apply to the build, with an optional suffix, i.e. "1.0.0-beta001".

.PARAMETER AutoVersion
Calculate the version number based on the most recent Git tag.  For example, if the most recent tag is "release-1.0.1", the version number will be "1.0.2".

.PARAMETER VersionSuffix
This suffix will be applied to the automatically calculated version number.

.PARAMETER Configuration
Configuration to build, defaults to "Release".

.EXAMPLE
.\Build.ps1 -AutoVersion

.EXAMPLE
.\Build.ps1 -AutoVersion -VersionSuffix beta001

.EXAMPLE
.\Build.ps1 -Version 1.0.0-beta001 -Configuration Debug

#>

Param(
  [string]
  [parameter(ParameterSetName="SpecificVersion", Mandatory=$true)]
  $Version,
  
  [switch]
  [parameter(ParameterSetName="AutoVersion", Mandatory=$true)]
  $AutoVersion,
  
  [string]
  [parameter(ParameterSetName="AutoVersion")]
  $VersionSuffix = "",
  
  [string]
  $Configuration = "Release"
)

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
$rootPath = Join-Path -Resolve $scriptPath ".."

$solutionFile = Get-ChildItem $rootPath\src\*.sln | select -First 1 -ExpandProperty FullName

# List of Nuget projects to be packaged within the solution
$nugetProjects = & $scriptPath\ListProjects.ps1 $solutionFile |
	where { -not $_.ProjectName.EndsWith("Tests") }

if ($AutoVersion) {
	$Version = & $scriptPath\AutoVersionNumber.ps1 -VersionSuffix $VersionSuffix
}

$additionalParameters = @("-p:Version=$Version")

dotnet build --no-incremental -c $Configuration $additionalParameters $solutionFile
if ($LASTEXITCODE -ne 0) {
  exit $LASTEXITCODE
}

Remove-Item -Recurse -Force $rootPath\artifacts -ErrorAction Ignore
New-Item -ItemType Directory $rootPath\artifacts | Out-Null

$nugetProjects | % {
	dotnet pack --include-symbols --no-build -c $Configuration $additionalParameters $_.Path
	if ($LASTEXITCODE -ne 0) {
	  exit $LASTEXITCODE
	}

	$projectFolder = Split-Path -Parent $_.Path
	Copy-Item $projectFolder\bin\$Configuration\$($_.ProjectName).$Version.*nupkg $rootpath\artifacts\ | Out-Null
}