using System;
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

        public string Name { get; set; }

        #endregion

        #region Commands

        public ICommand OnLoginClickCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            SetupCommands();
        }

        public void SetupCommands()
        {
            OnLoginClickCommand = new ActionCommand(OnLoginClick);
        }

        #region Mvvm Methods

        public void OnLoginClick(object obj)
        {

        }

        #endregion
    }
}
