using Acr.UserDialogs;
using System.Threading.Tasks;

namespace CommonClasses.Forms.Helpers
{
    public static class DialogsHelper
    {
        private static IProgressDialog _loading;
        private static IUserDialogs _dialogs;

        public static void Init(IUserDialogs dialogs)
        {
            _dialogs = dialogs;
        }

        public static void ShowMessage(string title, string message)
        {
            _dialogs.Alert(message, title);
        }

        public static void ShowMessage(string message)
        {
            _dialogs.Alert(message, "");
        }

        public static async Task ShowMessageAsync(string message)
        {
            if (_dialogs == null)
                return;
            await _dialogs.AlertAsync(message, "");
        }
        public static async Task<bool> AskUserAsync(string title, string message, string okLabel=null, string cancelLabel=null)
        {
            return await _dialogs.ConfirmAsync(message, title,okLabel,cancelLabel);
        }

        public static void ShowLoading(string title = null)
        {
            if (_dialogs == null)
                return;
            _loading = _dialogs.Loading(title);
            _loading.Show();
        }

        public static void UpdateLoading(string title)
        {
            if (_loading != null)
            {
                _loading.Title=title;
            }
        }

        public static void HideLoading()
        {
            if (_loading != null)
            {
                _loading.Hide();
            }
        }

        public static void ShowSuccess(string toast = "done!")
        {
            _dialogs.SuccessToast(toast);
        }

        public static async Task<string> ShowOptions(string title, string cancel, string distructive, params string[] buttons)
        {
            return await _dialogs.ActionSheetAsync(title, cancel, distructive, buttons);
        }
    }
}
