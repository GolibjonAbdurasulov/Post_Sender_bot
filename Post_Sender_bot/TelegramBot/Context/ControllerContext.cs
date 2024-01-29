using Post_Sender_bot.Domain.SessionModels;

namespace Post_Sender_bot.Context;

public class ControllerContext:BaseContext
{
    public Func<Task> TerminateSession { get; set; }
    public Session Session { get; set; }
}