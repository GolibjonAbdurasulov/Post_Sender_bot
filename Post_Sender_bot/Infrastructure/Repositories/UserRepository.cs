using Post_Sender_bot.Domain.Entitys;

namespace Post_Sender_bot.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}