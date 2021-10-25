# CI explained
In this repository, the project is built and tested at every push on the `main` branch.

### Tests
Tests are ran using `dotnet test` and use the Debug configuration.

### Builds
Build are done using `dotnet build` and use the Release configuration.

### Deployments
Deployments (to NuGet) are done only when a release is published. You can publish a new release directly on the web Github web interface.  
One important point: The release must have a tag and where this tag's name is the package version formed as following : `v.1.0.0` (v.MAJOR.MINOR.FIX)