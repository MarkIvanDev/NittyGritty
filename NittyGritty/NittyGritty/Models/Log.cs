using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class Log : ObservableObject
    {
        public Log(LogLevel level, string message, string source)
        {
            Level = level;
            Message = message;
            Source = source;
        }

        private LogLevel _level;

        public LogLevel Level
        {
            get { return _level; }
            set { Set(ref _level, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { Set(ref _source, value); }
        }

    }

    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }
}
