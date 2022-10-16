using System;

namespace Flipkart.Common
{
    public class TwicLogger
    {
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TwicLogger));
        public TwicLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void Debug(object message, Exception exceptionData)
        {
            logger.Debug(message, exceptionData);
        }

        public void Error(object message)
        {
            logger.Error(message);
        }

        public void Error(object message, Exception exceptionData)
        {
            logger.Error(message, exceptionData);
        }

        public void Fatal(object message)
        {
            logger.Fatal(message);
        }

        public void Fatal(object message, Exception exceptionData)
        {
            logger.Fatal(message, exceptionData);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void Info(object message, Exception exceptionData)
        {
            logger.Info(message, exceptionData);
        }

        public void Warn(object message)
        {
            logger.Warn(message);
        }

        public void Warn(object message, Exception exceptionData)
        {
            logger.Warn(message, exceptionData);
        }
    }
}
