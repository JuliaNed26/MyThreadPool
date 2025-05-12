using System.Collections.Concurrent;

namespace MyThreadPool;

public class ThreadPool
{
    private readonly BlockingCollection<Action> _poolOfTasks;

    public ThreadPool()
    {
        _poolOfTasks = new BlockingCollection<Action>(new ConcurrentQueue<Action>());
        var executionThread = new Thread(ExecuteAsync);
        executionThread.Start();
    }

    public void Schedule(Action taskToExecute)
    {
        _poolOfTasks.Add(taskToExecute);
    }

    public void StopAdding()
    {
        _poolOfTasks.CompleteAdding();
    }

    private void ExecuteAsync()
    {
        foreach (var action in _poolOfTasks.GetConsumingEnumerable())
        {
            action.Invoke();
        }
    }
}