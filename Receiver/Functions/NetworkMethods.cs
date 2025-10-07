using System.Text;
using WinSettingManager.Lib.Network;

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
