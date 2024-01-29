using AttendanceControlBot.Domain.Dtos.AuthDtos;
using AttendanceControlBot.Domain.Exceptions;
using Post_Sender_bot.Domain.Entitys;
using Post_Sender_bot.Infrastructure.Repositories;

namespace Post_Sender_bot.Services;

public class AuthService 
{
    private UserRepository _userRepository; 

    public AuthService( UserRepository repository)
    {
        this._userRepository = repository;
    }
   
    // public async Task RegisterUser(UserRegistrationDto userRegistration)
    // {
    //
    //     var oldUser  = await this.repository.GetAll()
    //         .FirstOrDefaultAsync(x => x.PhoneNumber == userRegistration.PhoneNumber);
    //
    //     if (oldUser is User)
    //         throw new Exception("User already exists");
    //     
    //     var insertedUser = await this.repository.AddAsync(new Worker
    //     {
    //         PhoneNumber = userRegistration.PhoneNumber,
    //         Password = userRegistration.Password,
    //         TelegramChatId = userRegistration.ChatId,
    //         Signed = false,
    //         LastLogindate = DateTime.Now,
    //         Role = Role.Teacher
    //     });
    //     
    //     if (insertedUser is null)
    //         throw new Exception("Unable to insert user");
    //     var client = new Client()
    //     {
    //         UserId = insertedUser.Id, // Updated line
    //         Status = ClientStatus.Enabled,
    //         IsPremium = false,
    //         UserName = string.Empty,
    //         Nickname = string.Empty
    //     };
    //
    //     var insertedClient = await this._clientDataService.AddAsync(client);
    //
    //     if (insertedClient is null)
    //         throw new Exception("Unable to add new client");
    // }

    public async Task<User?> Login(UserLoginDto user)
    {
        var userInfo = _userRepository.GetAll()
            .FirstOrDefault(item => 
                item.Password == user.Password
                && item.PhoneNumber == user.Login);

        if (userInfo is null)
        {
            throw new UserException("Foydalanuvchi topilmadi");
        }

        if (userInfo is  User)
        {
             userInfo.Signed = true;
             userInfo.LastLoginDate = DateTime.Now;
        }

        if (userInfo.TelegramChatId==0)
            userInfo.TelegramChatId = user.TelegramChatId;

        await _userRepository.UpdateAsync(userInfo);
        return userInfo;
    }

    public async Task Logout(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new UserException("Foydalanuvchi topilmadi");

        user.Signed = false;
        await _userRepository.UpdateAsync(user);
    }
}