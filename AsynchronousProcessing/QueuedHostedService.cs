using AsynchronousProcessing.TaskQueue;

namespace AsynchronousProcessing
{
    /// <summary>
    /// 排程器
    /// </summary>
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        public readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceProvider _services;

        public QueuedHostedService(ILogger<QueuedHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _services = serviceProvider;
            _taskQueue = serviceProvider.GetRequiredService<IBackgroundTaskQueue>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    //_logger.LogInformation("The {JobName} is starting at {ExcuteTime}.", this.GetType().Name, DateTimeOffset.Now);

                    using (var scope = _services.CreateScope())
                    {
                        var workItem = await _taskQueue.DequeueAsync(cancellationToken);
                        await workItem(cancellationToken, scope.ServiceProvider);
                    }

                    //_logger.LogInformation("The {JobName} is end at {ExcuteTime}. (Succeeded)", this.GetType().Name, DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.ErrorJobProcessing(ex, this.GetType().Name);
                }
                finally
                {
                }
            }

            _logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}