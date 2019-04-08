$nuget = './src/.nuget/nuget.exe';
$csproj = 'src/Our.Umbraco.DataAnnotations/Our.Umbraco.DataAnnotations.csproj';

& $nuget @("pack", $csproj, '-properties', 'Configuration=Release')

# Run Grunt
Set-Location -Path .\build\
grunt

# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem .\release\nuget\*.nupkg | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push umbraco package
Get-ChildItem .\release\umbraco\*.zip | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }

# Push zip
Get-ChildItem .\release\zip\*.zip | ForEach-Object { Push-AppveyorArtifact $_.FullName -FileName $_.Name }