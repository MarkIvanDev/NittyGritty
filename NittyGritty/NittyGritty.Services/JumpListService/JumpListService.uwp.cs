using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.JumpList;

namespace NittyGritty.Services
{
    public partial class JumpListService
    {
        private readonly List<JumpListItem> jumpListItems = new List<JumpListItem>();

        void PlatformConfigure(JumpListItem item)
        {
            jumpListItems.Add(item);
        }

        async Task PlatformInitialize(JumpListSystemManagedGroup systemManagedGroup)
        {
            var jumpList = await Windows.UI.StartScreen.JumpList.LoadCurrentAsync();
            jumpList.SystemGroupKind = (Windows.UI.StartScreen.JumpListSystemGroupKind)systemManagedGroup;
            jumpList.Items.Clear();

            foreach (var item in jumpListItems)
            {
                var jli = Windows.UI.StartScreen.JumpListItem.CreateWithArguments(item.Arguments, item.DisplayName);
                jli.Description = item.Description;
                jli.GroupName = item.GroupName;
                jli.Logo = item.Logo;

                jumpList.Items.Add(jli);
            }

            await jumpList.SaveAsync();
        }

        async Task PlatformClear()
        {
            var jumpList = await Windows.UI.StartScreen.JumpList.LoadCurrentAsync();
            jumpList.Items.Clear();
            await jumpList.SaveAsync();
        }

    }
}
