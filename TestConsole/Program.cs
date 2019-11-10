using System;
using System.Diagnostics;
using System.Threading;
using LokiLogger;
using LokiLogger.LoggerAdapter;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch t1 = Stopwatch.StartNew();
            DoStuff("12");
            t1.Stop();
             
            //Loki.UpdateAdapter(new BasicLoggerAdapter());
            Stopwatch t2 = Stopwatch.StartNew();
            DoStuff2("13");
            t2.Stop();
            Console.WriteLine($"T1:\t{t1.ElapsedMilliseconds}");
            Console.WriteLine($"T2:\t{t2.ElapsedMilliseconds}");
            
        }

        public static void DoStuff(string input)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(2000));
        }
        [Loki]
        public static long DoStuff1(string input)
        {
            for (int i = 0; i < 10; i++)
            {
                input += "2";
            }

            return long.Parse(input);
        }
        [Loki]
        public static void DoStuff2(string input)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(2000));
        }
    }
}