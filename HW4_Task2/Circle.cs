using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_Task2
{
    class Circle
    {
        private double _radius;
        public double Radius 
        { 
            get => _radius;
            set 
            {
                if (value <= 0)
                    throw new Exception("The radius must be positive");
                _radius = value;
            } 
        }
        public CircleToScalar TransformFunction { get; set; }
        public double GetScalar() 
        {
            return TransformFunction?.Invoke(Radius) ?? Radius;
        }
    }
}
