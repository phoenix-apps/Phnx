$namespacePhnx = "namespace Phnx"
$namespaceUnknown = "namespace "

$namespacePhnxCount = 0
$namespaceUnknownCount = 0

Get-ChildItem ..\src -Filter *.cs -Recurse | ForEach-Object {
    $file = Get-Content $_.FullName
    $namespaceUnknown = $false

    ForEach ($line in $file) {
        If ($line -match $namespacePhnx) {
            break
        }
        ElseIf ($line -match $namespaceUnknown) {
            $namespaceUnknown = $true
            break
        }
    }

    If ($namespaceUnknown) {
        $namespaceUnknownCount++
        Write-Host "Unknown Namespace File Found: "
        Write-Host $_.FullName
        Write-Host ""
    }
}

Write-Host "Phnx File Count:" $namespacePhnxCount
Write-Host "Unknown File Count:" $namespaceUnknownCount