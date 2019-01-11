# Kindly

Kindly is a web-application using ASP.NET Core Web API, Entity Framework Core and Angular 6 created as a learning project following [Neil Cummings'](https://github.com/neilcummings) Udemy course as a base line.

## Prerequisites

To deploy this application it is assumed that the following files exists:

* secrets.json

This file can be accessed from Visual Studio > Project > Manage Secrets.
It is located in a folder following this pattern: "C:\Users\\{user}\AppData\Roaming\Microsoft\UserSecrets\\{guid}\secrets.json"

It should follow this template:

### secrets.json

```json
{
  "AppSettings":
  {
    "EncryptionKey": "{encryption-key}",
    "Cloudinary":
    {
      "Cloud": "{cloudinary-cloud-name}",
      "ApiKey": "{cloudinary-api-key}",
      "ApiSecret": "{cloudinary-api-secret}"
    },
    "Facebook":
    {
      "ApiKey": "{facebook-app-key}",
      "ApiSecret": "{facebook-app-secret}"
    },
    "Google":
    {
      "ApiKey": "{google-app-key}",
      "ApiSecret": "{google-app-secret}"
    },
    
    "Twitter":
    {
      "ApiKey": "{twitter-app-key}",
      "ApiSecret": "{twitter-app-secret}"
    },
    "Microsoft":
    {
      "ApiKey": "{microsoft-app-key}",
      "ApiSecret": "{microsoft-app-secret}"
    },
    "Twilio":
    {
      "ID": "{twilio-id}",
      "Key": "{twilio-key}",
      "FromNumber": "{twilio-from-number}"
    },
    "SendGrid":
    {
      "Key": "{send-grid-key}"
    }
  },
  "ConnectionStrings":
  {
    "DefaultConnection": "{connection-string}"
  }
}
```