using NLog;

namespace LibraryNuget
{
    public class NLogger
    {
        public void Log(string lineToLog)
        {
            ILogger logger = LogManager.GetLogger(lineToLog);
            logger.Info(lineToLog);
        }
    }
}
