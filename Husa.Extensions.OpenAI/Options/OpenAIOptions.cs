namespace Husa.Extensions.OpenAI.Options;

using System;
using System.ComponentModel.DataAnnotations;
using Husa.Extensions.OpenAI.Enums;
using Husa.Extensions.OpenAI.Helpers;

public record OpenAIOptions
{
    public const string SectionName = "OpenAIOptions";

    [Required(AllowEmptyStrings = false)]
    public string OrganizationId { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string ApiKey { get; set; } = string.Empty;

    [Required]
    [NotEmptyGuid]
    public Guid ApplicationId { get; set; }

    public ModelType Model { get; set; } = ModelType.Gpt4;

    public int MaxTokens { get; set; } = 300;

    public double Temperature { get; set; } = 0.6;

    public int MaxReplyCharacters { get; set; } = 900;

    public string SystemRole { get; set; } = "You are a helpful real estate agent that uses the details of homes to write appealing and professional home descriptions for MLS listings.";

    public string UserPrompt { get; set; } = "I am listing my home on the MLS and want it to stand out. Please write a detailed home description using the following bullet points";
}
