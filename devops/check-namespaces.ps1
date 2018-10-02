$namespacePhnx = "namespace Phnx"
$namespaceMarkSFrancis = "namespace MarkSFrancis"

$containsPhnxCount = 0
$containsMarkSFrancisCount = 0

Get-ChildItem ..\src -Filter *.cs -Recurse | ForEach-Object {
    $file = Get-Content $_.FullName
    $containsPhnx = $false
    $containsMarkSFrancis = $false

    ForEach ($line in $file) {
        If ($line -match $namespacePhnx) {
            $containsPhnx = $true
            break
        }
        ElseIf ($line -match $namespaceMarkSFrancis) {
            $containsMarkSFrancis = $true
            break
        }
    }

    If ($containsPhnx) {
        $containsPhnxCount++
        Write-Host "Phnx File Found: "
        Write-Host $_.FullName
        Write-Host ""
    }
    ElseIf ($containsMarkSFrancis) {
        $containsMarkSFrancisCount++
        Write-Host "MarkSFrancis File Found: "
        Write-Host $_.FullName
        Write-Host ""
    }
}

Write-Host "Phnx File Count:" $containsPhnxCount
Write-Host "MarkSFrancis File Count:" $containsMarkSFrancisCount