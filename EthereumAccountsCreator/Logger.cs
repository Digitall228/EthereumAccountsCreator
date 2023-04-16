using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAccountsCreator
{
    public static class Logger
    {
        public static object locker { get; private set; } = new object();
        public static void LogAdd(string text, ConsoleColor color)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{DateTime.UtcNow}: {text}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
