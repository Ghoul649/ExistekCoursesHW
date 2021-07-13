using HWCommon.Commands;
using HWCommon.Commands.Parsers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW5
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler manager = new CommandHandler();
            new BaseCommands(manager);
            manager.Init(new Program());
            BaseConsoleIO.Start(manager);
        }
        List<TickerTimer> Timers = new List<TickerTimer>();
        [Command("Create new timer")]
        public void CreateTimer([QuotedText] string name, double interval)
        {
            var timer = new TickerTimer() { Interval = (int)(interval * 1000), Name = name };
            timer.Tick += (t) => Console.Write($"{timer.Name}: Tick\n->");
            Timers.Add(timer);
        }

        [Command("Start timer")]
        public void StartTimer(int id)
        {
            Timers[id].Start();
        }
        [Command("Stop timer")]
        public void StopTimer(int id)
        {
            Timers[id].Stop();
        }
        [Command("Display timers")]
        public void List()
        {
            Console.WriteLine("Timers:");
            int index = 0;
            foreach (var t in Timers)
                Console.WriteLine($"{index++} - {t}");
        }
        [Command("Start all timers")]
        public void Start()
        {
            foreach (var t in Timers)
                if (!t.IsActive)
                    t.Start();
        }
        [Command("Stop all timers")]
        public void Stop() 
        {
            foreach (var t in Timers)
                Task.Run(() =>
                {
                    if (t.IsActive)
                        t.Stop();
                });
        }
    }
}
