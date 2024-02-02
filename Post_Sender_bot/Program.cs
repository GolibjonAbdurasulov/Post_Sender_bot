// See https://aka.ms/new-console-template for more information

using Post_Sender_bot.TelegramBot;

TelegramBotService botService = new TelegramBotService();
await botService.Start();
