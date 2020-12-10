using Digikabu.NET.Persistence;
using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Digikabu.NET
{
    class MainViewModel : BaseModel
    {
        #region Events

        public event EventHandler<MvvmMessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<MvvmDialogEventArgs> DialogCreateRequest;
        public event EventHandler<EventArgs> DialogCloseRequest;

        #endregion

        #region Databinding

        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Commands

        public ICommand OnLoginClickCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            SetupTheme();
            SetupCommands();
        }

        private void SetupTheme()
        {
            if(!Properties.Settings.Default.IsThemeLocked)
            {
                var theme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1);
                switch (theme)
                {
                    case 0:
                        Properties.Settings.Default.UseDarkMode = true;
                        Properties.Settings.Default.Save();
                        break;
                    case 1:
                        Properties.Settings.Default.UseDarkMode = false;
                        Properties.Settings.Default.Save();
                        break;
                }
            }
        }

        public void SetupCommands()
        {
            OnLoginClickCommand = new ActionCommand(OnLoginClick);
        }

        #region Mvvm Methods

        public async void OnLoginClick(object obj)
        {
            var passwordBox = obj as PasswordBox;
            Password = passwordBox.Password;
            //RequestMessageBox(null, $"{await DigiCon.AuthorizeAsync(Username, Password)}", "demoCaption", false);
            bool authorized = await DigiCon.AuthorizeAsync(Username, Password);
            if (authorized)
            {
                new MasterWindow().Show();
            }
            else
            {
                RequestMessageBox(null, $"Nutzername und/oder Passwort falsch", "Fehler", false);
            }
        }

        #endregion

        private void RequestMessageBox(Action<bool> resultAction, string messageBoxText, string caption = "", bool onlyOk = true)
        {
            this.MessageBoxRequest?.Invoke(this, new MvvmMessageBoxEventArgs(resultAction, messageBoxText, caption, onlyOk));
        }

        private void RequestDialog(DialogType type, Action actionAfterCreate = null)
        {
            this.DialogCreateRequest?.Invoke(this, new MvvmDialogEventArgs(type, actionAfterCreate));
        }
    }
}
