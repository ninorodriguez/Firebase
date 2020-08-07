# Firebase

Our Firebase API is an APS.NET Core 3.1 Web Application, that allows you to upload a file to firebase storage, create an authenticated user and store the user information in firebase cloud storage, update the user information and get the user information.

## How to configured it?
Our project has two methodologies for the configurations.

### Azure app configuration
You can use this methodology creating in AzurePortal an app configuration and storing the followings configurations by environments:

Key | Value  | Label
----| -----  | -----
FirebaseOptions:ApiKey | your ApiKey | Development
FirebaseOptions:Bucket | your Bucket | Development
FirebaseOptions:Email  | your Email  | Development
FirebaseOptions:Password | your Password | Development
FirebaseOptions:ProjectId | your ProjectId | Development

One time you have the Azure app configuration you need to set-up in your project Environment variables the following:

- ASPNETCORE_ENVIRONMENT = Development
- AZCE = your Azure app configuration connection string

### appsettings.json
The second methology is creating an appsettings.environment.json file and adding the following:

```
"FirebaseOptions": {
  "ApiKey": "your ApiKey",
  "ProjectId": "your ProjectId",
  "Bucket": "your Bucket",
  "Email": "your Email",
  "Password": "your Password"
}
```

Another configuration that you need to add is GOOGLE_APPLICATION_CREDENTIALS.

For this configuration follow this link: https://firebase.google.com/docs/admin/setup#windows






