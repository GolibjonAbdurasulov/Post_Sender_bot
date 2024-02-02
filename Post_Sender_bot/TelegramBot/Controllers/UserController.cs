using Post_Sender_bot.Context;
using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Extensions;
using Post_Sender_bot.Services;
using Telegram.Bot.Types.Enums;

namespace Post_Sender_bot.TelegramBot.Controllers;

public class UserController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly PostService _postService;
    private readonly TelegramChannelService _telegramChannelService;
    public UserController(ControllerManager.ControllerManager controllerManager, AuthService authService, PostService postService, TelegramChannelService telegramChannelService) : base(controllerManager)
    {
        _authService = authService;
        _postService = postService;
        _telegramChannelService = telegramChannelService;
    }

    protected override async Task HandleAction(ControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await Index(context);
                break;
            case nameof(LogOut):
                await LogOut(context);
                break;
            case nameof(AddPostStart):
                await AddPostStart(context);
                break;
        }
    }
    
    protected override async Task HandleUpdate(ControllerContext context)
    {
        if (context.Message?.Type is MessageType.Text)
        {
            string text = context.Message?.Text;
            switch (text)
            {
                case "Po'st qo'shish":
                    context.Session.Action = nameof(AddPostStart);
                    break;
                // case "Postlar ro'yxatini olish":
                //     context.Session.Action = nameof(ChangePasswordStart);
                //     break;
                // case "Postni tahrirlash":
                //     context.Session.Action = nameof(ChangePasswordStart);
                //     break;
                case "Sozlamalar⚙️":
                    context.Session.Controller = nameof(SettingsController);
                    context.Session.Action = nameof(SettingsController.Index);
                    break;
                case "Log out":
                    context.Session.Action = nameof(LogOut);
                    break;
            }
        }
    }

    public async Task Index(ControllerContext context)
    {
       await context.SendBoldTextMessage("Tanlang: ",context.MakeUserDashboardReplyKeyboardMarkup());
    }
    
    public async Task LogOut(ControllerContext context)
    {
        await _authService.Logout(context.Session.User.Id);
        await context.TerminateSession();
        await context.SendBoldTextMessage("Logged out", replyMarkup: context.HomeControllerIndexButtons());
    }

    public async Task AddPostStart(ControllerContext context)
    {
        await context.SendBoldTextMessage("Postni yuboring:",context.Back());
        
    }

    public async Task AddPost(ControllerContext context)
    {
        Post post = new Post
        {
            OwnerId = context.Message.Chat.Id,
            ChannelId = context.Session.ChannelId,
            SendingTime = default,
            Message = context.Message
        };
       await _postService.CreateAsync(post);
    }
    
}