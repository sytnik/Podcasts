using System.Diagnostics;
using System.Text.Json;
using Azure.AI.OpenAI;

namespace TelegramOpenAIDemo.Logic;

public static class OpenAi
{
    public static string GetCandidateCvPrompt(string candidateCv) =>
        $"convert plain text candidate cv here '{candidateCv}' to the format" +
        $"'{CandidateDtoJsonSchema}'" +
        $", translate to english if needed," +
        $" extract all skills, return only json";

    // public static string GetVacancyPrompt(string vacancyDetails) =>
    //     $"convert plaintext vacancy details of {vacancyDetails} to the format '{VacancyDtoJsonSchema}' " +
    //     $"extract all skills, return only json";
    //
    // public static string GetCandidateMatchPrompt(string candidate, string vacancy) =>
    //     $"analyze the match between candidate '{candidate}' and vacancy '{vacancy}', provide the result" +
    //     $"in the format '{CandidateVacancyDtoJsonSchema}', return only json";

    public static async Task<T> GetDtoFromOpenAi<T>(string prompt, string model = "gpt-3.5-turbo")
    {
        var client = new OpenAIClient(OpenAiKey);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await client.GetChatCompletionsAsync(model,
            new ChatCompletionsOptions {Messages = {new ChatMessage(ChatRole.User, prompt)}});
        stopwatch.Stop();
        var elapsed = stopwatch.ElapsedMilliseconds;
        var messageContent = result.Value.Choices.ElementAtOrDefault(0)?.Message?.Content;
        return JsonSerializer.Deserialize<T>(messageContent ?? "");
    }
}