using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Views.Payloads
{
    public class CommandLinePayload
    {
        public CommandLinePayload(string command, string currentDirectory, QueryString parameters)
        {
            Command = command;
            CurrentDirectory = currentDirectory;
            Parameters = parameters;
        }

        public string Command { get; }

        public string CurrentDirectory { get; }

        public QueryString Parameters { get; }
    }
}
