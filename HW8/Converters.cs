using HWCommon.Commands.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW8
{
    public class Converters
    {
        [Converter(typeof(Matrix<double>))]
        public static object ToMatrix(string value)
        {
            var res = value.Split(new char[] { ' ' }, 3);
            if (res.Length != 3)
                throw new Exception("Wrong matrix format! Use : {width} {height} {0:0}, {1:0}, ... {width - 1:0}, {0:1}, {1:1}, ... {width - 1: height - 1}");
            int x = int.Parse(res[0]);
            int y = int.Parse(res[1]);
            return new Matrix<double>(x, y, res[2], ',');
        }
        
    }
}
