using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Talysoft.IO
{
    /// <summary>
    /// Allows for the serialization and deserialization to .csv files.
    /// </summary>
    public static class CSV
    {
        private const char SEPARATOR = ',';

        public static string Serialize<T>(T[] ts)
        {
            //get public variables, or ones marked with the CSVAttribute
            List<PropertyInfo> properties = new List<PropertyInfo>();
            PropertyInfo[] tempProperties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(pi => pi.GetCustomAttribute<CSVAttribute>() != null).ToArray();
            properties.AddRange(tempProperties);
            tempProperties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).Where(pi => pi.GetCustomAttribute<CSVAttribute>() != null).ToArray();
            properties.AddRange(tempProperties);

            List<FieldInfo> fields = new List<FieldInfo>();
            FieldInfo[] tempFields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public).Where(fi => fi.GetCustomAttribute<CSVAttribute>() != null).ToArray();
            fields.AddRange(tempFields);
            tempFields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(fi => fi.GetCustomAttribute<CSVAttribute>() != null).ToArray();
            fields.AddRange(tempFields);

            //combine and sort
            List<MemberInfo> members = new List<MemberInfo>();
            members.AddRange(properties);
            members.AddRange(fields);
            members = members.OrderBy(m => m.GetCustomAttribute<CSVAttribute>().Order).ToList();

            //get how many total we have
            int count = members.Count;

            string[] headers = new string[count];

            //populate the headers
            for(int i = 0; i < count; i++)
            {
                headers[i] = members[i].Name;
            }

            //populate the lines, using the given data
            List<string[]> lines = new List<string[]>(ts.Length);

            string[] line;

            foreach(T t in ts)
            {
                line = new string[count];

                for (int i = 0; i < count; i++)
                {
                    if(members[i] is FieldInfo fi)
                    {
                        line[i] = ToString(fi.GetValue(t));
                    } else if (members[i] is PropertyInfo pi)
                    {
                        line[i] = ToString(pi.GetValue(t, null));
                    }
                }

                lines.Add(line);
            }

            //now we have all the headers and the lines, so compile it into a string
            StringBuilder sb = new StringBuilder();

            //headers
            sb.AppendLine(string.Join(SEPARATOR.ToString(), headers));

            //content
            foreach(string[] s in lines)
            {
                sb.AppendLine(string.Join(SEPARATOR.ToString(), s));
            }

            //return
            return sb.ToString();
        }

        private static string ToString(object o)
        {
            string str = o.ToString();

            //if contains any commas, surround it in " "
            if(str.Contains(SEPARATOR))
            {
                return $"\"{str.Replace("\"", "\\\"")}\"";
            }

            return str;
        }

        private static string FromString(string s)
        {
            if(s.StartsWith("\"") && s.EndsWith("\""))
            {
                //it was padded, so unpad it
                return s.Substring(1, s.Length - 2).Replace("\\\"", "\"");
            } else
            {
                return s;
            }
        }

        public static T Deserialize<T>(string text)
        {
            throw new NotImplementedException();
        }
    }
}
