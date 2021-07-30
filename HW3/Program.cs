using HW.Common.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HW3
{
    partial class Program
    {
        static List<Product> products = new List<Product>();
        static CommandHandler commandManager = new CommandHandler();
        static bool Exit = false;
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            commandManager.Echo = (str) => 
            {
                var lc = Console.ForegroundColor; 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(str); 
                Console.ForegroundColor = lc; 
            };

            InitCommands();

            if (File.Exists("Data.json"))
            {
                var json = File.ReadAllText("Data.json");
                try
                {
                    var res = JsonSerializer.Deserialize<List<Product>>(json);
                    products.AddRange(res);
                    Console.WriteLine("Products loaded from \"Data.json\"");
                }
                catch
                {
                    Console.WriteLine("File \"Data.json\" is broken");
                }
            }
            while (!Exit) 
            {
                Console.Write("->");
                commandManager.Execute(Console.ReadLine());
            }

        }
    }
}
