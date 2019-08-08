using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class CommandLineUtilities
    {
        public static ParsedCommand Parse(string cmdArgs = null)
        {
            var command = string.Empty;
            var parameters = new QueryString();

            if(cmdArgs == null)
            {
                return new ParsedCommand(command, parameters);
            }
            
            string[] args = cmdArgs.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    var part = args[i];
                    if(i == 0 && !part.StartsWith("-") && !part.StartsWith("/"))
                    {
                        if(part.Length > 0)
                        {
                            command = part;
                        }
                    }
                    else if (part.StartsWith("-") || part.StartsWith("/"))
                    {
                        var (key, values) = ParseData(args, ref i);
                        if (key != null)
                        {
                            // Uncomment the following line if you want each subsequent command replaces the values by the same previous command
                            // parameters.RemoveAll(key);
                            if (values.Count == 0)
                            {
                                parameters.Add(key);
                            }
                            else
                            {
                                foreach (var value in values)
                                {
                                    parameters.Add(key, value);
                                }
                            }
                        }
                    }
                    else
                    {
                        // all parts that do not start with - or / will be added to the key of value string.Empty
                        parameters.Add(string.Empty, part);
                    }
                }
            }
            return new ParsedCommand(command, parameters);
        }

        private static (string key, IReadOnlyCollection<string> values) ParseData(string[] args, ref int index)
        {
            string key = null;
            var values = new List<string>();
            for (int i = index; i < args.Length; i++)
            {
                var part = args[i];
                string value = null;

                if (part.StartsWith("-") || part.StartsWith("/"))
                {   
                    // we are at the start of the segment
                    if(i == index)
                    {
                        // the value is at the end of the part after the :
                        if(part.Contains(":"))
                        {
                            string argument = part;
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
                        // the value is not a part of this segment
                        else
                        {
                            key = part.Substring(1);     // trim the '/' and the ':'
                        }
                    }
                    // we have reached the next segment
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }
                // all parts that does not start with - or / will be treated as value for the current key
                else
                {
                    if(part.Length > 0)
                    {
                        values.Add(args[i]);
                    }
                }
            }
            return (key, new ReadOnlyCollection<string>(values));
        }
    }
}
