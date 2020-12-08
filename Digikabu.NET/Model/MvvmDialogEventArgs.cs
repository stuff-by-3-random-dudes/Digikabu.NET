using System;

namespace Digikabu.NET
{
    public class MvvmDialogEventArgs : EventArgs
    {
        public DialogType type;
        public Action actionAfterCreate;
        public MvvmDialogEventArgs(DialogType type, Action actionAfterCreate = null)
        {
            this.type = type;
            this.actionAfterCreate = actionAfterCreate;
        }
    }
}
