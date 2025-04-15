namespace Husa.Extensions.OpenAI.Configuration;

using global::OpenAI.Chat;
using Husa.Extensions.Common;
using Husa.Extensions.OpenAI.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            var model = openAIOptions.Value.Model.ToStringFromEnumMember();
            var chatClient = new ChatClient(model, new(openAIOptions.Value.ApiKey), new()
            {
                OrganizationId = openAIOptions.Value.OrganizationId,
            });

            return new OpenAIClient(chatClient, openAIOptions, logger);
        });

        return services;
    }
}
