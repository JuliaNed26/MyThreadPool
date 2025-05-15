// See https://aka.ms/new-console-template for more information

using ThreadPool = MyThreadPool.ThreadPool;

var threadPool = new ThreadPool();
threadPool.Schedule(() => new Task(() =>
{
    Console.WriteLine("Buenas tardes!");
    Thread.Sleep(100);
}));

threadPool.Schedule(() => new Task(() =>
{
    Console.WriteLine("Me llamo Julia");
    Thread.Sleep(50);
}));

threadPool.Schedule(() => new Task(() =>
{
    Console.WriteLine("Yo ablo Espanol");
    Thread.Sleep(30);
}));

await threadPool.StopAddingAsync();