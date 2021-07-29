using System;
namespace HWCommon.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Output { get; set; }
        public bool IgnoreCase { get; set; }
        public CommandAttribute() { }
        public CommandAttribute(string desc) 
        {
            Description = desc;
        }
        public CommandAttribute(string keyword, string desc)
        {
            Keyword = keyword;
            Description = desc;
        }
    }
}
