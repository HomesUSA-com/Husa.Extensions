# Husa.Extensions.OpenAI

## Introduction
This package helps real estate professionals produce high-quality descriptions that capture the essence of each property and attract potential buyers. In the following sections, we will delve into the technical implementation, usage instructions, and examples to help you get started with this powerful tool.

# Property Descriptions Package: Design and Implementation

## Design Overview

The Property Descriptions package is designed as a .NET 8.0 package that leverages the capabilities of OpenAI's API (version 1.11.0) to generate high-quality property descriptions. The package supports both GPT-4 and GPT-3.5-turbo models and is built with a focus on easy integration, configurability, and robustness.

### Key Components

1. **API Controller**: Manages the endpoint for generating property descriptions.
2. **Configuration Options**: Provides configurable settings via `appsettings.json`.
3. **Service Registration**: Ensures the package is correctly configured and registered within the application's dependency injection (DI) container.

## API Controller

### Functionality

The API Controller is responsible for handling HTTP requests to generate property descriptions. It includes the following key functionalities:

- **Endpoint**: Exposes a POST endpoint at `property-descriptions`.
- **Request Handling**: Accepts property details in the request body and returns a generated property description.
- **Logging**: Provides debug logging for request tracing and debugging purposes.

### Expected Inputs and Outputs

#### Input

The controller expects a JSON payload containing property details. The expected structure is defined by the `PropertyDetailRequest` class. Here are the typical fields it might include:

- **PropertyType**: The property's type (e.g. "residential", "commercial", etc.).
- **PropertySubType**: The property's sub-type (e.g. "single-family", "multi-family", etc.).
- **Bathrooms**: Number of bathrooms.
- **SquareFootage**: The size of the property in square feet.
- **SchoolDistrict**: School district of the home.
- **TotalStories**: Additional descriptive text provided by the user.

#### Output

The controller returns a JSON response containing the generated property description. The structure is defined by the `PromptResponse` class, which typically includes:

- **Description**: The generated property description text.

### Sample JSON Input

```json
{
      "PropertyType": "residential",
      "TotalSecondaryBedrooms": 3,
      "TotalPrimaryBedrooms": 1,
      "TotalFullBathrooms": 4,
      "TotalHalfBathrooms": 0,
      "TotalSquareFootage": 3380,
      "TotalLivingSquareFootage": 2380,
      "LotSizeAcres": 0.459,
      "YearBuilt": 2024,
      "PropertySubType": "singleFamilyResidence",
      "SchoolDistrict": "northwest",
      "ElementarySchool": "johnieDaniel",
      "MiddleSchool": "pike",
      "HighSchool": "northwest",
      "EstimatedCompletionDate": "2024-08-10T05:00:00",
      "TotalStories": 1,
      "GarageSpaces": 3,
      "CarportSpaces": 0,
      "InteriorFeatures": [ "cableTVAvailable", "decorativeLighting" ],
      "ExteriorFeatures": [ "coveredPatioPorch", "rainGutters" ],
      "HeatingSystemFeatures": [ "central", "energyStarInstallation" ],
      "CoolingSystemFeatures": [ "centralAir" ],
      "ParkingFeatures": [ "tandemStyle" ],
      "LotDescription": [ "interiorLot", "landscaped", "sprinklerSystem" ]
    }
```

### Sample JSON Output

```json
{
  "description": "This stunning 4-bedroom, 3-bathroom home at 123 Main St, Anytown, USA, offers 2500 square feet of luxurious living space. Features include a beautiful pool, a spacious garage, and a lush garden. A perfect family home with modern amenities and a charming atmosphere."
}
```

## Configuration Options

The package is highly configurable via `appsettings.json`. The configuration settings are encapsulated in the `OpenAIOptions` record:

### Configuration Fields

- **OrganizationId**: OpenAI organization ID.
- **ApiKey**: API key for OpenAI.
- **ApplicationId**: Unique identifier for the application to be used for logging and tracing purposes.
- **Model**: Specifies the model to be used (GPT-4 or GPT-3.5-turbo).
- **MaxTokens**: Maximum number of tokens for the generated description.
- **Temperature**: Controls the creativity of the AI.
- **MaxReplyCharacters**: Maximum length of the generated description.
- **SystemRole**: System role description for context setting.
- **UserPrompt**: Prompt template for generating descriptions.

### Sample Configuration in `appsettings.json`

```json
{
  "OpenAIOptions": {
    "OrganizationId": "your-organization-id",
    "ApiKey": "your-api-key",
    "ApplicationId": "4f8fb251-05a8-4c69-8167-14494a5e1e2c",
    "Model": "Gpt4",
    "MaxTokens": 300,
    "Temperature": 0.6,
    "MaxReplyCharacters": 900,
    "SystemRole": "You are a helpful real estate agent that uses the details of homes to write appealing and professional home descriptions for MLS listings.",
    "UserPrompt": "I am listing my home on the MLS and want it to stand out. Please write a detailed home description using the following bullet points"
  }
}
```

## Service Registration

To ensure the package is correctly configured, the following method must be invoked at the startup of the application:

### Functionality

- **Service Configuration**: Binds and validates configuration options from `appsettings.json`.
- **Dependency Injection**: Registers the OpenAI client as a singleton service.
- **OpenAI Client**: Initializes the OpenAI API client using the configured options and authentication details.

### Example Usage in `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Register the OpenAI API client.
builder.Services.ConfigureOpenAiApiClient();

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
```
