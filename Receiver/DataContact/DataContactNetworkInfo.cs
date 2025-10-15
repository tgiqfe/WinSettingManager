namespace Receiver.DataContact
{
    public class DataContactNetworkInfo
    {
        public string InterfaceName { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public bool? Enabled { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public string SubnetMask { get; set; }
        public string DefaultGateway { get; set; }
        public string[] DNSServers { get; set; }
        public bool? DHCPEnabled { get; set; }
    }
}
