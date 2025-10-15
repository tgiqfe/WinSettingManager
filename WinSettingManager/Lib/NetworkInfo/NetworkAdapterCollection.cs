using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.NetworkInfo
{
    public class NetworkAdapterCollection
    {
        public NetworkAdapter[] NetworkAdapters { get; set; }

        public static NetworkAdapterCollection Load()
        {
            //  Source data for Network adapter.
            var netAdapters = new ManagementClass(
                    "\\ROOT\\StandardCimv2",
                    "MSFT_NetAdapter",
                    new ObjectGetOptions(null, TimeSpan.MaxValue, true)).
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for Network adapter config.
            var netConfigs = new ManagementClass(
                    "Win32_NetworkAdapterConfiguration").
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for Pnp device.
            var pnpEntities = new ManagementClass(
                    "Win32_PnpEntity").
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for service.
            var devServices = ServiceController.GetDevices();

            var list = new List<NetworkData>();
            foreach (var netAdapter in netAdapters)
            {
                var netConfig = netConfigs.FirstOrDefault(x => x["SettingID"]?.ToString() == netAdapter["DeviceID"]?.ToString());
                var pnpEntity = pnpEntities.FirstOrDefault(x => x["PNPDeviceID"]?.ToString() == netAdapter["PnPDeviceID"]?.ToString());
                var devService = netConfig == null ?
                    null :
                    devServices.FirstOrDefault(x => x.ServiceName == netConfig["ServiceName"]?.ToString().TrimEnd('.'));    //  Some service names contain an unnecessary dot at the end.
                list.Add(new NetworkData()
                {
                    NetworkAdapter = netAdapter,
                    NetworkConfig = netConfig,
                    PnpEntity = pnpEntity,
                    DeviceService = devService
                });
            }

            return new NetworkAdapterCollection()
            {
                NetworkAdapters = list.Select(x => new NetworkAdapter(x)).ToArray()
            };
        }
    }
}
