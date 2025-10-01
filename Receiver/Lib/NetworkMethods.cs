using WinSettingManager.Items.Network;

namespace Receiver.Lib
{
    public class NetworkMethods
    {
        public static async Task<NetworkAdapterCollection> GetNetworkAdapterInfo()
        {
            return await Task.Run<NetworkAdapterCollection>(() =>
            {
                return NetworkAdapterCollection.Load();
            });
        }
    }
}
