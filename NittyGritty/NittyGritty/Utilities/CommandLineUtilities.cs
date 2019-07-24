using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class CommandLineUtilities
    {
        public static (string command, Dictionary<string, ReadOnlyCollection<string>> parameters) Parse(string cmdArgs = null)
        {
            var command = string.Empty;
            var parameters = new Dictionary<string, ReadOnlyCollection<string>>();

            if(cmdArgs == null)
            {
                return (command, parameters);
            }
            
            string[] args = cmdArgs.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    var part = args[i].Trim();
                    if(i == 0 && !args[i].StartsWith("-") && !args[i].StartsWith("/"))
                    {
                        if(part.Length > 0)
                        {
                            command = part;
                        }
                    }
                    else if (args[i].StartsWith("-") || args[i].StartsWith("/"))
                    {
                        var (key, values) = ParseData(args, ref i);
                        if (key != null)
                        {
                            parameters[key] = values;
                        }
                    }
                }
            }
            return (command, parameters);
        }

        private static (string key, ReadOnlyCollection<string> values) ParseData(string[] args, ref int index)
        {
            string key = null;
            var values = new List<string>();
            for (int i = index; i < args.Length; i++)
            {
                string value = null;

                if (args[i].StartsWith("-") || args[i].StartsWith("/"))
                {   
                    // we are at the start of the segment
                    if(i == index)
                    {
                        // the value is at the end of the part after the :
                        if(args[i].Contains(":"))
                        {
                            string argument = args[index];
                            int endIndex = argument.IndexOf(':');
                            key = argument.Substring(1, endIndex - 1);   // trim the '/' and the ':'
                            int valueStart = endIndex + 1;
                            value = valueStart < argument.Length ? argument.Substring(
                                valueStart, argument.Length - valueStart) : null;
                            if(value != null)
                            {
                                values.Add(value);
                            }
                        }
                        // the value is the part of this segment
                        else
                        {
                            key = args[i].Substring(1);     // trim the '/' and the ':'
                        }
                    }
                    // we have reached the next segment
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }
                else
                {
                    if(args[i].Length > 0)
                    {
                        values.Add(args[i]);
                    }
                }
            }
            return (key, new ReadOnlyCollection<string>(values));
        }
    }
}
