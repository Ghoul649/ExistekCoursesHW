using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    [JsonConverter(typeof(ProductConverter))]
    abstract class Product
    {
        public static uint NextId = 0;
        public uint ID { get; private set; }
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public string Model { get; set; }
        private int _count;
        public int Count 
        { 
            get => _count;
            set 
            {
                if (value < 0)
                    _count = 0;
                else
                    _count = value;
            } 
        }
        private double _price;
        public double Price 
        {
            get => _price;
            set 
            {
                if (value < 0)
                    _price = 0;
                else
                    _price = value;
            }
        }

        public Product() 
        {
            ID = NextId++;
        }
        public Product(uint id) 
        {
                if (id < NextId)
                    throw new ArgumentOutOfRangeException("The specified ID is already taken");
                ID = id;
                NextId = ID + 1;
        }

        public virtual string GetFullName() 
        {
            return $"{ProducerName} \"{Name} {Model}\"";
        }
        public override string ToString()
        {
            return $"{ID}. " + (Count > 0 ? $"({Count}) " : "(Not available) ") + GetFullName() + $" - {Price}₴";
        }


    }
}
