using Receiver.DataContact;
using WinSettingManager.Lib.LogonSession;
using static Receiver.DataContact.LogonSessionDataContact;

namespace Receiver.Functions
{
    public class LogonSessionMethods
    {
        public static async Task<LogonSessionDataContact> GetLogonSessionsAsync()
        {
            return await Task.Run(() =>
            {
                return new LogonSessionDataContact()
                {
                    LoggedOnSessions = WinSettingManager.Lib.LogonSession.UserLogonSession.GetLoggedOnSession().
                        Select(s => new LogonSessionDataContact.UserLogonSession()
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

        public static async Task<LogonSessionDataContact> SetLogonSessionsAsync(LogonSessionDataContact contact)
        {
            switch(contact.Action)
            {
                case LogonSessionAction.Disconnect:
                    break;
                case LogonSessionAction.Logoff:
                    break;
                case LogonSessionAction.Get:
                case LogonSessionAction.None:
                default:
                    break;
            }

            return await GetLogonSessionsAsync();
        }
    }
}