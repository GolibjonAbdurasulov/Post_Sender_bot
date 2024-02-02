using Post_Sender_bot.Domain.Entitys;

namespace Post_Sender_bot.Infrastructure.Repositories;

public class ChannelRepository : RepositoryBase<TelegramChannel>
{
    public ChannelRepository(DataContext context) : base(context)
    {
    }
}