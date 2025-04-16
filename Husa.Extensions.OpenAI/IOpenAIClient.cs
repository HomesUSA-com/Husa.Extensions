namespace Husa.Extensions.OpenAI;

using System.Collections.Generic;
using System.Threading.Tasks;
using Husa.Extensions.OpenAI.Models;

public interface IOpenAIClient
{
    Task<PromptResponse> CreateChatCompletionAsync(PropertyDetailRequest propertyDetails);
    Task<ICollection<LocatePeopleAndBrandsResponse>> LocatePeopleAndBrandsFromImagesAsync(IEnumerable<LocatePeopleAndBrandsRequest> images);
}
