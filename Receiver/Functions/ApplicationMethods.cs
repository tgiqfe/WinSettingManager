using System.Reflection;

namespace Receiver.Functions
{
    public class ApplicationMethods
    {
        /// <summary>
        /// Application Exit
        /// </summary>
        public static async void ExitApplicationAsync()
        {
            await Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                Environment.Exit(0);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Application Version
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetVersionAsync()
        {
            return await Task.Run(() =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return version != null ? 
                    "Windows Setting Manager: " + version.ToString() : 
                    "Windows Setting Manager: Unknown Version";
            });
        }
    }
}
