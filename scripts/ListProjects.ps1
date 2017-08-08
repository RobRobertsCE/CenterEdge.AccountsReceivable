<#
.SYNOPSIS
Gets a list of projects from a solution file.

.DESCRIPTION
Builds the solution and runs any tests.

.PARAMETER SolutionFile
Solution file to list.

.OUTPUTS
System.Collections.Hashtable
List of project files, relative to the solution file.  Hashtable has "ProjectName" and "Path" properties.

.EXAMPLE
.\Test.ps1

.EXAMPLE
.\Test.ps1 -Configuration Debug

#>

Param(
	[string]
	[parameter(Position=1, Mandatory=$true)]
	$SolutionFile
)

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
$rootPath = Join-Path -Resolve $scriptPath ".."

& dotnet sln $SolutionFile list | Out-String -Stream |
where {
	$_.EndsWith(".csproj")
} |
select @{n="ProjectName";e={$_ -split "\\" | select -last 1 | % { $_.Substring(0, $_.Length-7)}}},
       @{n="Path";e={Join-Path (Split-Path -Parent $SolutionFile) $_}}