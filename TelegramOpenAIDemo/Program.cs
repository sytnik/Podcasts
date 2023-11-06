using System.Text.Json;
using TeamMagnetix.Dal.Dto;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
OpenAiKey = builder.Configuration["OpenAiKey"];
OpenAiModel = builder.Configuration["OpenAiModel"];
var botClient = new TelegramBotClient(builder.Configuration["TelegramKey"]!);
ReceiverOptions receiverOptions = new() { AllowedUpdates = Array.Empty<UpdateType>() };
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions
);

await app.RunAsync();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message) return;
    if (message.Text is not { } messageText) return;
    var chatId = message.Chat.Id;
    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    if (!messageText.Contains(ParseCandidateCvKeyword))
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
    else
    {
        var prompt = GetCandidateCvPrompt(messageText);
        var result = await GetDtoFromOpenAi<CandidateDto>(prompt);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: JsonSerializer.Serialize(result),
            cancellationToken: cancellationToken);
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}