using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views.Payloads
{
    public class CommandLinePayload
    {
        public CommandLinePayload(string command, string currentDirectory, QueryString parameter)
        {
            Command = command;
            CurrentDirectory = currentDirectory;
            Parameter = parameter;
        }

        public string Command { get; }

        public string CurrentDirectory { get; }

        public QueryString Parameter { get; }
    }
}
