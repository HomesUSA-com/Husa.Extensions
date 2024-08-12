namespace Husa.Extensions.OpenAI;

using System.Threading.Tasks;
using Husa.Extensions.OpenAI.Models;

public interface IOpenAIClient
{
    Task<PromptResponse> CreateChatCompletionAsync(PropertyDetailRequest propertyDetails);
}
