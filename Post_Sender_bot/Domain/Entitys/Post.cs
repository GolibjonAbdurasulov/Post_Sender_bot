using System.ComponentModel.DataAnnotations.Schema;
using Telegram.Bot.Types;

namespace Post_Sender_bot.Domain.Entitys;
[Table("posts")]
public class Post : BaseEntity
{
    [Column("owner_id")] public long OwnerId { get; set; }
    [Column("channel_id")] public long ChannelId { get; set; }
    [Column("sending_time")]public DateTime SendingTime { get; set; }
    [Column("message")] public Message Message { get; set; }
}