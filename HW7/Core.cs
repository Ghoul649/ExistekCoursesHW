using HWCommon.Commands;
using HWCommon.Commands.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HW7
{
    class Core
    {
        public Assembly CurrentAssembly;
        void checkAssembly() 
        {
            if (CurrentAssembly == null)
                throw new FileLoadException("Assembly is not loaded");
        }
        [Command("Load assembly")]
        public void Load([QuotedText] string filepath) 
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException("Specified file does not exist");
            CurrentAssembly = Assembly.LoadFrom(filepath);
        }

        [Command("Display all types in loaded assembly")]
        public void List() 
        {
            checkAssembly();
            foreach (var type in CurrentAssembly.GetTypes()) 
            {
                Console.WriteLine(type);
            }
        }
        [Command("Display filtered types in loaded assembly")]
        public void List([Line]string filter) 
        {
            checkAssembly();
            foreach (var type in CurrentAssembly.GetTypes().Where(t => t.ToString().Contains(filter,StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(type);
            }

        }
    }
}
