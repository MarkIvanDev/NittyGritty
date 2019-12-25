using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        public RelayCommand(Action execute)
            : this(execute, null) { }

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        /// <param name="canExecute">The predicate to check whether the function can be executed. </param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected override void Execute()
        {
            SetRunning(true);
            _execute();
            SetRunning(false);
        }

        public bool TryExecute()
        {
            if (CanExecute)
            {
                SetRunning(true);
                _execute();
                SetRunning(false);
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

    public class RelayCommand<T> : CommandBase<T>
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        public RelayCommand(Action<T> execute)
            : this(execute, null) { }

        /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class. </summary>
        /// <param name="execute">The action to execute. </param>
        /// <param name="canExecute">The predicate to check whether the function can be executed. </param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
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
        protected override void Execute(T parameter)
        {
            SetRunning(true);
            _execute(parameter);
            SetRunning(false);
        }

        public bool TryExecute(T parameter)
        {
            if (CanExecute(parameter))
            {
                SetRunning(true);
                _execute(parameter);
                SetRunning(false);
                return true;
            }
            return false;
        }
    }
}
