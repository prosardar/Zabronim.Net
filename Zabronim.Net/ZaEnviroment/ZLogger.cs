using System;
using NLog;

namespace Zabronim.Net.ZaEnviroment {
    public class ZLogger {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, params object[] args) {
            logger.Info(message, args);
        }

        public static void ErrorException(string message, Exception e) {
            logger.ErrorException(string.Format("{0} {1}", message, e.Message), e);
        }

        public static void Error(string message) {
            logger.Error(message);
        }

        public static void Error(string message, object[] args) {
            logger.Error(message, args);
        }
    }
}