namespace Husa.Extensions.OpenAI;

using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using global::OpenAI.Chat;
using Husa.Extensions.Common;
using Husa.Extensions.OpenAI.Models;
using Husa.Extensions.OpenAI.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class OpenAIClient : IOpenAIClient
{
    private readonly ChatClient openAiApi;
    private readonly IOptions<OpenAIOptions> options;
    private readonly ILogger<OpenAIClient> logger;

    public OpenAIClient(ChatClient client, IOptions<OpenAIOptions> options, ILogger<OpenAIClient> logger)
    {
        this.openAiApi = client ?? throw new ArgumentNullException(nameof(client));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<PromptResponse> CreateChatCompletionAsync(PropertyDetailRequest propertyDetails)
    {
        propertyDetails.ConfigureUserPrompt(this.options.Value.UserPrompt, this.options.Value.MaxReplyCharacters);

        var completionOptions = new ChatCompletionOptions
        {
            Temperature = (float?)(propertyDetails.Temperature ?? this.options.Value.Temperature),
            MaxOutputTokenCount = this.options.Value.MaxTokens,
        };
        List<ChatMessage> messages = [
            new SystemChatMessage(this.options.Value.SystemRole),
            new UserChatMessage(propertyDetails),
        ];

        this.logger.LogDebug(
            "Calling Open AI from application {ApplicationId} with model {GptModel}",
            this.options.Value.ApplicationId,
            this.options.Value.Model.ToStringFromEnumMember());
        var chatResult = await this.openAiApi.CompleteChatAsync(messages, completionOptions);
        this.logger.LogDebug("Reply {ReplyId} received from OpenAI", chatResult?.Value?.Id);
        return new PromptResponse(Description: this.GetDescription(chatResult));
    }

    public async Task<ICollection<LocatePeopleAndBrandsResponse>> LocatePeopleAndBrandsFromImagesAsync(IEnumerable<LocatePeopleAndBrandsRequest> images)
    {
        if (!images?.Any() ?? true)
        {
            return [];
        }

        var sys = Properties.Resources.LocatePeopleAndBrandingsInstruction;
        var prompt = Properties.Resources.LocatePeopleAndBrandingsPrompt;
        var imageParts = images.Select(img => ChatMessageContentPart.CreateImagePart(new Uri(img.ImageUrl)));
        var messages = new List<ChatMessage>()
        {
            new SystemChatMessage(sys),
            new UserChatMessage(imageParts.Append(ChatMessageContentPart.CreateTextPart(prompt)).ToList()),
        };

        var settings = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat(),
        };

        this.logger.LogDebug(
            "Calling Open AI from application {ApplicationId} with model {GptModel}",
            this.options.Value.ApplicationId,
            this.options.Value.Model.ToStringFromEnumMember());
        ClientResult<ChatCompletion> chatResult;

        try
        {
            chatResult = await this.openAiApi.CompleteChatAsync(messages, settings);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error calling OpenAI CompleteChatAsync");
            return null;
        }

        var responseText = this.GetDescription(chatResult);
        var json = JsonObject.Parse(responseText);
        var imagesText = string.Join("\n", images.Select(img => $"**{img.ImageName}**: {img.ImageUrl}"));
        this.logger.LogDebug(
            "The reply with ID: {ReplyId} received from OpenAI was:\n{ResponseText}\nAnd the images provided were:\n{Images}",
            chatResult?.Value?.Id,
            responseText,
            imagesText);
        return json["images"].AsArray().Select((item, index) =>
        {
            var img = images.ElementAt(index);
            return new LocatePeopleAndBrandsResponse
            {
                ImageName = img.ImageName,
                ImageUrl = img.ImageUrl,
                ContainsBranding = item["contains_branding"].GetValue<bool>(),
                ContainsPersons = item["contains_persons"].GetValue<bool>(),
                Description = item["explanation"].GetValue<string>(),
            };
        }).ToList();
    }

    internal virtual string GetDescription(ClientResult<ChatCompletion> chatResult)
    {
        if (!chatResult?.Value?.Content.Any() ?? true)
        {
            return string.Empty;
        }

        return chatResult.Value.Content
            .Select(x => x.Text)
            .Aggregate((a, b) => string.Join("\n", a, b))
            .Trim();
    }
}
