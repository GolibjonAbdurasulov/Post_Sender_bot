using Post_Sender_bot.Context;
using Post_Sender_bot.Extensions;
using Telegram.Bot.Types.Enums;

namespace Post_Sender_bot.TelegramBot.Controllers;

public class HomeController : ControllerBase
{
    
    public HomeController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
        
    }
    public async Task Index(ControllerContext context)
    {
        await context.SendBoldTextMessage(
            "Assalomu alaykum 40-maktabning davomatni nazorat qiluvchi botga xush kelibsiz!!!",
            replyMarkup: context.HomeControllerIndexButtons());
    }
    
    protected override async Task HandleAction(ControllerContext context)
    {
        // if (context.Session.Action == nameof(this.Index))
        //     await this.Index(context);
        await this.Index(context);
    }

    protected override async Task HandleUpdate(ControllerContext context)
    {
        //Check commands
        if (message!.Type == MessageType.Text)
        {
            var text = message.Text;
            if (text is not null)
                switch (text)
                {
                    case "/start":
                        context.Session.Action = nameof(Index);
                        break;
                    case "Login":
                        context.Session.Controller = nameof(AuthController);
                        context.Session.Action = nameof(AuthController.LoginUserStart);
                        break;
                   case "Bot haqida":
                        context.Session.Controller = nameof(AboutTheBotController);
                        context.Session.Action = nameof(AboutTheBotController.Index);
                        break;
                }
        }
    }


}