using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using NittyGritty.Extensions;
using NittyGritty.Uwp.Activation.Operations;

namespace NittyGritty.Uwp.Services
{
    public class BackgroundTaskService
    {
        private readonly Dictionary<string, BackgroundTask> backgroundTasks = new Dictionary<string, BackgroundTask>();

        public void Add(BackgroundTask task)
        {
            if (!backgroundTasks.TryAdd(task.Name, task))
            {
                throw new ArgumentException("You only have to register for a background task once");
            }
        }

        public async Task Register()
        {
            BackgroundExecutionManager.RemoveAccess();
            var result = await BackgroundExecutionManager.RequestAccessAsync();

            if (result == BackgroundAccessStatus.DeniedBySystemPolicy
                || result == BackgroundAccessStatus.DeniedByUser)
            {
                return;
            }

            var taskNames = BackgroundTaskRegistration.AllTasks.Select(t => t.Value.Name).ToList();
            foreach (var task in backgroundTasks)
            {
                if (!taskNames.Contains(task.Value.Name))
                {
                    task.Value.Register(
                        new BackgroundTaskBuilder()
                        {
                            Name = task.Value.Name
                        });
                }
            }
        }

        public void HandleActivation(BackgroundActivatedEventArgs args)
        {
            if (backgroundTasks.TryGetValue(args.TaskInstance?.Task?.Name ?? string.Empty, out var task))
            {
                task.Run(args.TaskInstance).FireAndForget();
            }
        }
    }
}
