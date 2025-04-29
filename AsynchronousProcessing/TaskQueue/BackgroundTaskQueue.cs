using System.Collections.Concurrent;

namespace AsynchronousProcessing.TaskQueue
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ILogger<BackgroundTaskQueue> _logger;

        private ConcurrentQueue<Func<CancellationToken, IServiceProvider, Task>> _workItems =
            new ConcurrentQueue<Func<CancellationToken, IServiceProvider, Task>>();

        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public BackgroundTaskQueue(ILogger<BackgroundTaskQueue> logger)
        {
            _logger = logger;
        }

        public void QueueBackgroundWorkItem(Func<CancellationToken, IServiceProvider, Task> workItem)
        {
            try
            {
                if (workItem == null)
                    throw new ArgumentNullException(nameof(workItem));

                _workItems.Enqueue(workItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"BackgroundTaskQueue QueueBackgroundWorkItem Error Message:{ex.Message}");
            }
            finally
            {
                _signal.Release();
            }
        }

        public async Task<Func<CancellationToken, IServiceProvider, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);
            return workItem;
        }
    }
}