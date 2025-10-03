using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Functions
{
    public class ServiceControl
    {
        /// <summary>
        /// Get ServiceController instance from service name or display name.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static ServiceController GetServiceController(string serviceName)
        {
            var ret = ServiceController.GetServices().FirstOrDefault(
                x => x.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            if (ret == null)
            {
                ret = ServiceController.GetServices().FirstOrDefault(
                    x => x.DisplayName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            }
            return ret;
        }

    }
}
