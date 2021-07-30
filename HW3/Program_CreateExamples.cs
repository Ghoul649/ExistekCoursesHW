using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    partial class Program
    {
        static void CreateExamples()
        {
            products.Add(
                    new FlashDrive()
                    {
                        ProducerName = "Kingston",
                        Name = "DataTreveler 100 G3",
                        Model = "DT100G3",
                        Capacity = new DataSize("64000MB"),
                        USBVersion = "3.0",
                        Count = 100,
                        Price = 229
                    });
            products.Add(
                new Drive()
                {
                    ProducerName = "Kingston",
                    Name = "A400",
                    Model = "SA400S37",
                    Capacity = new DataSize("480GB"),
                    DriveType = "SSD",
                    Count = 13,
                    Price = 1649,
                    WritingSpeed = new DataSpeed("450MB"),
                    ReadingSpeed = new DataSpeed("500MB")
                });
            products.Add(
                new Drive()
                {
                    ProducerName = "Seagate",
                    Name = "SkyHawk",
                    Model = "ST6000VX001",
                    Capacity = new DataSize("6000GB"),
                    DriveType = "HDD",
                    Count = 5,
                    Price = 6799,
                    WritingSpeed = new DataSpeed("180MB"),
                    ReadingSpeed = new DataSpeed("180MB")
                });
            products.Add(
                new OpticalDisc()
                {
                    ProducerName = "HP",
                    Name = "CD-R",
                    Model = "69308",
                    Capacity = new DataSize("700MB"),
                    Count = 100,
                    Price = 58,
                    WritingSpeed = 52,
                    PackSize = 10
                });
        }
    }
}
