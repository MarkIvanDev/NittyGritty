using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NittyGritty.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, ICancellationManager, IStateManager, ITaskManager
    {

        private string _title;

        /// <summary>
        /// Gets or sets the title of the view model.
        /// </summary>
        public virtual string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }


        private int _loadingCounter = 0;

        /// <summary>Gets or sets a value indicating whether the view model is currently loading. </summary>
        public bool IsLoading
        {
            get { return _loadingCounter > 0; }
            set
            {
                if (value)
                    _loadingCounter++;
                else if (_loadingCounter > 0)
                    _loadingCounter--;

                RaisePropertyChanged();
            }
        }

        #region Cancellation Manager

        public Collection<CancellationTokenSource> CancellationTokenSources { get; private set; }

        /// <summary>Registers a <see cref="CancellationTokenSource"/> which will be cancelled when cleaning up the view model. </summary>
        /// <param name="cancellationTokenSource"></param>
        public void RegisterCancellationTokenSource(CancellationTokenSource cancellationTokenSource)
        {
            if (CancellationTokenSources == null)
                CancellationTokenSources = new Collection<CancellationTokenSource>();

            CancellationTokenSources.Add(cancellationTokenSource);
        }

        /// <summary>Creates a <see cref="CancellationTokenSource"/> and registers it if not disabled. </summary>
        public CancellationTokenSource CreateCancellationTokenSource()
        {
            var token = new CancellationTokenSource();
            RegisterCancellationTokenSource(token);
            return token;
        }

        /// <summary>Disposes and deregisters a <see cref="CancellationTokenSource"/>. 
        /// Should be called when the task has finished cleaning up the view model. </summary>
        /// <param name="cancellationTokenSource"></param>
        public void DeregisterCancellationTokenSource(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
            catch { }
            CancellationTokenSources.Remove(cancellationTokenSource);
        }

        public void CancelAll()
        {
            if (CancellationTokenSources != null)
            {
                foreach (var cancellationTokenSource in CancellationTokenSources)
                    DeregisterCancellationTokenSource(cancellationTokenSource);
            }
        }

        #endregion

        #region Task Manager

        /// <summary>Runs a task and correctly updates the <see cref="IsLoading"/> property, 
        /// throws exceptions that are caught, 
        /// and automatically creates and registers a cancellation token source. </summary>
        /// <param name="task">The task to run. </param>
        /// <returns>The awaitable task. </returns>
        public async Task<TResult> Run<TResult>(Func<CancellationToken, Task<TResult>> task)
        {
            TResult result = default(TResult);
            var tokenSource = CreateCancellationTokenSource();
            try
            {
                IsLoading = true;
                result = await task(tokenSource.Token);
                IsLoading = false;
            }
            catch (OperationCanceledException)
            {
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
                throw;
            }
            DeregisterCancellationTokenSource(tokenSource);
            return result;
        }

        /// <summary>Runs a task and correctly updates the <see cref="IsLoading"/> property, 
        /// throws exceptions that are caught. </summary>
        /// <param name="task">The task to run. </param>
        /// <returns>The awaitable task. </returns>
        public async Task<TResult> Run<TResult>(Task<TResult> task)
        {
            TResult result = default(TResult);
            try
            {
                IsLoading = true;
                result = await task;
                IsLoading = false;
            }
            catch (OperationCanceledException)
            {
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
                throw;
            }
            return result;
        }

        /// <summary>Runs a task and correctly updates the <see cref="IsLoading"/> property, 
        /// throws exceptions that are caught,
        /// and automatically creates and registers a cancellation token source. </summary>
        /// <param name="task">The task to run. </param>
        /// <returns>The awaitable task. </returns>
        public async Task Run(Func<CancellationToken, Task> task)
        {
            var tokenSource = CreateCancellationTokenSource();
            try
            {
                IsLoading = true;
                await task(tokenSource.Token);
                IsLoading = false;
            }
            catch (OperationCanceledException)
            {
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
                throw;
            }
            DeregisterCancellationTokenSource(tokenSource);
        }

        /// <summary>Runs a task and correctly updates the <see cref="IsLoading"/> property, 
        /// throws exceptions that are caught. </summary>
        /// <param name="task">The task to run. </param>
        /// <returns>The awaitable task. </returns>
        public async Task Run(Task task)
        {
            try
            {
                IsLoading = true;
                await task;
                IsLoading = false;
            }
            catch (OperationCanceledException)
            {
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
                throw;
            }
        }

        #endregion

        #region State Management

        public virtual void OnLoading()
        {

        }

        public virtual void LoadState(object parameter, Dictionary<string, object> state)
        {
            OnLoading();
        }

        public virtual void OnSaving()
        {
            CancelAll();
        }

        public virtual void SaveState(Dictionary<string, object> state)
        {
            OnSaving();
        }

        public T RestoreStateItem<T>(Dictionary<string, object> state, string stateKey, T defaultValue = default(T))
        {
            T value = state != null && 
                state.ContainsKey(stateKey) && 
                state[stateKey] != null && 
                state[stateKey] is T ? (T)state[stateKey] : defaultValue;
            state?.Remove(stateKey);
            return value;
        }

        #endregion
    }
}
