$buildPath = Split-Path $MyInvocation.MyCommand.Path -Parent;
Write-Host "build path: $buildPath";

# Run Grunt
Set-Location $buildPath;
yarn
yarn global add grunt-cli
grunt

# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem ".\release\nuget\*.nupkg" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push umbraco package
Get-ChildItem ".\release\umbraco\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push zip
Get-ChildItem ".\release\zip\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }