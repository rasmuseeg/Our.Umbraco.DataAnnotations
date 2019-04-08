$nuget = './src/.nuget/nuget.exe';
$csproj = 'src/Our.Umbraco.DataAnnotations/Our.Umbraco.DataAnnotations.csproj';
$releasePath = ".\build\release";

& $nuget @("pack", $csproj, '-properties', 'Configuration=Release')

# Run Grunt
grunt --base build

# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem "$releasePath\nuget\*.nupkg" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push umbraco package
Get-ChildItem "$releasePath\umbraco\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push zip
Get-ChildItem "$releasePath\zip\*.zip" | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }