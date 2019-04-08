$nuget = './src/.nuget/nuget.exe';

& $nuget @("restore","src/")

# Install dependencies
yarn