namespace Husa.Extensions.OpenAI.Configuration;

using Husa.Extensions.OpenAI.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI_API;

public static class OpenAIBoostrapper
{
    public static IServiceCollection ConfigureOpenAiApiClient(this IServiceCollection services)
    {
        services
            .AddOptions<OpenAIOptions>()
            .BindConfiguration(configSectionPath: OpenAIOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IOpenAIClient, OpenAIClient>(serviceProvider =>
        {
            var openAIOptions = serviceProvider.GetRequiredService<IOptions<OpenAIOptions>>();
            var logger = serviceProvider.GetRequiredService<ILogger<OpenAIClient>>();
            var openAiApi = new OpenAIAPI(new APIAuthentication(openAIOptions.Value.ApiKey, openAIOptions.Value.OrganizationId));

            return new OpenAIClient(openAiApi, openAIOptions, logger);
        });

        return services;
    }
}
