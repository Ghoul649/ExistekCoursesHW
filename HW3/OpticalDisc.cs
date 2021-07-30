using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class OpticalDisc : StorageDevice
    {
        public OpticalDisc() : base()
        {
        }
        public OpticalDisc(uint id) : base(id)
        {
        }
        public int WritingSpeed { get; set; }
        public int PackSize { get; set; }

        public override string GetFullName()
        {
            return $"{base.GetFullName()} {WritingSpeed}x {PackSize} Packs Optical Disc";
        }
    }
}
