using System;
using System.Collections.Generic;
using System.Text;

namespace HW4
{
    struct TimeInterval
    {
        public int Minutes;
        public TimeInterval(int minutes) 
        {
            if (minutes < 0)
                throw new ArgumentOutOfRangeException("Minutes","Value must be positive");
            Minutes = minutes;
        }
        public TimeInterval(int hours,int minutes)
        {
            Minutes = minutes + hours * 60;
            if (Minutes < 0)
                throw new ArgumentOutOfRangeException("TimeInterval", "Value must be positive");
        }
        public TimeInterval(int days, int hours, int minutes)
        {
            Minutes = minutes + hours * 60 + days * 1440;
            if (Minutes < 0)
                throw new ArgumentOutOfRangeException("TimeInterval", "Value must be positive");
        }
        public TimeInterval(int years, int days, int hours, int minutes)
        {
            Minutes = minutes + hours * 60 + days * 1440 + years * 525600;
            if (Minutes < 0)
                throw new ArgumentOutOfRangeException("TimeInterval", "Value must be positive");
        }
        public override string ToString() 
        {
            int m = Minutes;
            if (m / 525600 > 10)
                return $"{Math.Round(m / 525600.0 , 1)} years";
            int y = m / 525600;
            m -= y * 525600;
            if (y > 0)
                return $"{y} years, {Math.Round(m / 1440.0, 0)} days";
            int d = m / 1440;
            m -= d * 1440;
            if(d > 0)
                return $"{d} days, {Math.Round(m/60.0, 0)} hours";
            int h = m / 60;
            m -= h * 60;
            return $"{d} hours, {m} minutes";
        }
    }
}
