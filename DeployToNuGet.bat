nuget.exe update -self
ECHO Y | DEL *.nupkg

#NOTE - Using an account for factorygirl@meinershagen.net to publish to NuGet.org.  This can be changed in the future.
nuget.exe setApiKey 57dc68f8-50fb-46fe-a99b-175c9099673e
nuget.exe pack FactoryGirl.NET\FactoryGirl.NET.csproj
nuget.exe push *.nupkg