using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    /// <summary>
    /// Class used to hold the event data required when a page attempts to load state.
    /// </summary>
    public class LoadStateEventArgs : EventArgs
    {
        /// <summary>
        /// The parameter value passed to <see cref="Navigate(Type, object)"/> 
        /// when this view was initially requested.
        /// </summary>
        public object Parameter { get; private set; }

        /// <summary>
        /// A dictionary of state preserved by this view during an earlier
        /// session.  This will be null the first time a view is visited.
        /// </summary>
        public Dictionary<string, object> State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadStateEventArgs"/> class.
        /// </summary>
        /// <param name="parameter">
        /// The parameter value passed to <see cref="Navigate(Type, Object)"/> 
        /// when this view was initially requested.
        /// </param>
        /// <param name="state">
        /// A dictionary of state preserved by this view during an earlier
        /// session.  This will be null the first time a view is visited.
        /// </param>
        public LoadStateEventArgs(object parameter, Dictionary<string, object> state)
            : base()
        {
            this.Parameter = parameter;
            this.State = state;
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="LoadState"/>event
    /// </summary>
    public delegate void LoadStateEventHandler(object sender, LoadStateEventArgs e);
}
