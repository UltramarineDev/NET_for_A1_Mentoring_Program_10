using NLog;

namespace MvcMusicStore
{
    public class MvcMusicLogger : ILogger
    {
        NLog.ILogger _logger;

        public MvcMusicLogger()
        {
            _logger = LogManager.GetLogger("MvcMusicStoreLogger");
        }

        public void Debug(string message)
        {
            _logger.Debug($"{message}");
        }

        public void Error(string message)
        {
            _logger.Error($"{message}");
        }

        public void Info(string message)
        {
            _logger.Info($"{message}");
        }
    }
}