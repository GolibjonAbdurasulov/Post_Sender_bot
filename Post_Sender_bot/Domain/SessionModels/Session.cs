using AttendanceControlBot.Domain.SessionModels;
using Post_Sender_bot.Domain.Entitys;

namespace Post_Sender_bot.Domain.SessionModels;

public class Session
{
    public long Id { get; set; }
    public string Action { get; set; }
    public string Controller { get; set; }
    public long ChatId { get; set; }
    public LoginSessionModel LoginData { get; set; }
    public long ChannelId { get; set; }
    public string? InlineResultQueryId { get; set; }
    public User User { get; set; }
}