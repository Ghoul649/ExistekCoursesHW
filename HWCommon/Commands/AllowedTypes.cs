using System;
using System.Collections.Generic;
using System.Text;

namespace HWCommon
{
    public static class AllowedTypes
    {
        private static Dictionary<string, Type> allowedTypes = new Dictionary<string, Type>();
        public static Type GetType(string type)
        {
            if (allowedTypes.ContainsKey(type))
                return allowedTypes[type];
            var res = Type.GetType(type);
            if (res is null)
                throw new Exception($"There is no type with name \"{type}\"");
            return res;
        }
        public static string GeTypeName(Type type)
        {
            foreach (var item in allowedTypes)
                if (item.Value == type)
                    return item.Key;
            return type.ToString();
        }
        public static void Add(string key, Type type) 
        {
            if (allowedTypes.ContainsKey(key))
                throw new Exception("Type is already loaded");
            allowedTypes.Add(key, type);
        }
        public static void LoadBase() 
        {
            allowedTypes.Add("int", typeof(int));
            allowedTypes.Add("double", typeof(double));
            allowedTypes.Add("float", typeof(float));
            allowedTypes.Add("string", typeof(string));
            allowedTypes.Add("type", typeof(Type));
            allowedTypes.Add("void", typeof(void));
        }
    }
}
