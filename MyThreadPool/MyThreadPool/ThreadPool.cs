using System.Collections.Concurrent;

namespace MyThreadPool;

public class ThreadPool
{
    private readonly Task _processingTask;
    private readonly BlockingCollection<Func<Task>> _poolOfTasks;

    public ThreadPool()
    {
        _poolOfTasks = new BlockingCollection<Func<Task>>(new ConcurrentQueue<Func<Task>>());
        _processingTask = Task.Run(ExecuteAsync);
    }

    public void Schedule(Func<Task> taskToExecute)
    {
        _poolOfTasks.Add(taskToExecute);
    }

    public async Task StopAddingAsync()
    {
        _poolOfTasks.CompleteAdding();
        await _processingTask;
    }

    private async Task ExecuteAsync()
    {
        var tasks = new List<Task>();
        foreach (var task in _poolOfTasks.GetConsumingEnumerable())
        {
            var taskToExecute = task();
            if (taskToExecute.Status == TaskStatus.Created)
            {
                taskToExecute.Start();
            }

            tasks.Add(taskToExecute); 
        }

        await Task.WhenAll(tasks);
    }
}