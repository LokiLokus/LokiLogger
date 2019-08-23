using System;
using System.Threading.Tasks;
using LokiLogger;
using LokiLogger.LoggerAdapter;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Loki.UpdateAdapter(new BasicLoggerAdapter());
            Stuff("asdasdasd", 3).Wait();
            d();
            Console.WriteLine("Hello World!");
        }
        [Loki]
        public static void d()
        {
            Console.WriteLine("asdas");
        }
        
        [Loki]
        public static async Task Stuff(string data, int asd)
        {
            await Task.Delay(323);
            Console.WriteLine("hallo");
        }
    }
}