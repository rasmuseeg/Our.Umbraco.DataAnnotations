$nuget = './src/.nuget/nuget.exe';
$csproj = 'src/Our.Umbraco.DataAnnotations/Our.Umbraco.DataAnnotations.csproj';

& $nuget @("pack", $csproj, '-properties', 'Configuration=Release')

# Run Grunt
grunt



# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem .\build\release\nuget\*.nupkg | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push umbraco package
Get-ChildItem .\build\release\umbraco\*.zip | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push zip
Get-ChildItem .\build\release\zip\*.zip | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }