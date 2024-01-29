using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Infrastructure.Repositories;

namespace Post_Sender_bot.Services;

public class SettingsService
{
    private UserService UserService{ get; set;}

    public SettingsService(UserService userService)
    {
        UserService = userService;
    }

    public async Task<User> ChangePassword(User user)
    {
        return await UserService.UpdateAsync(user);
    }
    
    public async Task<User> ChangePhoneNumber(User user)
    {
        return await UserService.UpdateAsync(user);
    }
}