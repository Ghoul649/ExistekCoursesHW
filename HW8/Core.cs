using HWCommon.Commands;
using HWCommon.Commands.Converters;
using HWCommon.Commands.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW8
{
    public class Core
    {
        public Dictionary<string, object> Objects = new Dictionary<string, object>();

        [Command("Create new object")]
        public void Let(string varName, Type type, [Line] string value) 
        {
            if (!Converter._converters.ContainsKey(type)) 
                throw new Exception($"There is no converter to type \"{type}\"");
            Objects[varName] = Converter._converters[type].Invoke(value);
        }
    }
}
