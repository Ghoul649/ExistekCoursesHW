using System;

namespace HWCommon.Commands
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class ParamAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ParamAttribute() 
        {
            
        }
        public ParamAttribute(string desc) 
        {
            Description = desc;
        }
    }
}
