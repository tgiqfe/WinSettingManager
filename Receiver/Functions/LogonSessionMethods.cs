using Receiver.DataContact;
using WinSettingManager.Lib.LogonSession;

namespace Receiver.Functions
{
    public class LogonSessionMethods
    {
        public static async Task<DataContactLogonSession> GetLogonSessions()
        {
            return await Task.Run(() =>
            {
                return new DataContactLogonSession()
                {
                    LoggedOnSessions = UserLogonSession.GetLoggedOnSession().
                        Select(s => new DataContactLogonSession.UserLogonSession_ex()
                    {
                        UserName = s.UserName,
                        UserDomain = s.UserDomain,
                        SessionID = s.SessionID,
                        SessionType = s.SessionType,
                        SessionState = s.SessionState,
                        ProtocolType = s.ProtocolType,
                        LogonTime = s.LogonTime
                    }).ToArray()
                };
            });
        }
    }
}
