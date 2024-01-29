using Post_Sender_bot.Context;

namespace Post_Sender_bot.TelegramBot.Controllers;

public class UserController : ControllerBase
{
    public UserController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
    }

    protected override Task HandleAction(ControllerContext context)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleUpdate(ControllerContext context)
    {
        throw new NotImplementedException();
    }

    public async Task Index(ControllerContext context)
    {
    }
}