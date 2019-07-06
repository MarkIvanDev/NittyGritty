using NittyGritty.Extensions;
using NittyGritty.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;

namespace NittyGritty.Uwp.Services.Activation
{
    public class BackgroundActivationHandler : ActivationHandler<BackgroundActivatedEventArgs>
    {

        public BackgroundActivationHandler()
        {
            Handler = async (e) =>
            {
                Start(e.TaskInstance);
                await Task.CompletedTask;
            };
        }

        private static List<BackgroundTask> backgroundTasks = new List<BackgroundTask>();

        public static BackgroundTaskRegistration GetBackgroundTasksRegistration<T>()
            where T : BackgroundTask
        {
            if (!BackgroundTaskRegistration.AllTasks.Any(t => t.Value.Name == typeof(T).Name))
            {
                // This condition should not be met. If it is, it means the background task was not registered correctly.
                // Please check CreateInstances to see if the background task was properly added to the BackgroundTasks property.
                return null;
            }

            return (BackgroundTaskRegistration)BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == typeof(T).Name).Value;
        }



        public async Task Register(params BackgroundTask[] tasks)
        {
            BackgroundExecutionManager.RemoveAccess();
            var result = await BackgroundExecutionManager.RequestAccessAsync();

            if (result == BackgroundAccessStatus.DeniedBySystemPolicy
                || result == BackgroundAccessStatus.DeniedByUser)
            {
                return;
            }

            foreach (var task in tasks)
            {
                backgroundTasks.Add(task);
            }

            foreach (var task in backgroundTasks)
            {
                task.Register();
            }
        }

        public void Start(IBackgroundTaskInstance taskInstance)
        {
            var task = backgroundTasks.FirstOrDefault(b => b.Match(taskInstance?.Task?.Name));

            if (task == null)
            {
                // This condition should not be met. If it is, it means the background task to start was not found in the background tasks managed by this service.
                // Please check CreateInstances to see if the background task was properly added to the BackgroundTasks property.
                return;
            }

            task.RunAsync(taskInstance).FireAndForget();
        }

    }
}
