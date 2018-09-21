@echo off
set buildFiles="IO/Threaded" "MarkSFrancis" "Benchmark" "Collections" "Console" "Core/Configuration" "Data/Data" "Data/EFCore" "Drawing" "IO/IO" "IO/Json" "Random" "Reflection" "Security" "Serialization" "Web" "Windows/Windows" "Windows/Configuration"

call :build "IO/Threaded"
if %ERRORLEVEL% GEQ 1 goto :quit

echo Starting builds...
for %%a in (%buildFiles%) do (
	echo Building %%a...
	call :build %%a
	
	if %ERRORLEVEL% GEQ 1 echo Quitting...
	if %ERRORLEVEL% GEQ 1 goto :quit
)

echo Finished builds
goto :quit

:build
dotnet restore %~1
if %ERRORLEVEL% GEQ 1 goto :EOF

dotnet build %~1
if %ERRORLEVEL% GEQ 1 echo BUILD FAILED WEEEEEEEEEEEE
goto :EOF

:quit
exit /b %ERRORLEVEL%