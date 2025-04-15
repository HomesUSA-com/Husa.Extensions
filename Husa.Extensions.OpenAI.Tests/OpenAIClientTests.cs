namespace Husa.Extensions.OpenAI.Tests;

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using global::OpenAI.Chat;
using Husa.Extensions.OpenAI.Enums;
using Husa.Extensions.OpenAI.Models;
using Husa.Extensions.OpenAI.Options;
using Husa.Extensions.OpenAI.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

public class OpenAIClientTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenDependenciesAreNull()
    {
        // Arrange
        var openAiApiMock = new Mock<ChatClient>();
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
        var openAiApiMock = new Mock<ChatClient>();
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

        var mockPipelineResponse = new Mock<PipelineResponse>();
        var chatResult = ClientResult<ChatCompletion>.FromOptionalValue<ChatCompletion>(
            null, mockPipelineResponse.Object);
        openAiApiMock.Setup(api => api.CompleteChatAsync(
            It.IsAny<IEnumerable<ChatMessage>>(),
            It.IsAny<ChatCompletionOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(chatResult);

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
        var openAiApiMock = new Mock<ChatClient>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();
        var client = new OpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);

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

        var mockPipelineResponse = new Mock<PipelineResponse>();
        var chatResult = ClientResult<ChatCompletion>.FromOptionalValue<ChatCompletion>(
            null, mockPipelineResponse.Object);
        openAiApiMock.Setup(api => api.CompleteChatAsync(
            It.IsAny<IEnumerable<ChatMessage>>(),
            It.IsAny<ChatCompletionOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(chatResult);

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

    [Fact]
    public async Task LocatePeopleAndBrandsFromImagesAsyncWhenOpenAIApiFail()
    {
        // Arrange
        var openAiApiMock = new Mock<ChatClient>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();
        var client = new OpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);
        var images = Array.Empty<LocatePeopleAndBrandsRequest>().Append(new()
        {
            ImageName = Faker.Lorem.GetFirstWord(),
            ImageUrl = Faker.Internet.Url(),
        });

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

        openAiApiMock.Setup(api => api.CompleteChatAsync(
            It.IsAny<IEnumerable<ChatMessage>>(),
            It.IsAny<ChatCompletionOptions>(),
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await client.LocatePeopleAndBrandsFromImagesAsync(images);

        Assert.Null(result);
    }

    [Fact]
    public async Task LocatePeopleAndBrandsFromImagesAsyncWhenNoData()
    {
        // Arrange
        var openAiApiMock = new Mock<ChatClient>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();
        var client = new OpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);
        var images = Array.Empty<LocatePeopleAndBrandsRequest>();

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

        var mockPipelineResponse = new Mock<PipelineResponse>();
        var chatResult = ClientResult<ChatCompletion>.FromOptionalValue<ChatCompletion>(
            null, mockPipelineResponse.Object);
        openAiApiMock.Setup(api => api.CompleteChatAsync(
            It.IsAny<IEnumerable<ChatMessage>>(),
            It.IsAny<ChatCompletionOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(chatResult);

        // Act
        var result = await client.LocatePeopleAndBrandsFromImagesAsync(images);

        Assert.Empty(result);
    }

    [Fact]
    public async Task LocatePeopleAndBrandsFromImagesAsync_Success()
    {
        // Arrange
        var openAiApiMock = new Mock<ChatClient>();
        var optionsMock = new Mock<IOptions<OpenAIOptions>>();
        var loggerMock = new Mock<ILogger<OpenAIClient>>();
        var client = new TestOpenAIClient(openAiApiMock.Object, optionsMock.Object, loggerMock.Object);
        var images = Array.Empty<LocatePeopleAndBrandsRequest>().Append(new()
        {
            ImageName = Faker.Lorem.GetFirstWord(),
            ImageUrl = Faker.Internet.Url(),
        }).Append(new()
        {
            ImageName = Faker.Lorem.GetFirstWord(),
            ImageUrl = Faker.Internet.Url(),
        });
        var expectedResponse = new List<LocatePeopleAndBrandsResponse>
        {
            new()
            {
                ContainsBranding = true,
                ContainsPersons = false,
                Description = "This image contains branding.",
            },
            new()
            {
                ContainsBranding = false,
                ContainsPersons = true,
                Description = "This image contains persons.",
            },
        };

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

        var mockPipelineResponse = new Mock<PipelineResponse>();
        var chatResult = ClientResult<ChatCompletion>.FromOptionalValue<ChatCompletion>(
            null, mockPipelineResponse.Object);
        openAiApiMock.Setup(api => api.CompleteChatAsync(
            It.IsAny<IEnumerable<ChatMessage>>(),
            It.IsAny<ChatCompletionOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(chatResult);

        // Act
        var result = await client.LocatePeopleAndBrandsFromImagesAsync(images);

        Assert.NotEmpty(result);
        Assert.All(result, (item, index) =>
        {
            Assert.Equal(expected: expectedResponse[index].ContainsBranding, actual: item.ContainsBranding);
            Assert.Equal(expected: expectedResponse[index].ContainsPersons, actual: item.ContainsPersons);
            Assert.Equal(expected: expectedResponse[index].Description, actual: item.Description);
        });
    }
}
