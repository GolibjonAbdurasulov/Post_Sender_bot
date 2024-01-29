using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Sender_bot.Domain.Entitys;
[Table("users")]
public class User : BaseEntity
{
    [Column("full_name")] public string FullName { get; set; }
    [Column("phone_number")] public string PhoneNumber { get; set; }
    [Column("password")] public string Password { get; set; }
    [Column("telegram_chat_id")] public long TelegramChatId { get; set; }
    [Column("signed")] public bool Signed { get; set; }
    [Column("last_login_date")] public DateTime LastLoginDate { get; set; }
  
}