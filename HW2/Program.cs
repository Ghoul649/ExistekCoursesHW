using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class Program
    {
        static List<Student> Students = new List<Student>();
        static bool Exit = false;
        static void Main(string[] args)
        {
            Console.WriteLine("->Commands");
            writeCommands();
            while (!Exit) 
            {
                Console.Write("->");
                try
                {
                    processCommand(Console.ReadLine());
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Error! Unknown student");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Error! Wrong arguments");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error! {e.Message}");
                }
                
            }
        }
        static void processCommand(string commandLine) 
        {
            int sIndex = commandLine.IndexOf(' ');
            string command = sIndex != -1 ? commandLine.Substring(0, sIndex).ToLower() : commandLine.ToLower();

            switch (command)
            {
                case "commands":
                    writeCommands();
                    break;
                case "addstudent":
                    if (sIndex == -1)
                        throw new ArgumentException();
                    addStudent(commandLine.Substring(sIndex));
                        break;
                case "getstudents":
                    getStudents();
                    break;
                case "getmarks":
                    if (sIndex == -1)
                        throw new ArgumentException();
                    getMarks(commandLine.Substring(sIndex));
                    break;
                case "addsubject":
                    if (sIndex == -1)
                        throw new ArgumentException();
                    addSubject(commandLine.Substring(sIndex));
                    break;
                case "addmark":
                    if (sIndex == -1)
                        throw new ArgumentException();
                    addMark(commandLine.Substring(sIndex));
                    break;
                case "exit":
                    Exit = true;
                    break;
                case "":
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
        static void writeCommands() 
        {
            Console.WriteLine("Commands - Get info about existing commands");
            Console.WriteLine("AddStudent {FirstName} {SecondName} {Patronymic} {Group} - Create new student");
            Console.WriteLine("AddSubject {StudentIndex} {Subject} - Add subject to student");
            Console.WriteLine("AddMark {StudentIndex} {Subject} {Mark} - Add mark to student");
            Console.WriteLine("GetStudents - Display all students");
            Console.WriteLine("GetMarks {StudentIndex} - Display student marks");
            Console.WriteLine("Exit - exit");
        }
        static void PrintRow(int[] columnSizes, params string[] values) 
        {
            int cursor = 0;
            for (int i = 0; i < values.Length; i++) 
            {
                int size = columnSizes.Length > i ? columnSizes[i] : columnSizes[columnSizes.Length - 1];
                string value;
                if (values[i] != null)
                    value = values[i].Length > size ? values[i].Substring(0, size) : values[i];
                else 
                    value = "";
                Console.CursorLeft = cursor;
                Console.Write(values[i]);
                cursor += size;
                Console.CursorLeft = cursor++;
                Console.Write('|');
            }
            Console.WriteLine();
        }
        static void getStudents() 
        {
            int[] columns = { 10, 20, 20, 20, 10 };
            PrintRow(columns,"Index" ,"First name", "Second name", "Patronymic","Group", "Av. Mark");
            int i = 0;
            foreach (var std in Students) 
            {
                PrintRow(columns, i++.ToString(), std.FirstName, std.SecondName, std.Patronymic, std.Group, Math.Round(std.GetMark(), 2).ToString());
            }
        }
        static void addStudent(string line) 
        {
            string[] args = line.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 4)
                throw new ArgumentException();
            Students.Add(new Student(args[0], args[1], args[2], args[3]));
            Console.WriteLine($"New student has index {Students.Count - 1}");
        }
        static void addSubject(string line)
        {
            string[] args = line.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 2 || !int.TryParse(args[0], out int index))
                throw new ArgumentException();
            Students[index].AddSubject(args[1]);
        }
        static void addMark(string line)
        {
            string[] args = line.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 3 || !int.TryParse(args[0], out int index) || !int.TryParse(args[2], out int mark))
                throw new ArgumentException();
            Students[index].AddMark(args[1], mark);
        }
        static void getMarks(string line)
        {
            string[] args = line.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 1 || !int.TryParse(args[0], out int index))
                throw new ArgumentException();
            var subjects = Students[index].GetSubjects().ToArray();
            int max = 0;
            List<int[]> marks = new List<int[]>();
            foreach (var subject in subjects)
            {
                var subjectMarks = Students[index].GetMarks(subject).ToArray();
                marks.Add(subjectMarks);
                if (subjectMarks.Length > max)
                    max = subjectMarks.Length;
            }
            int[] rowDef = new int[] { 20 };
            int[] sumMarks = new int[subjects.Length];
            PrintRow(rowDef, subjects);
            string[] row = new string[subjects.Length];
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = marks[j].Length > i ? marks[j][i].ToString() : "[]";
                    sumMarks[j] += marks[j].Length > i ? marks[j][i] : 0;
                }
                PrintRow(rowDef, row);
            }
            PrintRow(rowDef, new string[rowDef.Length]);
            for (int i = 0; i < subjects.Length; i++)
                row[i] = sumMarks[i].ToString();
            PrintRow(rowDef, row);
        }

    }
}
