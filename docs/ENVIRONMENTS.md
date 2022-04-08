# Environments - deployment to azure environments

# Preconditions
- Repository seecret variable AZURE_CREDENTIALS => put here [service principal](https://docs.microsoft.com/en-us/azure/active-directory/develop/app-objects-and-service-principals) for deployment to azure resources
- Create environment + environment variable
- Create publish profile per environment stored in AZURE_WEBAPP_PUBLISH_PROFILE environment variable.

# Create Service principal
```
az ad sp create-for-rbac --name <name> --role <role> --scopes <scope> --sdk-auth
```
json result:
```
{
  "clientId": "<masked>",
  "clientSecret": "<masked>",
  "subscriptionId": "<masked>",
  "tenantId": "<masked>",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
  "resourceManagerEndpointUrl": "https://management.azure.com/",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com/",
  "managementEndpointUrl": "https://management.core.windows.net/"
}
```
Store json result in repository secret with name : AZURE_CREDENTIALS
- Open git repository
- Settings
- Secrets
- Action
- Create new repository secret
  - Name: AZURE_CREDENTIALS
  - Value: json result from previous step

# Create environment
  - Open git repository
  - Settings
  - Environments
  - Create new environment
  
# Create publish profile per environment
  - Open git repository
  - Settings
  - Environments
  - Open environment
  - Add new secret 
  -- Name: AZURE_WEBAPP_PUBLISH_PROFILE 
  -- Value: web application publish profile

# Setting environment release required reviewersapprove 
  - Open git repository
  - Settings
  - Environments
  - Open environment
  - Set required reviewers

# Current status
2 environments - deployed using [dotner-deploy.yml](../.github/workflows/dotnet-deploy.yml)
- dev - deployed from Main branch
- dev1 - deployed on approve after dev deployment
![image](https://user-images.githubusercontent.com/100909070/162370116-4669346e-a5a0-4d23-b24e-bbcd5a2c0404.png)



