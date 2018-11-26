$blankLinesCount = 0
$linesCount = 0
$commentLinesCount = 0

Get-ChildItem ..\src -Filter *.cs -Recurse | ForEach-Object {
    $file = Get-Content $_.FullName

    ForEach ($line in $file) {
        if ($line -match "^( *)$") {
            $blankLinesCount++
        }
        elseif ($line -match "(^( *\/\/).*)") {
            $commentLinesCount++
        }
        else {
            $linesCount++
        }
    }
}

Write-Host "Total Comment Lines: " $commentLinesCount
Write-Host "Total Blank Lines: " $blankLinesCount
Write-Host "Total Lines of Code: " $linesCount