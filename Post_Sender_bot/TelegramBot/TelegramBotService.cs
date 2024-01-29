using AttendanceControlBot.Configuration;
using AttendanceControlBot.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Post_Sender_bot.Context;
using Post_Sender_bot.Extensions;
using Post_Sender_bot.Infrastructure;
using Post_Sender_bot.Infrastructure.Repositories;
using Post_Sender_bot.Services;
using Post_Sender_bot.TelegramBot.Controllers;
using Post_Sender_bot.TelegramBot.SimpleSessionManager;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Post_Sender_bot.TelegramBot;

public class TelegramBotService
{
    private UserRepository UserRepository;
    public static TelegramBotClient? _client { get; set; }
    private List<Func<ControllerContext, CancellationToken, Task>> updateHandlers { get; set; }
    private DataContext _dataContext;

    private ControllerManager.ControllerManager ControllerManager;
    private AuthService _authService;
    private SessionManager SessionManager;

    public TelegramBotService()
    {
        _client = new TelegramBotClient(Settings.botToken);
        this.updateHandlers = new List<Func<ControllerContext, CancellationToken, Task>>();
        UserRepository = new UserRepository(_dataContext);
        _dataContext = new DataContext();
        _authService = new AuthService(repository: UserRepository);
        SessionManager = new SessionManager();
        ControllerManager =
            new ControllerManager.ControllerManager(_authService, SessionManager, repository: UserRepository);
    }


    public async Task Start()
    {
        //Session handler
        this.updateHandlers.Add(async (context, token) =>
        {
            long? chatId = context.GetChatIdFromUpdate();

            if (chatId is null)
                throw new Exception("Chat id not found to find session");

            var session = await SessionManager.GetSessionByChatId(chatId.Value);
            context.Session = session;
            context.TerminateSession = async () => await this.SessionManager.TerminateSession(context.Session);
        });

        //Log handler
        this.updateHandlers.Add(async (context, token) =>
        {
            Console.WriteLine("Log -> {0} | {1} | {2}", DateTime.Now, context.Session.ChatId,
                context.Update.Message?.Text ?? context.Update.Message?.Caption);
        });


        this.updateHandlers.Add(async (context, token) =>
        {
            var signedUser = await UserRepository
                .GetAll()
                .FirstOrDefaultAsync(user =>
                    user.Signed
                    && user.TelegramChatId == context.GetChatIdFromUpdate());
            if (signedUser is not null)
            {
                context.Session.User = signedUser;
                context.Session.User.TelegramChatId = signedUser.TelegramChatId;
            }
        });

        //Check for auth
        List<string> authRequiredControllers = new List<string>()
        {
            nameof(UserController)
        };

        this.updateHandlers.Add(async (context, token) =>
        {
            if (context.Session is not null)
            {
                if (context.Session.User.TelegramChatId > 0)
                {
                    string controller = context.Session.Controller ?? nameof(HomeController);
                    if (nameof(HomeController) == controller)
                    {
                        //|| controller == nameof(AuthController)
                        context.Session.Controller = nameof(HomeController);
                        context.Session.Action = nameof(HomeController);
                    }
                }
                else if (authRequiredControllers.Contains(context.Session.Controller))
                {
                    await context.SendErrorMessage("Unauthorized", 401);
                    context.Session.Controller = null;
                    context.Session.Action = null;
                }

                ;
            }
        });


        this.updateHandlers.Insert(this.updateHandlers.Count,
            async (context, token) => { await context.Forward(this.ControllerManager); });

        await StartReceiver();
    }


    private async Task StartReceiver()
    {
        var cancellationToken = new CancellationToken();
        var options = new ReceiverOptions();
        _client.StartReceiving(OnUpdate, ErrorMessage, options, CancellationToken.None);
        Console.WriteLine("{0} | Bot is starting...", DateTime.Now);
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, args) => { cts.Cancel(); };

        while (!cts.IsCancellationRequested)
        {

        }
    }

    private async Task OnUpdate(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        ControllerContext context = new ControllerContext()
        {
            Update = update
        };

        try
        {
            foreach (var updateHandler in this.updateHandlers)
                await updateHandler(context, token);
        }
        catch (Exception e)
        {
            //context.Reset();

            string errorMessage = ("Handler Error: " + e.Message
                                                     + "\nInner exception message: "
                                                     + e.InnerException?.Message
                    // + "\nStack trace: " + e.StackTrace
                );
            Console.WriteLine(errorMessage + "\nStackTrace: " + e.StackTrace);
            if (e.GetType() == typeof(UserException))
            {
                context.Session.Controller = nameof(HomeController);
                context.Session.Action = nameof(HomeController.Index);
              await  context.SendBoldTextMessage(e.Message);
            }
            else
             await context.SendErrorMessage(errorMessage, 500);
        }
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
}
    
   