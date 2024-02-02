using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Sender_bot.Domain.Entitys;
[Table("telegram_channels")]
public class TelegramChannel : BaseEntity
{
   [Column("channel_name")] public long ChannelName { get; set; }
   [Column("owner_id")] public long OwnerId { get; set; }
   [Column("channel_chat_id")]public long ChannelChatId { get; set; }
}