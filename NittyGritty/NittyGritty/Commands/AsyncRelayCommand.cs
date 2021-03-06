﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Commands
{
    public class AsyncRelayCommand : CommandBase
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>Initializes a new instance of the <see cref="AsyncRelayCommand"/> class. </summary>
        /// <param name="execute">The function to execute. </param>
        public AsyncRelayCommand(Func<Task> execute)
            : this(execute, null) { }


        /// <summary>Initializes a new instance of the <see cref="AsyncRelayCommand"/> class. </summary>
        /// <param name="execute">The function. </param>
        /// <param name="canExecute">The predicate to check whether the function can be executed. </param>
        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected override async void Execute()
        {
            await TryExecute();
        }

        public async Task<bool> TryExecute()
        {
            if (CanExecute)
            {
                try
                {
                    SetRunning(true);
                    await _execute();
                }
                finally
                {
                    SetRunning(false);
                }
                return true;
            }
            return false;
        }

        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public override bool CanExecute
        {
            get { return !IsRunning && (_canExecute == null || _canExecute()); }
        }
    }

    public class AsyncRelayCommand<T> : CommandBase<T>
    {
        private readonly Func<T, Task> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>Initializes a new instance of the <see cref="AsyncRelayCommand"/> class. </summary>
        /// <param name="execute">The function. </param>
        public AsyncRelayCommand(Func<T, Task> execute)
            : this(execute, null) { }


        /// <summary>Initializes a new instance of the <see cref="AsyncRelayCommand"/> class. </summary>
        /// <param name="execute">The function. </param>
        /// <param name="canExecute">The predicate to check whether the function can be executed. </param>
        public AsyncRelayCommand(Func<T, Task> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public override bool CanExecute(T parameter)
        {
            return !IsRunning && (_canExecute == null || _canExecute(parameter));
        }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected override async void Execute(T parameter)
        {
            await TryExecute(parameter);
        }

        public async Task<bool> TryExecute(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    SetRunning(true);
                    await _execute(parameter);
                }
                finally
                {
                    SetRunning(false);
                }
                return true;
            }
            return false;
        }
    }
}
