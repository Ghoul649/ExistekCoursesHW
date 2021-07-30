using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class FlashDrive : StorageDevice
    {
        public FlashDrive() : base()
        {
        }
        public FlashDrive(uint id) : base(id)
        {
        }
        public string USBVersion { get; set; }
        public override string GetFullName()
        {
            return $"{base.GetFullName()} {USBVersion} USB Flash drive";
        }
    }
}
