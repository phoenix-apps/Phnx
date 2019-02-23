# Creating Windows Nuget Packages

This guide does not apply to NET Core or NET Standard packages

You'll need the [Nuget CLI](https://www.nuget.org/downloads) installed on your computer

# If a `nuspec` file doesn't exist for the project
1. Navigate in a terminal to the folder containing the project you want to generate a nuspec file for

1. Run `nuget spec`. This will create a new `{project}.nuspec` file in the same folder

# Edit the `nuspec` file
1. Edit `version` to the current release version. If this is the first release, that'll `1.0.0`

1. Set the `id` to the project's name. This is used as the published package name

1. Populate the `description` field with a short description of the package's purpose

1. Ensure that `authors`, `owners`, `copyright`, `licenseUrl`, `projectUrl` and `iconUrl` are set according to the example below

Example nuspec:
```xml
<?xml version="1.0"?>
<package >
  <metadata>
    <id>Phnx.Windows.Drawing</id>
    <version>1.0.0</version>
    <authors>Phoenix Apps Ltd</authors>
    <owners>Phoenix Apps Ltd</owners>
    <copyright>Phoenix Apps Ltd</copyright>
    <licenseUrl>https://github.com/phoenix-apps/Phnx/blob/master/LICENSE</licenseUrl>
    <projectUrl>https://github.com/phoenix-apps/Phnx</projectUrl>
    <iconUrl>https://raw.githubusercontent.com/phoenix-apps/Phnx/master/qrcode-link-to-github.png</iconUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>Provides helpers for drawing and scaling images</description>
  </metadata>
</package>
```

# Compile and package

1. Build the project in Visual Studio

1. Navigate in a terminal to the folder containing the project you want to create a package for

1. Run `nuget pack {project}.csproj`. This will combine your `{project}.nuspec` with the `{project}.csproj` file in the same folder

1. This will generate the `{project}.{version}.nupkg` file for you. This is the file you'll upload

1. Navigate to https://www.nuget.org/packages/upload in a browser

1. Upload the `{project}.{version}.nupkg` you generated here

1. Check the details shown, then click "Publish" at the bottom to publish your new package/ update