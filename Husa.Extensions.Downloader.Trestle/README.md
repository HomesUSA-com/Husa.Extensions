# Trestle Client
* Husa.Extensions.Downloader.Trestle

# Getting Started
Client library for interacting with a Trestle server to pull real estate listings, photos and other data made available from an MLS system.

# Prerequisites
* Git
* Visual Studio 2022
* .Net Core 6

# Installing

Install the nugget, bind Trestle Options in bootstraper configuration:
* services.BindTrestleOptions();

Add the trestle services
* services.AddTrestleServices();

# Trestle Client
Inject ITrestleClient to your service and use the methods defined in the interface to pull information from trestle

## Values required in the secrets.json file to run the application
For security reasons the passwords and connection strings have been removed from the **appsettings.json** and **appsettings.Development.json** files and replaced by default values that won't work. These must be provided via the secrets.json file. _Please keep in mind the password must match the username and db instance already set in either the **appsettings.json** or **appsettings.Development.json**_.

```
{
  "MarketConfiguration": {
      "ConnectionString": "connection-to-replace",
      "ClientId": "client_to-replace",
      "ClientSecret": "password-to-replace",
      "AuthenticationType": "Basic",
      "LoginUrl": "https://api-prod.corelogic.com/trestle/oidc/connect/token",
      "Timeout": "0.01:00:00",
      "BaseUrl": "https://api-prod.corelogic.com/trestle/",
      "UriType": "Relative",
      "MarketLimit": 20000
  },
  "BlobConfiguration": {
      "ConnectionString": "environment-specific-connection-string-for-azure-table",
      "TableName": "marketAuth",
      "PartitionKey": "trestle"
  }
}
```
# Built With
Versioning
First version will be 0.1.0

## Authors
HomesUSA.com Dev Team

## License
This project is licensed under the MIT License - see the LICENSE.md file for details
