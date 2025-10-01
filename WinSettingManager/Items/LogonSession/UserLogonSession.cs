using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//  [WTS_INFO_CLASS]
//  https://learn.microsoft.com/ja-jp/windows/win32/api/wtsapi32/ne-wtsapi32-wts_info_class
//  [WTSINFOA]
//  https://learn.microsoft.com/en-us/windows/win32/api/wtsapi32/ns-wtsapi32-wtsinfoa

namespace WinSettingManager.Items.LogonSession
{
    public class UserLogonSession
    {
        #region Public Parameter

        public string UserName { get; set; }
        public string UserDomain { get; set; }
        public int SessionID { get; set; }
        public string SessionType { get; set; }
        public string SessionState { get; set; }
        public int ProtocolType { get; set; }
        public DateTime LogonTime { get; set; }

        #endregion

        /// <summary>
        /// Get all currently logged on sessions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserLogonSession> GetLoggedOnSession()
        {
            List<UserLogonSession> list = new List<UserLogonSession>();

            nint serverHandle = PInvoke.WTSOpenServer(Environment.MachineName);
            nint buffer = nint.Zero;
            int count = 0;
            int retVal = PInvoke.WTSEnumerateSessions(serverHandle, 0, 1, ref buffer, ref count);
            int dataSize = Marshal.SizeOf(typeof(PInvoke.WTS_SESSION_INFO));
            nint current = buffer;
            uint bytes = 0;

            if (retVal != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    PInvoke.WTS_SESSION_INFO si = (PInvoke.WTS_SESSION_INFO)Marshal.PtrToStructure(current, typeof(PInvoke.WTS_SESSION_INFO));
                    current += dataSize;

                    nint userNamePtr = nint.Zero;
                    nint domainNamePtr = nint.Zero;
                    nint sessionTypePtr = nint.Zero;
                    nint protocolTypePtr = nint.Zero;
                    nint wtsinfoPtr = nint.Zero;

                    PInvoke.WTSQuerySessionInformation(serverHandle, si.SessionID, PInvoke.WTS_INFO_CLASS.WTSUserName, out userNamePtr, out bytes);
                    PInvoke.WTSQuerySessionInformation(serverHandle, si.SessionID, PInvoke.WTS_INFO_CLASS.WTSDomainName, out domainNamePtr, out bytes);
                    PInvoke.WTSQuerySessionInformation(serverHandle, si.SessionID, PInvoke.WTS_INFO_CLASS.WTSWinStationName, out sessionTypePtr, out bytes);
                    PInvoke.WTSQuerySessionInformation(serverHandle, si.SessionID, PInvoke.WTS_INFO_CLASS.WTSClientProtocolType, out protocolTypePtr, out bytes);
                    PInvoke.WTSQuerySessionInformation(serverHandle, si.SessionID, PInvoke.WTS_INFO_CLASS.WTSSessionInfo, out wtsinfoPtr, out bytes);

                    var wtsinfo = (PInvoke.WTSINFOA)Marshal.PtrToStructure(wtsinfoPtr, typeof(PInvoke.WTSINFOA));
                    var userName = Marshal.PtrToStringAnsi(userNamePtr);
                    if (!string.IsNullOrEmpty(userName))
                    {
                        list.Add(new UserLogonSession()
                        {
                            UserName = userName,
                            UserDomain = Marshal.PtrToStringAnsi(domainNamePtr),
                            SessionID = si.SessionID,
                            SessionType = Marshal.PtrToStringAnsi(sessionTypePtr),
                            SessionState = si.State.ToString(),
                            ProtocolType = Marshal.ReadInt32(protocolTypePtr),
                            LogonTime = wtsinfo.LogonTime,
                        });
                    }

                    PInvoke.WTSFreeMemory(userNamePtr);
                    PInvoke.WTSFreeMemory(domainNamePtr);
                    PInvoke.WTSFreeMemory(sessionTypePtr);
                    PInvoke.WTSFreeMemory(protocolTypePtr);
                    PInvoke.WTSFreeMemory(wtsinfoPtr);
                }
            }
            PInvoke.WTSFreeMemory(buffer);
            PInvoke.WTSCloseServer(serverHandle);

            return list;
        }

        /// <summary>
        /// Disconnect the RDP connection
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (ProtocolType == 2)   //  ProtocolType 2: RDP
            {
                nint serverHandle = PInvoke.WTSOpenServer(Environment.MachineName);
                bool result = PInvoke.WTSDisconnectSession(serverHandle, SessionID, false);
                PInvoke.WTSCloseServer(serverHandle);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Disconnecting an RDP connection by specifying a username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userdomain"></param>
        /// <returns></returns>
        public static bool Disconnect(string username, string userdomain = null)
        {
            var sessions = GetLoggedOnSession();
            var session = sessions.
                FirstOrDefault(x => x.UserName == username && (userdomain == null || x.UserDomain == userdomain));
            return session?.Disconnect() ?? false;
        }

        /// <summary>
        /// Logoff
        /// </summary>
        /// <returns></returns>
        public bool Logoff()
        {
            nint serverHandle = PInvoke.WTSOpenServer(Environment.MachineName);
            bool result = PInvoke.WTSLogoffSession(serverHandle, SessionID, false);
            PInvoke.WTSCloseServer(serverHandle);
            return result;
        }

        /// <summary>
        /// Log off with a specified username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userdomain"></param>
        /// <returns></returns>
        public static bool Logoff(string username, string userdomain = null)
        {
            var sessions = GetLoggedOnSession();
            var session = sessions.
                FirstOrDefault(x => x.UserName == username && (userdomain == null || x.UserDomain == userdomain));
            return session?.Logoff() ?? false;
        }

        /// <summary>
        /// Returns whether the state is active
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return SessionState == PInvoke.WTS_CONNECTSTATE_CLASS.WTSActive.ToString();
        }

        /// <summary>
        /// Specify a user name and return whether it is active or not.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userdomain"></param>
        /// <returns></returns>
        public static bool IsActive(string username, string userdomain = null)
        {
            var sessions = GetLoggedOnSession();
            var session = sessions.
                FirstOrDefault(x => x.UserName == username && (userdomain == null || x.UserDomain == userdomain));
            return session?.IsActive() ?? false;
        }
    }
}
