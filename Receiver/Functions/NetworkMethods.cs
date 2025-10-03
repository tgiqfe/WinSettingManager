using System.Text;
using WinSettingManager.Items.Network;

namespace Receiver.Lib
{
    public class NetworkMethods
    {
        public static async Task<NetworkAdapterCollection> GetNetworkAdapterCollection()
        {
            return await Task.Run(() =>
                NetworkAdapterCollection.Load());
        }

    }
}
