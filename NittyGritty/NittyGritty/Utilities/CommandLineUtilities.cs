using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class CommandLineUtilities
    {
        public static ReadOnlyCollection<ParsedCommand> Parse(string cmdArgs = null)
        {
            var parsedArgs = new List<ParsedCommand>();
            string[] args = cmdArgs.Split(' ');

            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-") || args[i].StartsWith("/"))
                    {
                        var data = ParseData(args, i);
                        if (data != null)
                        {
                            for (int j = 0; j < parsedArgs.Count; j++)
                            {
                                if (parsedArgs[j].Command == data.Command)
                                {
                                    parsedArgs.RemoveAt(j);
                                }
                            }
                            parsedArgs.Add(data);
                        }
                    }
                }
            }
            return new ReadOnlyCollection<ParsedCommand>(parsedArgs);
        }

        private static ParsedCommand ParseData(string[] args, int index)
        {
            string command = null;
            string parameter = null;
            if (args[index].StartsWith("-") || args[index].StartsWith("/"))
            {
                if (args[index].Contains(":"))
                {
                    string argument = args[index];
                    int endIndex = argument.IndexOf(':');
                    command = argument.Substring(1, endIndex - 1);   // trim the '/' and the ':'.
                    int valueStart = endIndex + 1;
                    parameter = valueStart < argument.Length ? argument.Substring(
                        valueStart, argument.Length - valueStart) : null;
                }
                else
                {
                    command = args[index];
                    int argIndex = 1 + index;
                    if (argIndex < args.Length && !(args[argIndex].StartsWith("-") || args[argIndex].StartsWith("/")))
                    {
                        parameter = args[argIndex];
                    }
                    else
                    {
                        parameter = null;
                    }
                }
            }

            return command != null ? new ParsedCommand(command, parameter) : null;
        }
    }
}
