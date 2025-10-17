namespace Receiver.DataContact
{
    public class WindowsServiceDataContact
    {
        public class ServiceSummary
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Status { get; set; }
            public string StartupType { get; set; }
            public bool? TriggerStart { get; set; }
            public bool? DelayedAutoStart { get; set; }
            public string ExecutePath { get; set; }
            public string Description { get; set; }
            public string LogonName { get; set; }
            public long? ProcessId { get; set; }
        }

        public class ServiceSimpleSummary
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Status { get; set; }
            public string StartupType { get; set; }
        }

        public ServiceSummary[] ServiceSummaries { get; set; }
        public ServiceSimpleSummary[] ServiceSimpleSummaries { get; set; }
    }
}
