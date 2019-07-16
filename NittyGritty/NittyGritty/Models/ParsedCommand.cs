using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ParsedCommand
    {
        public ParsedCommand(string command, string parameter)
        {
            Command = command;
            Parameter = parameter;
        }

        public string Command { get; }

        public string Parameter { get; }
    }
}
