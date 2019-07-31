using NittyGritty.Uwp.Activation.Operations;
using NittyGritty.Uwp.Activation.Operations.Jobs;
using NittyGritty.Uwp.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class FileOperation : IViewOperation<FileActivatedEventArgs>
    {
        private readonly Dictionary<string, FileConfiguration> configurations;

        /// <summary>
        /// Creates a FileOperation to handle the file that activated the app
        /// </summary>
        /// <param name="verb">The verb this FileOperation can handle. Cannot be null, empty, or whitespace
        /// A value of open means the FileOperation is activated through file launch</param>
        /// <param name="strategy">Specifies what strategy the file operation will use</param>
        public FileOperation(string verb, FileOperationStrategy strategy, params FileConfiguration[] configurations) : base()
        {
            if(string.IsNullOrWhiteSpace(verb))
            {
                throw new ArgumentException("Verb cannot be null, empty, or whitespace.", nameof(verb));
            }

            Verb = verb;
            Strategy = strategy;
            this.configurations = new Dictionary<string, FileConfiguration>();
            foreach (var configuration in configurations)
            {
                this.configurations.Add(configuration.FileType, configuration);
            }
        }

        public string Verb { get; }

        public FileOperationStrategy Strategy { get; }

        public virtual async Task Run(FileActivatedEventArgs args, Frame frame)
        {
            if(args.Verb != Verb)
            {
                return;
            }

            var files = new List<IStorageFile>();
            if(args.Files[0] is IStorageFolder folder)
            {
                // a folder is passed
                files.AddRange(await folder.GetFilesAsync());
            }
            else
            {
                // one or more files are passed
                files.AddRange(args.Files.OfType<IStorageFile>());
            }

            var groupedFiles = files.GroupBy(f => f.FileType).ToDictionary(k => k.Key, v => v.ToList());
            if (Strategy == FileOperationStrategy.Single)
            {
                if(groupedFiles.Count == 0)
                {
                    var config = GetConfiguration(groupedFiles.First().Key);
                    var payload = new FilePayload(Verb, groupedFiles.First().Value, config.Processor);
                    await config.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
                }
                else
                {
                    var config = GetConfiguration("*");
                    var payload = new FilePayload(Verb, files, config.Processor);
                    await config.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
                }
            }
            else if(Strategy == FileOperationStrategy.Group)
            {   
                foreach (var key in groupedFiles.Keys)
                {
                    var config = GetConfiguration(key);
                    var payload = new FilePayload(Verb, groupedFiles[key], config.Processor);
                    await config.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
                }
            }
            else if(Strategy == FileOperationStrategy.Unqiue)
            {
                foreach (var file in files)
                {
                    var config = GetConfiguration(file.FileType);
                    var payload = new FilePayload(Verb, new[] { file }, config.Processor);
                    await config.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
                }
            }

            //var filesData = new Dictionary<string, List<object>>();
            //foreach (var file in args.Files)
            //{
            //    if (file is IStorageFile storageFile)
            //    {
            //        var d = await ProcessFile(storageFile);
            //        if (filesData.TryGetValue(storageFile.FileType, out var fileData))
            //        {
            //            fileData.Add(d);
            //        }
            //        else
            //        {
            //            filesData.Add(storageFile.FileType, new List<object>() { d });
            //        }
            //    }
            //    else if (file is IStorageFolder folder)
            //    {
            //        // TODO: fetch files in folder and operate on them
            //        var folderFiles = await folder.GetFilesAsync();
            //        foreach (var item in folderFiles)
            //        {
            //            var d = await ProcessFile(item);
            //            if (filesData.TryGetValue(item.FileType, out var fileData))
            //            {
            //                fileData.Add(d);
            //            }
            //            else
            //            {
            //                filesData.Add(item.FileType, new List<object>() { d });
            //            }
            //        }
            //    }
            //}

            //foreach (var fileType in filesData.Keys)
            //{
            //    var payload = new FilePayload(Verb, fileType, filesData[fileType]);

            //    var configuration = configurations[fileType];
            //    await configuration.Run(payload, args.CurrentlyShownApplicationViewId, frame);
            //}
        }

        //private async Task<object> ProcessFile(IStorageFile file)
        //{
        //    if(configurations.TryGetValue(file.FileType, out var config))
        //    {
        //        return await config.Processor.Process(file);
        //    }
        //    else
        //    {
        //        if(configurations.TryGetValue("*", out var fallbackConfig))
        //        {
        //            return await fallbackConfig.Processor.Process(file);
        //        }
        //        else
        //        {
        //            throw new ArgumentException(
        //                string.Format(
        //                    "No configuration for file type: {0}. Did you forget to call FileOperation.Configure?",
        //                    file.FileType));
        //        }
        //    }
        //}

        protected FileConfiguration GetConfiguration(string fileType)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(fileType, out var config))
                {
                    return config;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackConfig))
                    {
                        return fallbackConfig;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No configuration for file type: {0}. Did you forget to call FileOperation.Configure?",
                                fileType),
                            nameof(fileType));
                    }
                }
            }
        }
    }
}
