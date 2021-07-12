using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HWCommon.Commands.Converters
{
    public delegate object ConverterDelegate(string value);

    public class ConverterAttribute : Attribute 
    {
        public Type ConverterType { get; set; }
        public ConverterAttribute(Type type) 
        {
            ConverterType = type;
        }
    }
    public static class Converter
    {
        public static Dictionary<Type, ConverterDelegate> _converters = new Dictionary<Type, ConverterDelegate>();
        public static TypeConverter BaseConverter;
        public static ConverterDelegate GetConverter() 
        {
            if (_converters == null)
                ReloadConverters();
            
            return null;
        }
        public static int ReloadConverters() 
        {
            BaseConverter = new TypeConverter();
            var types = Assembly.GetExecutingAssembly().GetTypes().ToList();
            types.AddRange(Assembly.GetEntryAssembly().GetTypes());
            Dictionary<Type, ConverterDelegate> newList = new Dictionary<Type, ConverterDelegate>();
            foreach (Type t in types)
            {
                var methods = t.GetMethods();
                foreach (var method in methods) 
                {
                    if (!method.IsStatic || !method.IsPublic)
                        continue;
                    var attrs = method.GetCustomAttributes(typeof(ConverterAttribute));
                    if (!attrs.Any())
                        continue;
                    var convAttr = method.GetCustomAttributes(typeof(ConverterAttribute)).First() as ConverterAttribute;
                    var parameters = method.GetParameters();
                    if (parameters.Length == 1 && parameters.First().ParameterType == typeof(string) && method.ReturnType == typeof(object))
                        newList[convAttr.ConverterType] = method.CreateDelegate(typeof(ConverterDelegate)) as ConverterDelegate;
                }
            }
            _converters = newList;
            return _converters.Count();
        }
    }
}
