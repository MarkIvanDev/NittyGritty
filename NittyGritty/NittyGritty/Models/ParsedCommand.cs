using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ParsedCommand
    {
        public ParsedCommand(string command, QueryString parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        public string Command { get; }

        public QueryString Parameters { get; }
    }
}
