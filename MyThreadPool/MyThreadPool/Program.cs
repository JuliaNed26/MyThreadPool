// See https://aka.ms/new-console-template for more information

using ThreadPool = MyThreadPool.ThreadPool;

var threadPool = new ThreadPool();
threadPool.Schedule(() => Console.WriteLine("Buenas tardes!"));
threadPool.Schedule(() => Console.WriteLine("Me illamo Julia"));
threadPool.Schedule(() => Console.WriteLine("Yo ablo Espanol"));
threadPool.StopAdding();