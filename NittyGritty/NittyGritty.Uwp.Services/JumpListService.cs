using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.JumpList;
using NittyGritty.Services.Core;

namespace NittyGritty.Uwp.Services
{
    public class JumpListService : IJumpListService
    {
        private readonly List<JumpListItem> jumpListItems = new List<JumpListItem>();

        public void Configure(JumpListItem item)
        {
            jumpListItems.Add(item);
        }

        public async Task Initialize(JumpListSystemManagedGroup systemManagedGroup)
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

        public async Task Clear()
        {
            var jumpList = await Windows.UI.StartScreen.JumpList.LoadCurrentAsync();
            jumpList.Items.Clear();
            await jumpList.SaveAsync();
        }
    }
}
