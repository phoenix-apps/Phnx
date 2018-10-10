$testText = "\[Test\]"

$testsCount = 0

Get-ChildItem ..\src -Filter *.cs -Recurse | ForEach-Object {
    $testsInFile = 0;
    $file = Get-Content $_.FullName

    ForEach ($line in $file) {
        If ($line -match $testText) {
            $testsInFile++
        }
    }

    If ($testsInFile -gt 0)
    {
        $testFilesCount++;
        $testsCount += $testsInFile
    }
}

Write-Host "Total Test Files: " $testFilesCount
Write-Host "Total Tests Count:" $testsCount