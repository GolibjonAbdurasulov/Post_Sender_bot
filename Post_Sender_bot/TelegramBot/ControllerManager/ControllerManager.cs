using Post_Sender_bot.Domain.SessionModels;
using Post_Sender_bot.Infrastructure;
using Post_Sender_bot.Infrastructure.Repositories;
using Post_Sender_bot.Services;
using Post_Sender_bot.TelegramBot.Controllers;
using Post_Sender_bot.TelegramBot.SimpleSessionManager;

namespace Post_Sender_bot.TelegramBot.ControllerManager;

public class ControllerManager
{
 
    private readonly AuthService _authService;
    private readonly SettingsService _settingsService;
    private readonly AuthController _authController;
    private readonly SettingsController _settingsController;

    private HomeController _homeController;
    public ControllerManager(AuthService authService,
        SessionManager sessionManager, SettingsService settingsService)
    {
        _settingsService = settingsService;
        _authService = authService;
        _authController = new AuthController(this, _authService);
        _homeController = new HomeController(this);
        this._settingsController = new SettingsController(this,_settingsService);
        
    }

    // public ControllerManager(DataContext dataContext, StudentsDepartmentController studentsDepartmentController)
    // {
    //     _dataContext = dataContext;
    //     this.studentsDepartmentController = studentsDepartmentController;
    // }

    public ControllerBase GetControllerBySessionData(Session session)
    {
        switch (session.Controller)
        {
            case nameof(HomeController):
                return this._homeController;
             case nameof(AuthController):
                 return this._authController;
             case nameof(SettingsController):
                 return this._settingsController;
        }
        return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}