using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Sender_bot.Domain.Entitys;

public class BaseEntity
{
    
    [Column("Id")]
    public long Id { get; set; }
}