using Telegram.Bot.Types;

namespace Post_Sender_bot.Context;

public class BaseContext
{
    public Update Update { get; set; }
    public Message Message => Update?.Message;
}