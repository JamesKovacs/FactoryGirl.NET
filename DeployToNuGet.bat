nuget.exe update -self
ECHO Y | DEL *.nupkg

#NOTE - Using an account for factorygirl@meinershagen.net to publish to NuGet.org.  This can be changed in the future.
set /p NuGetApiKey= Please enter the project's NuGet API Key: 
nuget.exe setApiKey %NuGetApiKey%
nuget.exe pack FactoryGirl.NET\FactoryGirl.NET.csproj
nuget.exe push *.nupkg
