using Post_Sender_bot.Domain.SessionModels;
using Post_Sender_bot.Infrastructure;
using Post_Sender_bot.Infrastructure.Repositories;
using Post_Sender_bot.Services;
using Post_Sender_bot.TelegramBot.Controllers;
using Post_Sender_bot.TelegramBot.SimpleSessionManager;

namespace Post_Sender_bot.TelegramBot.ControllerManager;

public class ControllerManager
{
    private readonly SessionManager _sessionManager;
    private DataContext _dataContext;

    private UserRepository _userRepository;
    
    //private readonly SettingsController _settingsController;

    private HomeController _homeController;
    public ControllerManager(AuthService authService,
        SessionManager sessionManager, UserRepository repository)
    {
        _userRepository = repository;
        _homeController = new HomeController(this);
        _sessionManager = sessionManager;
               this._dataContext = new DataContext();
        //this._settingsController = new SettingsController(this,clientDataService, boardService);
        //_clientInfoController = new ClientInfoController(this,clientDataService,_clientService);


        // this._authController = new AuthController(botClient, new AuthService(dataService));
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
            // case nameof(HomeController):
            //     return this._homeController;
            // case nameof(AuthController):
            //     return this._authController;
            // case nameof(SettingsController):
            //     return this._settingsController;
        }
        return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}