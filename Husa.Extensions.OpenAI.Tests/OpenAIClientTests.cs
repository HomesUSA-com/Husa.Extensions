namespace Husa.Extensions.OpenAI.Tests;

using System;
using System.Threading.Tasks;
using Husa.Extensions.OpenAI.Enums;
using Husa.Extensions.OpenAI.Models;
using Husa.Extensions.OpenAI.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OpenAI_API;
using OpenAI_API.Chat;
using Xunit;

public class OpenAIClientTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenDependenciesAreNull()
    {
        // Arrange
        var openAiApiMock = new Mock<IOpenAIAPI>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new OpenAIClient(null, optionsMock.Object, loggerMock.Object));
        Assert.Throws<ArgumentNullException>(() => new OpenAIClient(openAiApiMock.Object, null, loggerMock.Object));
        Assert.Throws<ArgumentNullException>(() => new OpenAIClient(openAiApiMock.Object, optionsMock.Object, null));
    }

    [Fact]
    public async Task CreateChatCompletionAsync_ReturnsPromptResponse_WhenApiCallIsSuccessful()
    {
        // Arrange
        var openAiApiMock = new Mock<IOpenAIAPI>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();

        var openAIOptions = new OpenAIOptions
        {
            UserPrompt = "Test prompt",
            MaxReplyCharacters = 100,
            Temperature = 0.7,
            MaxTokens = 200,
            ApplicationId = Guid.NewGuid(),
            SystemRole = "system",
            Model = ModelType.Gpt4,
        };
        optionsMock.Setup(o => o.Value).Returns(openAIOptions);

        var chatResult = new ChatResult { Id = "chat-id" };
        openAiApiMock.Setup(api => api.Chat.CreateChatCompletionAsync(It.IsAny<ChatRequest>())).ReturnsAsync(chatResult);

        var client = new OpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);

        var propertyDetailRequest = new PropertyDetailRequest { PropertyType = "TestProperty" };

        // Act
        var result = await client.CreateChatCompletionAsync(propertyDetailRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected: string.Empty, actual: result.Description);
    }

    [Fact]
    public async Task CreateChatCompletionAsync_LogsAppropriateMessages()
    {
        // Arrange
        var openAiApiMock = new Mock<IOpenAIAPI>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();

        var openAIOptions = new OpenAIOptions
        {
            UserPrompt = "Test prompt",
            MaxReplyCharacters = 100,
            Temperature = 0.7,
            MaxTokens = 200,
            ApplicationId = Guid.NewGuid(),
            SystemRole = "system",
            Model = ModelType.Gpt4,
        };
        optionsMock.Setup(o => o.Value).Returns(openAIOptions);

        var chatResult = new ChatResult { Id = "chat-id" };
        openAiApiMock.Setup(api => api.Chat.CreateChatCompletionAsync(It.IsAny<ChatRequest>())).ReturnsAsync(chatResult);

        var client = new OpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);

        var propertyDetailRequest = new PropertyDetailRequest { PropertyType = "TestProperty" };

        // Act
        await client.CreateChatCompletionAsync(propertyDetailRequest);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Calling Open AI from application")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Reply")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}
