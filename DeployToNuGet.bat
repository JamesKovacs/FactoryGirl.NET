nuget.exe update -self

#NOTE - Using an account for factorygirl@meinershagen.net to publish to NuGet.org.  This can be changed in the future.
nuget.exe setApiKey 57dc68f8-50fb-46fe-a99b-175c9099673e
nuget.exe pack FactoryGirl.NET\FactoryGirl.NET.csproj

#NOTE - The version for this package name will need to be updated if the Assembly Version of Assembly File Version are changed.
nuget.exe push FactoryGirl.NET.1.0.0.0.nupkg