using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Options;

namespace Receiver
{
    public class ServiceLifeTime : WindowsServiceLifetime
    {
        private readonly ILogger<ServiceLifeTime> _logger;

        public ServiceLifeTime(IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory, IOptions<HostOptions> optionsAccessor) : base(environment, applicationLifetime, loggerFactory, optionsAccessor)
        {
            _logger = loggerFactory.CreateLogger<ServiceLifeTime>();
        }

        public ServiceLifeTime(IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory, IOptions<HostOptions> optionsAccessor, IOptions<WindowsServiceLifetimeOptions> windowsServiceOptionsAccessor) : base(environment, applicationLifetime, loggerFactory, optionsAccessor, windowsServiceOptionsAccessor)
        {
            _logger = loggerFactory.CreateLogger<ServiceLifeTime>();
        }

        protected override void OnStart(string[] args)
        {
            _logger.LogInformation("Service is starting.");
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _logger.LogInformation("Service is stopping.");
            base.OnStop();
        }
    }
}
