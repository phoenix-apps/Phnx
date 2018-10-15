$linesCount = 0

Get-ChildItem ..\src -Filter *.cs -Recurse | ForEach-Object {
    $file = Get-Content $_.FullName

    ForEach ($line in $file) {
        if ([string]::IsNullOrWhiteSpace($line) -Or ($line -like "*//*")) {

        } else {
            $linesCount++
        }
    }
}

Write-Host "Total Lines of Code: " $linesCount