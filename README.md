#Firebase

FirebaseApi is an ASP.NET project that makes possible the interaction
with Firebase Storage and Database- CloudStorage

##Configurations

Before executes the APIs you need to configure your Firebase settings
in the appsetting.Production.json file. You need the following settings:
- ApiKey = Firebase ApiKey
- ProjectId = Firebase ProjectId
- Bucket = Firebase Storage Bucket
- Email = an email that exists in Firebase Authentication Users
- Password = a password for the previous email

Also, you need to configure the GOOGLE_APPLICATION_CREDENTIALS environment
with the path of the Firebase Auth .json file that you can generate in
Project settings - Service accounts and Generate a new private key

##NugetPackages
- FirebaseAdmin
- FirebaseAuthentication.net
- FirebaseStorage.net
- Google.Cloud.Firestore



