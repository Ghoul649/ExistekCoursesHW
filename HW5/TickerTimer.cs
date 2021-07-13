using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW5
{
    class TickerTimer
    {
        public event Action<TickerTimer> Tick;
        public string Name { get; set; }
        private int _interval;
        public int Interval 
        { 
            get => _interval;
            set
            {
                if(IsActive)
                    throw new Exception("Changing interval while timer is running is not allowed");
                if (value <= 0)
                    throw new Exception("Interval must be higger than 0");
                _interval = value;
            } 
        }
        public bool IsActive { get => _timerState; }
        private bool _timerState = false;
        private bool _taskState = false;
        public void Start() 
        {
            if (_timerState)
                throw new Exception("Timer was already running");
            _timerState = true;
            if (!_taskState)
            {
                _taskState = true;
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (!_timerState)
                            break;
                        Tick?.Invoke(this);
                        Thread.Sleep(Interval);
                    }
                    _taskState = false;
                });
            }
        }
        public void Stop() 
        {
            if (!_timerState)
                throw new Exception("Timer was already stopped");
            _timerState = false;
        }
        public override string ToString()
        {
            return $"{Name} ({Interval}ms)" + (IsActive ? " (Active)" : "");
        }
    }
}
