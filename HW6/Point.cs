using System;
using System.Collections.Generic;
using System.Text;

namespace HW6
{
    class Point
    {
        private static int _nextCharNum = 'A';
        private static int _maxCharNum = 'Z';
        public double X { get; set; }
        public double Y { get; set; }
        public char CharName { get; }
        public Point() 
        {
            if (_nextCharNum > _maxCharNum)
                throw new Exception("All characters had been used");
            CharName = (char)_nextCharNum++;
        }
        public override string ToString()
        {
            return $"{CharName} ({X} : {Y})";
        }
    }
}
