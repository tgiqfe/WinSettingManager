using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCode01.Items.LogonSession
{
    internal class PInvoke
    {
        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern nint WTSOpenServer(string pServerName);

        [DllImport("wtsapi32.dll")]
        internal static extern void WTSCloseServer(nint hServer);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern int WTSEnumerateSessions(
                nint hServer,
                int Reserved,
                int Version,
                ref nint ppSessionInfo,
                ref int pCount);

        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        internal static extern void WTSFreeMemory(nint memory);

        [DllImport("Wtsapi32.dll")]
        internal static extern bool WTSQuerySessionInformation(
            nint hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out nint ppBuffer, out uint pBytesReturned);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern bool WTSDisconnectSession(nint hServer, int sessionId, bool bWait);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern bool WTSLogoffSession(nint hServer, int SessionId, bool bWait);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WTS_SESSION_INFO
        {
            public int SessionID;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pWinStationName;
            public WTS_CONNECTSTATE_CLASS State;
        }

        internal enum WTS_INFO_CLASS
        {
            WTSInitialProgram = 0,
            WTSApplicationName = 1,
            WTSWorkingDirectory = 2,
            WTSOEMId = 3,
            WTSSessionId = 4,
            WTSUserName = 5,
            WTSWinStationName = 6,
            WTSDomainName = 7,
            WTSConnectState = 8,
            WTSClientBuildNumber = 9,
            WTSClientName = 10,
            WTSClientDirectory = 11,
            WTSClientProductId = 12,
            WTSClientHardwareId = 13,
            WTSClientAddress = 14,
            WTSClientDisplay = 15,
            WTSClientProtocolType = 16,
            WTSIdleTime = 17,
            WTSLogonTime = 18,
            WTSIncomingBytes = 19,
            WTSOutgoingBytes = 20,
            WTSIncomingFrames = 21,
            WTSOutgoingFrames = 22,
            WTSClientInfo = 23,
            WTSSessionInfo = 24,
            WTSSessionInfoEx = 25,
            WTSConfigInfo = 26,
            WTSValidationInfo = 27,
            WTSSessionAddressV4 = 28,
            WTSIsRemoteSession = 29
        }

        internal enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        internal struct WTSINFOA
        {
            public const int WINSTATIONNAME_LENGTH = 32;
            public const int DOMAIN_LENGTH = 17;
            public const int USERNAME_LENGTH = 20;
            public WTS_CONNECTSTATE_CLASS State;
            public int SessionId;
            public int IncomingBytes;
            public int OutgoingBytes;
            public int IncomingFrames;
            public int OutgoingFrames;
            public int IncomingCompressedBytes;
            public int OutgoingCompressedBytes;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WINSTATIONNAME_LENGTH)]
            public byte[] WinStationNameRaw;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DOMAIN_LENGTH)]
            public byte[] DomainRaw;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = USERNAME_LENGTH + 1)]
            public byte[] UserNameRaw;

            public string WinStationName { get { return Encoding.ASCII.GetString(WinStationNameRaw); } }
            public string Domain { get { return Encoding.ASCII.GetString(DomainRaw); } }
            public string UserName { get { return Encoding.ASCII.GetString(UserNameRaw); } }

            public long ConnectTimeUTC;
            public long DisconnectTimeUTC;
            public long LastInputTimeUTC;
            public long LogonTimeUTC;
            public long CurrentTimeUTC;

            public DateTime ConnectTime { get { return DateTime.FromFileTime(ConnectTimeUTC); } }
            public DateTime DisconnectTime { get { return DateTime.FromFileTime(DisconnectTimeUTC); } }
            public DateTime LastInputTime { get { return DateTime.FromFileTime(LastInputTimeUTC); } }
            public DateTime LogonTime { get { return DateTime.FromFileTime(LogonTimeUTC); } }
            public DateTime CurrentTime { get { return DateTime.FromFileTime(CurrentTimeUTC); } }
        }
    }
}
