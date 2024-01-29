using Post_Sender_bot.Context;
using Post_Sender_bot.Extensions;
using Post_Sender_bot.Services;
using Telegram.Bot.Types.Enums;

namespace Post_Sender_bot.TelegramBot.Controllers;

public class SettingsController : ControllerBase
{
    protected readonly SettingsService SettingsService;
    public SettingsController(ControllerManager.ControllerManager controllerManager, SettingsService settingsService) : base(controllerManager)
    {
        SettingsService = settingsService;
    }

    protected override async Task HandleAction(ControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await Index(context);
                break;
            case nameof(CheckPassword):
                await CheckPassword(context);
                break;
            case nameof(ChangePasswordStart):
                await ChangePasswordStart(context);
                break;
            case nameof(UpdatePassword):
                await UpdatePassword(context);
                break;
        }
    }

    public async Task Index(ControllerContext context)
    {
        await context.SendBoldTextMessage("Tanlang",
            replyMarkup: context.MakeSettingsControllerIndexButtonsReplyKeyboardMarkup());
    }

    protected override async Task HandleUpdate(ControllerContext context)
    {
        if (context.Message?.Type is MessageType.Text)
        {
            string text = context.Message?.Text;
            switch (text)
            {
                case "Parolni o'zgartirish":
                    context.Session.Action = nameof(ChangePasswordStart);
                    break;
                case "Ortga":
                    context.Session.Controller = nameof(UserController);
                   context.Session.Action = nameof(UserController.Index);
                    break;
            }
        }
    }

    protected async Task ChangePasswordStart(ControllerContext context)
    {
        await context.SendBoldTextMessage("Xozirgi parolingizni kiriting");
        context.Session.Action = nameof(CheckPassword);
    }
    protected async Task CheckPassword(ControllerContext context)
    {
        string oldPassword = context.Message.Text;
        if (oldPassword is not null && context.Session.User.Password==oldPassword)
        {
            await context.SendBoldTextMessage("Yangi parolingizni kiriting \nParol kamida 6 ta belgidan iborat bo'lsin");
            context.Session.Action = nameof(UpdatePassword);
        }
        else
        {
            await context.SendBoldTextMessage("Parol noto'g'ri \nParolni to'g'ri kiritganingizni tekshiring");
        }
    }
    protected async Task UpdatePassword(ControllerContext context)
    {
        string newPassword = context.Message.Text;
        if (newPassword is not null&&newPassword.Length>=6)
        {
            context.Session.User.Password = newPassword;
            await SettingsService.ChangePassword(context.Session.User);
            await context.SendBoldTextMessage("Parol muvaffaqiyatli o'zgartirildi");
            context.Session.Action = nameof(Index);
            await context.SendBoldTextMessage("Tanlang",
                replyMarkup: context.MakeSettingsControllerIndexButtonsReplyKeyboardMarkup());
        }
        else
        {
            await context.SendBoldTextMessage("Parol noto'g'ri\nParol 6ta belgidan iborat bo'lishi kerak");
        }
    }
}