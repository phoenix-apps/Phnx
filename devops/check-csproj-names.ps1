$projStartsWithPhnxCount = 0
$projDoesNotStartWithPhnx = 0

Get-ChildItem ..\src -Filter *.csproj -Recurse | ForEach-Object {
    If ($_.Name.StartsWith("Phnx")) {
        $projStartsWithPhnxCount++
        Write-Host "Phnx Project Found: "
        Write-Host $_.FullName
        Write-Host ""
    }
    Else {
        $projDoesNotStartWithPhnx++
        Write-Host "Non-Phnx Project Found: "
        Write-Host $_.FullName
        Write-Host ""
    }
}

Write-Host "Phnx Projects Count:" $projStartsWithPhnxCount
Write-Host "Non-Phnx Projects Count:" $projDoesNotStartWithPhnx