using Post_Sender_bot.Domain.SessionModels;
using TCBApp.TelegramBot.Managers;

namespace Post_Sender_bot.TelegramBot.SimpleSessionManager;

public class SessionManager : ISessionManager<Session>
{
    private List<Session> sessions = new List<Session>();

    public SessionManager()
    {
    }

    public async Task<Session> GetSessionByChatId(long chatId)
    {
        var lastSession = sessions.Find(x => x.ChatId == chatId);
        if (lastSession is null)
        {
            var session = new Session()
            {
                Action = null,
                Controller = null,
                Id = 0,
                ChatId = chatId
            };
            sessions.Add(session);
            return session;
        }

        return lastSession;
    }

    public async Task<Session> GetSessionByWorkerTelegramChatId(long chatId)
    {
        return this.sessions
            .FirstOrDefault(x => x.User.TelegramChatId == chatId);
    }

    public async Task TerminateSession(Session session)
    {
        this.sessions.Remove(session);
    }

}