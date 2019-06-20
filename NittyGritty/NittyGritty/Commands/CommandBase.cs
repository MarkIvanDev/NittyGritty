using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NittyGritty.Commands
{
    public abstract class CommandBase : ObservableObject, ICommand
    {
        /// <summary>
        /// Gets or sets a value indicating whether the command is currently running.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public abstract bool CanExecute { get; }

        /// <summary>Defines the method to be called when the command is invoked. </summary>
        protected abstract void Execute();

        /// <summary>Triggers the CanExecuteChanged event and a property changed event on the CanExecute property. </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            RaisePropertyChanged(nameof(CanExecute));

            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute. </summary>
        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        protected void SetRunning(bool isRunning)
        {
            IsRunning = isRunning;
            RaisePropertyChanged(nameof(IsRunning));
            RaiseCanExecuteChanged();
        }
    }

    public abstract class CommandBase<T> : ObservableObject, ICommand
    {
        
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the command is currently running.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>Gets a value indicating whether the command can execute in its current state. </summary>
        public abstract bool CanExecute(T parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        protected abstract void Execute(T parameter);

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        protected void SetRunning(bool isRunning)
        {
            IsRunning = isRunning;
            RaisePropertyChanged(nameof(IsRunning));
            RaiseCanExecuteChanged();
        }

        /// <summary>Triggers the CanExecuteChanged event. </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            RaisePropertyChanged(nameof(CanExecute));

            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute. </summary>
        public event EventHandler CanExecuteChanged;
    }
}
