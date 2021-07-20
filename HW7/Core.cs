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
                !t.IsGenericType &&
                t.IsClass &&
                !t.FullName.StartsWith('<')
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
            Console.WriteLine(g.WriteClassWithNS(selectedType));
        }
        [Command("Save code of class to file")]
        public void SaveCode(string type)
        {
            Type selectedType = _loadedTypes.First(t => t.FullName == type);
            Generator g = new Generator();
            string code = g.WriteClassWithNS(selectedType);
            if (File.Exists($"{selectedType.Name}.cs")) 
            {
                Console.WriteLine($"File {selectedType.Name}.cs already exists. Press Y to replace it or another key to create new file");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y)
                    File.WriteAllText($"{selectedType.Name}.cs", code);
                else 
                {
                    int index = 1;
                    while (File.Exists($"{selectedType.Name}{index}.cs")) 
                    {
                        index++;
                    }
                    File.WriteAllText($"{selectedType.Name}{index}.cs", code);
                }
            }
            else
                File.WriteAllText($"{selectedType.Name}.cs", code);
        }
    }
}
