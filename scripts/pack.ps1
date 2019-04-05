$nuget = './src/.nuget/nuget.exe';
$csproj = 'src/Our.Umbraco.DataAnnotations/Our.Umbraco.DataAnnotations.csproj';

& $nuget @("pack", $csproj, '-properties', 'Configuration=Release')