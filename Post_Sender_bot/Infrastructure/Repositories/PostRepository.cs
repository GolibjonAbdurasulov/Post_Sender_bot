using Post_Sender_bot.Domain.Entitys;

namespace Post_Sender_bot.Infrastructure.Repositories;

public class PostRepository : RepositoryBase<Post>
{
    public PostRepository(DataContext context) : base(context)
    {
    }
}