using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class Drive : StorageDevice
    {
        public Drive() : base() 
        {
        }
        public Drive(uint id) : base(id) 
        {
        }
        public string DriveType { get; set; }
        public DataSpeed WritingSpeed { get; set; }
        public DataSpeed ReadingSpeed { get; set; }
        public override string GetFullName()
        {
            return $"{base.GetFullName()} (W:{WritingSpeed}; R:{ReadingSpeed}) {DriveType}";
        }
    }
}
