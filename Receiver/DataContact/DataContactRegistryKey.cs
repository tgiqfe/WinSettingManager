namespace Receiver.DataContact
{
    public enum RegistryAction
    {
        None,
        Get,
        Set,
        Owner,
        Grant,
        Revoke,
        Create,
        Delete,
        Copy,
        Move,
        Load,
        Unload,
    }

    public class DataContactRegistryKey
    {
        public RegistryAction Action { get; set; } = RegistryAction.None;
        public string Path { get; set; }
        public string Owner { get; set; }
        public string Account { get; set; }
        public string Inherited { get; set; }
        public string Rights { get; set; }
        public bool Recursive { get; set; }
        public string AccessControl { get; set; }

    }
}
