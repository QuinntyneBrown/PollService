namespace PollService.Features.Core
{
    public interface ILogger
    {
        void AddProvider(ILoggerProvider provider);
    }
}
