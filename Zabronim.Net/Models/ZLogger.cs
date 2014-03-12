using NLog;

namespace Zabronim.Net.Models {
    public class ZLogger {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, params object[] args) {
            logger.Info(message, args);
        }
    }
}