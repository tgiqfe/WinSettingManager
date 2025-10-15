namespace Receiver.DataContact
{
    public class DataContactLogonSession
    {
        public enum LogonSessionAction
        {
            None,
            Get,
            Disconnect,
            Logoff,
        }

        public class UserLogonSession_ex
        {
            public string UserName { get; set; }
            public string UserDomain { get; set; }
            public int? SessionID { get; set; }
            public string SessionType { get; set; }
            public string SessionState { get; set; }
            public int? ProtocolType { get; set; }
            public DateTime? LogonTime { get; set; }
        }

        public UserLogonSession_ex[] LoggedOnSessions { get; set; }
        public LogonSessionAction Action { get; set; }
    }
}
