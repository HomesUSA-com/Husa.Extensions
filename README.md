# Husa.Extensions

## Husa.Extensions.Authorization

Add **UseAuthorizationContext** to **Startup.cs**

## Husa.Extensions.Api.Cors

Add **RegisterCors** and **ConfigureCors** to **Startup.cs**

## Husa.Extensions.Cache

Add **UseCache** to **Startup.cs**

## Husa.Extensions.Media

Add **ConfigureAzureBlobConnection** to **Startup.cs**

#### secrets.json
```
  "Azure": {
    "AccountName":,
    "AccountKey":
  }
```

#### appsettings.json
```
    "ConnectionStrings": {
        "AzureBlobConnection":
    },
    "Application": {
        "AzureConfiguration": {
            "BlobContainerName":
        },
    }
```
