using System;
using System.Collections.Generic;
using System.Text;

namespace HW6
{
    class Point
    {
        private static int _nextCharNum = 'A';
        private static int _maxCharNum = 'Z';
        public double X { get; }
        public double Y { get; }
        public char CharName { get; set; }
        public Point() 
        {
            if (_nextCharNum > _maxCharNum)
                throw new Exception("All characters had been used");
            CharName = (char)_nextCharNum++;
        }
    }
}
