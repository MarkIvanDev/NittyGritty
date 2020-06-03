using NittyGritty.Models;
using NittyGritty.Uwp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Extensions.Activation
{
    public static class LaunchExtensions
    {
        public static bool IsPrimary(this LaunchActivatedEventArgs args)
        {
            return args.TileId == "App" && string.IsNullOrEmpty(args.Arguments);
        }

        public static bool IsSecondary(this LaunchActivatedEventArgs args)
        {
            return !string.IsNullOrEmpty(args.TileId) && args.TileId != "App";
        }

        public static bool IsJumplist(this LaunchActivatedEventArgs args)
        {
            return args.TileId == "App" && !string.IsNullOrEmpty(args.Arguments);
        }

        public static bool IsChaseable(this LaunchActivatedEventArgs args)
        {
            return !(args.TileActivatedInfo is null) && args.TileActivatedInfo.RecentlyShownNotifications.Count > 0;
        }

        public static LaunchSource GetLaunchSource(this LaunchActivatedEventArgs args)
        {
            if (args.IsPrimary())
            {
                return LaunchSource.Primary;
            }
            else if (args.IsSecondary())
            {
                return LaunchSource.Secondary;
            }
            else if (args.IsJumplist())
            {
                return LaunchSource.Jumplist;
            }
            else if (args.IsChaseable())
            {
                return LaunchSource.Chaseable;
            }
            else
            {
                return LaunchSource.Primary;
            }
        }

        public static void Process(this LaunchActivatedEventArgs args, LaunchActivationOptions options)
        {
            var source = args.GetLaunchSource();
            switch (source)
            {
                case LaunchSource.Primary:
                default:
                    options.Frame.Navigate(options.PrimaryView);
                    break;
                case LaunchSource.Secondary:
                    options.Frame.Navigate(
                        options.ArgumentService.GetValue(
                            args.Arguments, options.ActionKey)
                            ?? options.PrimaryView,
                        args.Arguments);
                    break;
                case LaunchSource.Jumplist:
                    options.Frame.Navigate(
                        options.ArgumentService.GetValue(
                            args.Arguments, options.ActionKey)
                            ?? options.PrimaryView,
                        args.Arguments);
                    break;
                case LaunchSource.Chaseable:
                    options.Frame.Navigate(
                        options.ArgumentService.GetValue(
                            args.TileActivatedInfo.RecentlyShownNotifications[0].Arguments, options.ActionKey)
                            ?? options.PrimaryView,
                        args.Arguments);
                    break;
            }
        }
    }

    public enum LaunchSource
    {
        Primary = 0,
        Secondary = 1,
        Jumplist = 2,
        Chaseable = 3
    }

    public class LaunchActivationOptions
    {
        public Frame Frame { get; set; }

        public Type PrimaryView { get; set; }

        public string ActionKey { get; set; }

        public ArgumentService ArgumentService { get; set; }
    }
}
