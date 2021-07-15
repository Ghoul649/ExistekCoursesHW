using HWCommon.Commands.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW4_Task2
{
    delegate double CircleToScalar(double radius);
    enum FunctionsType
    {
        Radius,
        Diameter,
        Length,
        Area
    }
    static class FunctionsTypeExtension
    {
        public static CircleToScalar GetFunction(FunctionsType type)
        {
            switch (type) 
            {
                case FunctionsType.Diameter:
                    return r => 2 * r;
                case FunctionsType.Length:
                    return r => 2 * r * Math.PI;
                case FunctionsType.Area:
                    return r => r * r * Math.PI;
            }
            return null;
        }
        [Converter(typeof(FunctionsType))]
        public static object ToFunctionsType(string value) 
        {
            return Enum.Parse(typeof(FunctionsType), value);
        }
    }
}
