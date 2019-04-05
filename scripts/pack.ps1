$nuget = './src/.nuget/nuget.exe';
$csproj = 'src/Our.Umbraco.DataAnnotations/Our.Umbraco.DataAnnotations.csproj';

& $nuget @("pack", $csproj, '-properties', 'Configuration=Release')

# Push all newly created .nupkg files as Appveyor artifacts for later deployment.
Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }