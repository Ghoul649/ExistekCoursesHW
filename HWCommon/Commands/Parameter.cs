using HWCommon.Commands.Converters;
using HWCommon.Commands.Parsers;
using System;
using System.Linq;
using System.Reflection;

namespace HWCommon.Commands
{
    public class Parameter
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        protected ParserAttribute _parser;
        public ParserAttribute Parser { get => _parser; }
        protected ConverterDelegate _converter;
        public Type ParameterType { get; }
        public Parameter(ParameterInfo paramInfo)
        {
            ParameterType = paramInfo.ParameterType;
            if (!Converter._converters.TryGetValue(ParameterType, out _converter))
            {
                if (Converter.BaseConverter.CanConvertTo(ParameterType))
                    _converter = str => Converter.BaseConverter.ConvertTo(str, ParameterType);
                else
                    throw new NotImplementedException($"There is no converter to type \"{ParameterType}\"");
            }

            var attrs = paramInfo.GetCustomAttributes(typeof(ParamAttribute));
            var attr = (attrs.Any() ? attrs.First() : null) as ParamAttribute;
            if (attr != null)
            {
                Name = attr.Name;
                Description = attr.Description;
            }
            var parsers = paramInfo.GetCustomAttributes(typeof(ParserAttribute));
            var parser = (parsers.Any() ? parsers.First() : null) as ParserAttribute;
            if (parser != null)
            {
                _parser = parser;
            }

            if (string.IsNullOrWhiteSpace(Name))
                Name = paramInfo.Name;
            if (_parser == null)
                _parser = new Word();
        }
        public Parameter(string name, Type type, ParserAttribute parser = null, string description = null)
        {
            ParameterType = type;
            if (!Converter._converters.TryGetValue(ParameterType, out _converter))
            {
                if (Converter.BaseConverter.CanConvertTo(ParameterType))
                    _converter = str => Converter.BaseConverter.ConvertTo(str, ParameterType);
                else
                    throw new NotImplementedException($"There is no converter to type \"{ParameterType}\"");
            }
            Name = name;
            Description = description;

            if (parser == null)
                _parser = new Word();
            else
                _parser = parser;
        }
        public object Parse(string value, ref int index) 
        {
            var parsed = _parser.Parse(value, ref index);
            if (parsed == null)
                throw new ArgumentException($"Can not parse parameter \"{Name}\"");
            return _converter(parsed);
        }
    }
}
