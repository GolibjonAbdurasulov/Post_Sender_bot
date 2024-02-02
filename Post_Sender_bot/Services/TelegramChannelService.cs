using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Infrastructure.Repositories;

namespace Post_Sender_bot.Services;

public class TelegramChannelService : BaseService<TelegramChannel>
{
    public TelegramChannelService(RepositoryBase<TelegramChannel> repository) : base(repository)
    {
        
    }

    public async Task<List<TelegramChannel>> GetChannelsByOwnerId(long ownerId)
    {
        return _repository.GetAll().Where(channel => channel.OwnerId == ownerId).ToList();
    }
}