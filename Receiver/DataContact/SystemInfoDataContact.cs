namespace Receiver.DataContact
{
    public class SystemInfoDataContact
    {
        public string OSVersion { get; set; }
        public string MachineName { get; set; }
        public bool IsDomainPC { get; set; }
        public string DomainName { get; set; }
        public string[] LoggedOnUsers { get; set; }
        public int ProcessorCount { get; set; }
        public int SystemMemoryMB { get; set; }
        public bool Is64BitOS { get; set; }
        public string[] LogicalDrives { get; set; }
        public string MachineSerial { get; set; }
    }
}
