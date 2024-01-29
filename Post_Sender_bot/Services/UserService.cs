using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Infrastructure.Repositories;

namespace Post_Sender_bot.Services;

public class UserService : BaseService<User>
{
    public UserService(RepositoryBase<User> repository) : base(repository)
    {
    }
}