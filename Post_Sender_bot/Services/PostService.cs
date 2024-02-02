using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Infrastructure.Repositories;

namespace Post_Sender_bot.Services;

public class PostService : BaseService<Post>
{
    public PostService(PostRepository repository) : base(repository)
    {
    }

    //bitta userga tegishli barcha postlarni qaytaradi
    public async Task<List<Post>> GetPostsByOwnerId(long ownerId)
    {
        return _repository.GetAll().Where(post => post.OwnerId == ownerId).ToList();
    }
    
    //bitta kanalga jonatilishi kerak bolgan barcha postlarni qaytaridi
    public async Task<List<Post>> GetPostsByChannelId(long channelId)
    {
        return _repository.GetAll().Where(post => post.ChannelId == channelId).ToList();
    }

    //yuborilish vaqti kelgan postlarni yuborish uchun tekshirish
    public async Task<List<Post>> CheckPostSendTime()
    {
        return _repository.GetAll().Where(post => post.SendingTime == DateTime.Now).ToList();
    }
}