using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HW7
{
    public class Generator
    {
        public bool AutoProps { get; set; } = true;
        public List<string> Namespaces { get; private set; } = new List<string>();
        public override string ToString()
        {
            return base.ToString();
        }
        public void WriteLine(StringBuilder sb, int level) 
        {
            sb.Append('\n');
            for (int i = 0; i < level; i++)
                sb.Append('\t');
        }
        public void WriteType(StringBuilder sb, Type t)
        {
            if (!Namespaces.Contains(t.Namespace))
                Namespaces.Add(t.Namespace);
            if (t.IsGenericType)
            {
                sb.Append(t.Name.Substring(0, t.Name.IndexOf('`')));
                sb.Append('<');
                for (int i = 0; i < t.GenericTypeArguments.Length; i++)
                {
                    WriteType(sb, t.GenericTypeArguments[i]);
                    if (i + 1 < t.GenericTypeArguments.Length)
                        sb.Append(", ");
                }
                sb.Append('>');
            }
            else
                sb.Append(t.Name);
        }
        public void WriteProp(StringBuilder sb, PropertyInfo prop, int level)
        {
            bool isPublic = prop.CanWrite && prop.SetMethod.IsPublic;
            isPublic = isPublic || prop.CanRead && prop.GetMethod.IsPublic;

            if (isPublic)
                sb.Append("public ");
            WriteType(sb, prop.PropertyType);

            sb.Append($" {prop.Name}");
            if (AutoProps)
                sb.Append(" { ");
            else
            {
                WriteLine(sb, level);
                sb.Append("{");
                WriteLine(sb, level + 1);
            }
            if (prop.CanRead)
            {
                WritePropMethod(sb, prop.GetMethod);
                if (!AutoProps)
                {
                    if (prop.CanWrite)
                        WriteLine(sb, level + 1);
                }
                else
                    if (prop.CanWrite)
                    sb.Append(' ');
            }
            if (prop.CanWrite)
                WritePropMethod(sb, prop.SetMethod);
            if (AutoProps)
            {
                sb.Append(' ');
            }
            else
                WriteLine(sb, level);
            sb.Append('}');

        }
        public void WritePropMethod(StringBuilder sb, MethodInfo method)
        {
            if (method.IsPrivate)
                sb.Append("private ");
            if (method.ReturnType == typeof(void))
            {
                sb.Append("set");
            }
            else
            {
                sb.Append("get");
            }
            if (!AutoProps)
                sb.Append(" => throw new NotImplementedException();");
            else
                sb.Append(';');
        }
        public void WriteNamespaces(StringBuilder sb)
        {
            foreach (var ns in Namespaces)
                sb.AppendLine($"using {ns};");
        }
        public void WriteMethod(StringBuilder sb, MethodInfo method, int level)
        {
            if (method.Attributes.HasFlag(MethodAttributes.Public))
                sb.Append("public ");
            if (method.Attributes.HasFlag(MethodAttributes.Static))
                sb.Append("static ");
            if (method.Attributes.HasFlag(MethodAttributes.Virtual))
                sb.Append("virtual ");

            if (method.ReturnType == typeof(void))
                sb.Append("void ");
            else 
            {
                WriteType(sb, method.ReturnType);
                sb.Append(' ');
            }

            sb.Append(method.Name);
            sb.Append('(');
            var parameters = method.GetParameters();
            for(int i = 0; i < parameters.Length; i++)
            {
                WriteType(sb, parameters[i].ParameterType);
                sb.Append($" {parameters[i].Name}");
                if (i + 1 < parameters.Length)
                    sb.Append(", ");
            }
            sb.Append(")");
            WriteLine(sb, level);
            sb.Append('{');
            WriteLine(sb, level + 1);
            sb.Append("throw new NotImplementedException();");
            WriteLine(sb, level);
            sb.Append('}');
        }
        public void WriteProps(StringBuilder sb, Type t, int level) 
        {
            foreach (var prop in t.GetProperties())
            {
                WriteProp(sb, prop, level);
                WriteLine(sb, level);
            }
        }
        public void WriteMethods(StringBuilder sb, Type t, int level)
        {
            var methods = t.GetMethods().Where(t =>
                t.Attributes.HasFlag(MethodAttributes.Public) &&
                !t.Attributes.HasFlag(MethodAttributes.SpecialName) &&
                t.GetBaseDefinition() == t
            );
            foreach (var method in methods)
            {
                WriteMethod(sb, method, level);
                WriteLine(sb, level);
            }
        }
        public void WriteClass(StringBuilder sb, Type t, int level)
        {
            sb.Append($"class {t.Name}");
            WriteLine(sb, level);
            sb.Append('{');
            WriteLine(sb,level + 1);
            WriteProps(sb, t, level + 1);
            WriteMethods(sb, t, level + 1);
            WriteLine(sb, level);
            sb.Append('}');

        }
        public string WriteClassWithNS(Type t) 
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder body = new StringBuilder();

            WriteClass(body, t, 1);
            WriteNamespaces(sb);
            sb.Append('\n');
            sb.Append($"namespace {t.Namespace}\n{{\n\t{body}\n}}");
            return sb.ToString();
        }
    }
}
