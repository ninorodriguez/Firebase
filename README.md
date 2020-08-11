# Firebase
Our Firebase API is an APS.NET Core 3.1 Web Application, that allows you to upload a file to firebase storage, create an authenticated user and store the user information in firebase cloud storage, update the user information and get the user information.

## Our Project structure
We have our project separate by library class like:
- NativoPlusStudio.DataTransferObjects = in this library class, we have all the requests models classes
- NativoPlusStudio.Enums = in this library class, we have the folder names in an enum list
- NativoPlusStudio.FirebaseConnector = in this library class, we have all the service classes that connect with firebase to carry out the function of the APIs
- NativoPlusStudio.FluentValidation = in this library class, we have the validations for the request models
- NativoPlusStudio.Interfaces = in this library class, we have all the interfaces for all classes
- NativoPlusStudio.SharedConfiguration = in this library class, we have all the configuration files
- NativoPlusStudio.WebRequestHandlers = in this library class, we have all the handlers that manage the services

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
- AZCE = your Azure app configuration connection string. Example: "Endpoint= your endpoint;Id=your Id ;Secret= your secret"

### appsettings.json
The second methodology is creating an appsettings.environment.json file. In our configuration, we have the **appsettings.Production.json** file in the NativoPlusStudio.SharedConfiguration class library. In that file, you can add the following:
```
"FirebaseOptions": {
  "ApiKey": "your ApiKey",
  "ProjectId": "your ProjectId",
  "Bucket": "your Bucket",
  "Email": "your Email",
  "Password": "your Password"
}
```
To obtain the above values you need to go to:
1. For ApiKey and ProjectId: Firebase console, your firebase project, open project Settings.
2. For the Bucket:  Storage > Files and copy and paste the folder path and remove the "gs://". Example: "your project name.appspot.com"
3. For the email and password: Authentications > add user. Example: email: admin@test.com, password: password


### firebase.Auth.json file configuration
You can find this file in NativoPlusStudio.SharedConfiguration class library.

Firebase projects support Google service accounts, which you can use to call Firebase server APIs from your app server or trusted environment. If you're developing code locally or deploying your application on-premises, you can use credentials obtained via this service account to authorize server requests.

To authenticate a service account and authorize it to access Firebase services, you must generate a private key file in JSON format.

To generate a private key file for your service account:

1. In the Firebase console, your firebase project, open Settings > Service Accounts.
2. Click Generate New Private Key, then confirm by clicking Generate Key.
3. Securely store the JSON file containing the key.

One time you have the JSON file, you can copy and paste those values in **firebase.Auth.json file**.

### GOOGLE_APPLICATION_CREDENTIALS congiguration
- To configure the GOOGLE_APPLICATION_CREDENTIALS you can use the following link: https://firebase.google.com/docs/admin/setup#windows
- An easy way to configure the GOOGLE_APPLICATION_CREDENTIALS is:
1. Control Panel > System and Security > System 
2. In the left panel click Advanced system settings
3. Then click Environment Variables > click new in system variables, in variable name write GOOGLE_APPLICATION_CREDENTIALS and in value copy and paste the firebae.Auth.json        file path. Example: "C:\Users\nrodr\Documents\Visual Studio 2019\Repo\firebaseapi\NativoPlusStudio.SharedConfiguration\firebaseAuth.json" 

## One time you have all the configurations you can execute the project and start using the APIs.









