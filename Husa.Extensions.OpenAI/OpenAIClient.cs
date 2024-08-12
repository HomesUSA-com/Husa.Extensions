namespace Husa.Extensions.OpenAI;

using System;
using System.Threading.Tasks;
using Husa.Extensions.OpenAI.Enums;
using Husa.Extensions.OpenAI.Models;
using Husa.Extensions.OpenAI.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

public class OpenAIClient : IOpenAIClient
{
    private static readonly Model GptModel = Model.GPT4;

    private readonly IOpenAIAPI openAiApi;
    private readonly IOptions<OpenAIOptions> options;
    private readonly ILogger<OpenAIClient> logger;

    public OpenAIClient(IOpenAIAPI openAiApi, IOptions<OpenAIOptions> options, ILogger<OpenAIClient> logger)
    {
        this.openAiApi = openAiApi ?? throw new ArgumentNullException(nameof(openAiApi));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PromptResponse> CreateChatCompletionAsync(PropertyDetailRequest propertyDetails)
    {
        propertyDetails.ConfigureUserPrompt(this.options.Value.UserPrompt, this.options.Value.MaxReplyCharacters);

        var chatRequest = new ChatRequest
        {
            Model = this.GetModel(),
            Temperature = this.options.Value.Temperature,
            MaxTokens = this.options.Value.MaxTokens,
            user = this.options.Value.ApplicationId.ToString(),
            Messages =
            [
                new(role: ChatMessageRole.System, text: this.options.Value.SystemRole),
                new(role: ChatMessageRole.User, text: propertyDetails),
            ],
        };

        this.logger.LogDebug("Calling Open AI from application {ApplicationId} for property of type {PropertyType} with model {GptModel}", this.options.Value.ApplicationId, propertyDetails.PropertyType, GptModel);
        var chatResult = await this.openAiApi.Chat.CreateChatCompletionAsync(chatRequest);
        this.logger.LogDebug("Reply {ReplyId} received from OpenAI with request Id {RequestId}", chatResult.Id, chatResult.RequestId);
        return new PromptResponse(Description: GetDescription(chatResult));
    }

    private static string GetDescription(ChatResult chatResult)
    {
        if (chatResult == null)
        {
            return string.Empty;
        }

        return chatResult.ToString() ?? string.Empty;
    }

    private Model GetModel() => this.options.Value.Model switch
    {
        ModelType.Gpt4 => Model.GPT4,
        ModelType.Gpt3Turbo => Model.ChatGPTTurbo,
        _ => throw new NotSupportedException(),
    };
}
