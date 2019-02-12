param (
    [Parameter(Mandatory = $true)][string]$project
)

$projectReference = "<Reference Include="""
$projectReferenceCount = 0
$lastLineWasReference = $false
$file = Get-Content $project

ForEach ($line in $file) {
    If ($line -match $projectReference) {
        $projectReferenceCount++

        Write-Host "Project reference found: "
        $lastLineWasReference = $true
    }
    elseif ($lastLineWasReference) {
        Write-Host $line
        $lastLineWasReference = $false
    }
}

Write-Host "Project References Count:" $projectReferenceCount