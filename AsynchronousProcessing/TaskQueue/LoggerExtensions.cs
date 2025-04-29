namespace AsynchronousProcessing.TaskQueue
{
    public static class LoggerExtensions
    {
        #region JOB

        public static string InfoJobProcessingFormat = "Processing the Job {{JobName}} {0}.";

        public static void InfoJobProcessing(this ILogger logger, string jobName, string message, params object[] args)
        {
            var template = string.Format(InfoJobProcessingFormat, message);
            logger.LogInformation(template, new object[] { jobName }.Concat(args).ToArray());
        }

        public static string ErrorJobProcessingFormat = "Processing the Job {@JobName} occurs error: {@Message}.";

        public static void ErrorJobProcessing(this ILogger logger, Exception ex, string jobName)
        {
            logger.LogError(ex, ErrorJobProcessingFormat, jobName, ex.Message);
        }

        #endregion JOB

        #region Action

        public static string InfoActionExecutedFormat = "Executed the Action {{Action}} is successful {0}.";

        public static void InfoActionExecuted(this ILogger logger, string actionName, string message, params object[] args)
        {
            var template = string.Format(InfoActionExecutedFormat, message);
            logger.LogInformation(template, new object[] { actionName }.Concat(args).ToArray());
        }

        public static string WarnActionExecutingFormat = "Executing the Action {{Action}} {0}.";

        public static void WarnActionExecuting(this ILogger logger, string actionName, string message, params object[] args)
        {
            var template = string.Format(WarnActionExecutingFormat, message);
            logger.LogWarning(template, new object[] { actionName }.Concat(args).ToArray());
        }

        public static string ErrorActionExecutingFormat = "Executing the Action {{Action}} {0}.";

        public static void ErrorActionExecuting(this ILogger logger, Exception ex, string actionName, string message, params object[] args)
        {
            var template = string.Format(ErrorActionExecutingFormat, message);
            logger.LogError(ex, template, new object[] { actionName }.Concat(args).ToArray());
        }

        #endregion Action
    }
}