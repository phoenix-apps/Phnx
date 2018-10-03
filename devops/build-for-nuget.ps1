param (
    [Parameter(Mandatory = $true)][string]$project,
    [Parameter(Mandatory = $true)][string]$version
)

$outputDirectory = Get-Location
$outputDirectory = Join-Path $outputDirectory 'nupkgs'

$commandArgs = 'pack ' + $project + ' -p:PackageVersion=' + $version + ' --output ' + $outputDirectory
$command = 'dotnet'

Write-Host "Executing:" $command $commandArgs

& $command "--version"
& $command $commandArgs.Split(" ")