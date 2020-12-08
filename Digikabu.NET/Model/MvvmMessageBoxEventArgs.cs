using System;

namespace Digikabu.NET
{
    public class MvvmMessageBoxEventArgs : EventArgs
    {
        public MvvmMessageBoxEventArgs(Action<bool> resultAction, string messageBoxText, string caption = "", bool onlyOk = true)
        {
            this.ResultAction = resultAction;
            this.MessageBoxText = messageBoxText;
            this.Caption = caption;

            this.OnlyOk = onlyOk;
        }

        public Action<bool> ResultAction { get; set; }
        public string MessageBoxText { get; set; }
        public string Caption { get; set; }
        public bool OnlyOk { get; set; }
    }
}
