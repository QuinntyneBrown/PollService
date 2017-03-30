using System.Collections.Generic;

namespace PollService.Features.Core
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger(string categoryName);

        List<ILoggerProvider> GetProviders();
    }
}
