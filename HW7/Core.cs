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
        private IEnumerable<Type> _loadedTypes;
        void checkAssembly()
        {
            if (CurrentAssembly == null)
                throw new FileLoadException("Assembly is not loaded");
        }
        [Command("Load assembly")]
        public void Load([Line] string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException("Specified file does not exist");
            CurrentAssembly = Assembly.LoadFrom(filepath);
            _loadedTypes = CurrentAssembly.GetTypes().Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                !t.IsGenericType
            );
        }

        [Command("Display all types in loaded assembly")]
        public void List()
        {
            checkAssembly();
            foreach (var type in _loadedTypes)
            {
                Console.WriteLine(type);
            }
        }
        [Command("Display filtered types in loaded assembly")]
        public void List([Line] string filter)
        {
            checkAssembly();
            string[] lines = filter.Split();
            foreach (var type in _loadedTypes.Where(
                t =>
                {
                    var str = t.ToString();
                    foreach (var word in lines)
                        if (!str.Contains(word, StringComparison.OrdinalIgnoreCase))
                            return false;
                    return true;
                }
                )
            )
            {
                Console.WriteLine(type);
            }
        }

        [Command("Display all members of specified type")]
        public void Getmembers(string type) 
        {
            Type selectedType = _loadedTypes.First(t => t.FullName == type);
            foreach (var prop in selectedType.GetProperties())
                Console.WriteLine(prop);
            foreach (var method in selectedType.GetMethods())
                Console.WriteLine(method);
        }

        [Command("Display code of class")]
        public void WriteCode(string type) 
        {
            Type selectedType = _loadedTypes.First(t => t.FullName == type);
            Generator g = new Generator();

            StringBuilder sb = new StringBuilder();

            g.WriteClass(sb, selectedType);

            foreach (var prop in selectedType.GetProperties())
            {
                g.WriteProp(sb, prop);
                sb.Append('\n');
            }
            StringBuilder ns = new StringBuilder();
            g.WriteNamespaces(ns);
            Console.WriteLine(ns.ToString() + sb.ToString());

        }
    }
}
