# Sample ASP.NET Core Application Deployment with GitHub Actions

This repository contains a sample ASP.NET Core application and demonstrates how to build and deploy it to an Azure Web App using GitHub Actions. The application includes a simple endpoint and integrates with Swagger for API documentation.

## Prerequisites

Before you begin, make sure you have the following in place:
- An Azure subscription
- A GitHub account

### Deployment

This repository is configured with GitHub Actions workflows that automate the build, deployment, and infrastructure setup to an Azure Web App.

### Workflow Details

This repository includes two GitHub Actions workflows for building, deploying the application, and setting up Azure infrastructure. Here's an overview of the workflows:

**1. Build and deploy ASP.NET Core app to an Azure Web App**

- **Triggers**: Manual dispatch and pushes to the main branch
- **Environment**: Development
- **.NET SDK Version**: 7.0

The workflow consists of two jobs:

- **build**: This job builds the application, packages it, and uploads the artifact for deployment.
- **deploy**: This job downloads the artifact from the "build" job and deploys the application to the Azure Web App.

**2. Azure Infrastructure as Code (IaC)**

- **Triggers**: Manual dispatch and pushes to the main branch
- **Environment**: Production

The IaC workflow contains a job:

- **create-azure-resources**: This job creates and configures the required Azure resources, such as the Azure App Service plan and Web App. It is executed as part of the setup process for the ASP.NET Core app deployment workflow.

### Configuring Azure Service Principal and Secrets

To set up Azure Service Principal and securely manage secrets for your GitHub Actions workflow, follow these steps:
1. Use the Azure CLI to create a resource group. Replace `{location}` with your desired Azure region (e.g. westeurope).

   ```bash
   az group create --name samples-azure-web-app-with-github-actions --location {location}
   ```

1. Create a Service Principal:

   ```bash
   az ad sp create-for-rbac \
     --name "your-service-principal-name" \
     --role contributor \
     --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group} \
     --json-auth
   ```

    Replace `"your-service-principal-name", {subscription-id}, and {resource-group}` with your values.
    After running the command, you will receive a JSON response with the Service Principal credentials, including clientId, clientSecret, and tenantId.

1. Store these Service Principal credentials securely as secrets in your GitHub repository settings. You should add the following secrets:
    
    > **AZURE_SP_CLIENT_ID:** The clientId value from the JSON response.
    >
    > **AZURE_SP_PASSWORD:** The clientSecret value from the JSON response.
    >
    > **AZURE_SP_TENANT_ID:** The tenantId value from the JSON response.

These secrets will be used for authenticating with Azure in your GitHub Actions workflow.

### Updating Azure Web App Name:

It's important to update the name to be **unique** for your Azure Web App. 
Azure requires that each web app has a globally unique name within its Azure region. 
This uniqueness is necessary to ensure that your app is accessible at a unique URL and doesn't conflict with other Azure resources. 
Therefore, please follow the instruction.

Change the value of the `AZURE_WEBAPP_NAME` variable, which you can find it in the following files:

- `azure-deployment.yml`
- `azure-iac.yml`




### Disclaimer

Please note that this application is intentionally designed as a simplified example for learning purposes. 
It does not adhere to best practices that you would typically use in a production application. 
Instead, it focuses on showcasing the basic setup of deploying an ASP.NET Core app to Azure Web App using GitHub Actions.