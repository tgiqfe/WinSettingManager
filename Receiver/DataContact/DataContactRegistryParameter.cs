namespace Receiver.DataContact
{
    public class DataContactRegistryParameter
    {
        public DataContactRegistryAction Action { get; set; } = DataContactRegistryAction.None;
        public string Path { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
