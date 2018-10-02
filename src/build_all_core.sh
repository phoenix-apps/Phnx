# The list of projects to build. This can be a solution, .csproj, or a folder containing either. These are executed in order
buildFiles=("Phnx" "Benchmark" "Collections" "Console" "Core/Configuration" "Data/Data" "Data/EFCore" "Drawing" "IO/IO" "IO/Json" "IO/Threaded" "Random" "Reflection" "Security" "Serialization" "Web" "AspNetCore/AspNetCore" "AspNetCore/Modals" "AspNetCore/Rest")

buildStatus=0

build() {
    dotnet restore $1
    buildStatus=$?

    if [ $buildStatus -ge 1 ]
    then
        return
    fi

    dotnet build $1
    buildStatus=$?
}

for buildFile in ${buildFiles[@]};
do
    echo Building $buildFile...

    build $buildFile

    if [ $buildStatus -ge 1 ]
    then
        break
    fi
done

if [ $buildStatus = 1 ]
then
    exit $buildStatus
fi