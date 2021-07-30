using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    abstract class StorageDevice : Product
    {
        public StorageDevice() : base() 
        {
        }
        public StorageDevice(uint id) : base(id)
        { 
        }
        public virtual DataSize Capacity { get; set; }
        public override string GetFullName()
        {
            return $"{base.GetFullName()} ({Capacity})";
        }
    }
}
