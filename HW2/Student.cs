using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class Student
    {
        public string FirstName { get; }
        public string SecondName { get; }
        public string Patronymic { get; }
        public string Group { get; }
        private Dictionary<string, List<int>> _marks = new Dictionary<string, List<int>>();
        public Student(string fname, string sname, string patr, string group) 
        {
            FirstName = fname;
            SecondName = sname;
            Patronymic = patr;
            Group = group;
        }
        public IEnumerable<string> GetSubjects() 
        {
            return _marks.Keys;
        }
        public IEnumerable<int> GetMarks(string subject) 
        {
            if (!_marks.TryGetValue(subject, out List<int> marks))
                throw new Exception($"Subject \"{subject}\" does not exist");
            return marks;
        }
        public int GetMark(string subject)
        {
            if (!_marks.TryGetValue(subject, out List<int> marks))
                throw new Exception($"Subject \"{subject}\" does not exist");
            return marks.Sum();
        }
        public double GetMark()
        {
            double sMark = 0;
            foreach (var marks in _marks.Values)
                sMark += marks.Sum();
            return sMark == 0 ? 0 : sMark / _marks.Count;
        }
        public void AddSubject(string subject) 
        {
            if (_marks.ContainsKey(subject))
                throw new Exception($"Subject \"{subject}\" already exists");
            _marks.Add(subject, new List<int>());
        }
        public void AddMark(string subject, int mark) 
        {
            if (!_marks.TryGetValue(subject, out List<int> marks))
                throw new Exception($"Subject \"{subject}\" does not exist");
            marks.Add(mark);
        }
    }
}
