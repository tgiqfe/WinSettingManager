namespace Receiver.DataContact
{
    public class DataContactRegistryKey
    {
        public DataContactRegistryAction Action { get; set; } = DataContactRegistryAction.None;
        public string Path { get; set; }
        public string Owner { get; set; }
        public string Account { get; set; }
        public string Inherited { get; set; }
        public string Rights { get; set; }
        public bool Recursive { get; set; }
        public string AccessControl { get; set; }

    }
}
