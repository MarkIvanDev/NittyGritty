using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Views
{
    public interface IDialogService : IViewConfigurable<Type>
    {

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button with the text "OK".
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowMessage(
            string message,
            string title);

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowMessage(
            string message,
            string title,
            string buttonText);

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonConfirmText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="buttonCancelText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the text "Cancel" will be used.</param>
        /// <returns>A Task allowing this async method to be awaited. The task will return
        /// true or false depending on the dialog result.</returns>
        Task<bool> ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText);

        /// <summary>
        /// Displays a custom dialog corresponding to the key
        /// </summary>
        /// <param name="key">The key corresponding to the dialog that will be displayed</param>
        /// <returns>A Task allowing this async method to be awaited. The task will return
        /// true or false depending on the dialog result.</returns>
        Task<bool> Show(string key);

        /// <summary>
        /// Displays a custom dialog corresponding to the key and sets its data context using the parameter
        /// </summary>
        /// <param name="key">The key corresponding to the dialog that will be displayed</param>
        /// <param name="parameter">The object that the dialog will use as data context</param>
        /// <returns>A Task allowing this async method to be awaited. The task will return
        /// true or false depending on the dialog result.</returns>
        Task<bool> Show(string key, object parameter);

        /// <summary>
        /// Hides all the open dialogs.
        /// </summary>
        void HideAll();
    }
}
