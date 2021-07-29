using HWCommon.Commands;
using HWCommon.Commands.Converters;
using HWCommon.Commands.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HW8
{
    public class Core
    {
        public CommandHandler Manager { get; set; }
        public Dictionary<string, object> Objects = new Dictionary<string, object>();
        public Core(CommandHandler manager)
        {
            Manager = manager;
        }

        [Command("Display info about program")]
        public void Info() 
        {
            Console.WriteLine(Resource1.Info);
        }

        [Command("Save object to variable")]
        public void var(string varName, [Line] string command) 
        {
            Objects[varName] = Manager.Execute(command);
        }

        [Command("Create new object")]
        public object New(Type type, [QuotedText(Escaping = true)] string value) 
        {
            if (!Converter._converters.ContainsKey(type))
                throw new Exception($"There is no converter to type \"{type}\"");
            return Converter._converters[type].Invoke(value);
        }
        [Command("Read object from variable")]
        public object Read(string varName) 
        {
            return Objects[varName];
        }
        [Command("Generate matrix filled with random values")]
        public Matrix<double> Generate(int width, int height, double minVal, double maxVal,int seed) 
        {
            var res = new Matrix<double>(width, height);
            var rand = new Random(seed);
            double multiplier = maxVal - minVal;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    res[x, y] = rand.NextDouble() * multiplier + minVal;
            return res;
        }
        [Command("Multiply two matrices")]
        public Matrix<double> Multiply([Param("Name of variable with matrix")]string a, [Param("Name of variable with matrix")] string b)
        {
            
            return Matrix<double>.Multiply(Objects[a] as Matrix<double>, Objects[b] as Matrix<double>, (a, b) => a * b, (a, b) => a + b);
        }
        [Command("Multiply two matrices")]
        public Matrix<double> ParalelMultiply([Param("Name of variable with matrix")] string a, [Param("Name of variable with matrix")] string b)
        {
            return Matrix<double>.ParalelMultiply(Objects[a] as Matrix<double>, Objects[b] as Matrix<double>, (a, b) => a * b, (a, b) => a + b);
        }
        [Command("Execute command and display spent time")]
        public void Mesure([Line] string command) 
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Manager.Execute(command);
            sw.Stop();
            Console.WriteLine($"{Math.Round(sw.ElapsedMilliseconds / 1000.0,5)} seconds");
        }
    }
}
