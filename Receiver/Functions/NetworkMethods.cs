using System.Text;
using WinSettingManager.Lib.NetworkInfo;

namespace Receiver.Functions
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
