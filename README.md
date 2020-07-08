# FacebookEventApi

GET
https://apim-find-and-explore.azure-api.net/FacebookApi-Appmilla-dev/Places

GET
https://apim-find-and-explore.azure-api.net/FacebookApi-Appmilla-dev/Health


# Publish Client nuget package

Install Azure Artifacts Credential Provider
https://github.com/microsoft/artifacts-credprovider#azure-artifacts-credential-provider

Install Nuget client tools
https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools#nugetexe-cli

Click Get the tools button on this link with Nuget.exe option selected
https://dev.azure.com/appmilla/FindAndExplore/_packaging?_a=connect&feed=appmilla


This line below works when in the directory C:\GitHub\FacebookApi\FacebookApi.Client> 

C:\Nuget\nuget.exe push -Source "appmilla" -ApiKey az C:\GitHub\FacebookApi\FacebookApi.Client\bin\Debug\FacebookApi.Client.1.0.0.nupkg
