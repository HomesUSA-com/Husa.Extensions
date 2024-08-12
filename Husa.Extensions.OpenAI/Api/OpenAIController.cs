namespace Husa.Extensions.OpenAI.Api;

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Husa.Extensions.OpenAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("property-descriptions")]
public class OpenAIController(IOpenAIClient openAiApiClient, ILogger<OpenAIController> logger) : ControllerBase
{
    private readonly IOpenAIClient openAiApiClient = openAiApiClient ?? throw new ArgumentNullException(nameof(openAiApiClient));
    private readonly ILogger<OpenAIController> logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpPost(Name = "GetPropertyDescription")]
    [Produces(contentType: MediaTypeNames.Application.Json, Type = typeof(PromptResponse))]
    [Consumes(contentType: MediaTypeNames.Application.Json)]
    public async Task<PromptResponse> GetPropertyDescription([FromBody] PropertyDetailRequest propertyDetails)
    {
        this.logger.LogDebug("Getting prompt from open AI for the current property");
        return await this.openAiApiClient.CreateChatCompletionAsync(propertyDetails);
    }
}
