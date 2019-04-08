$buildPath = ".\build\"

# Run Grunt
Set-Location $buildPath;
yarn
grunt

# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem ".\nuget\*.nupkg" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push umbraco package
Get-ChildItem ".\umbraco\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push zip
Get-ChildItem ".\zip\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }