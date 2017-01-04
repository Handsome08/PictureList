using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureList
{
    class DelegateCommand : ICommand
    {
        public Action<object> ExcutedCommand = null;

        public Func<object,bool> CanExcuteCommand = null;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExcuteCommand != null)
            {
                return this.CanExcuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (ExcutedCommand != null)
            {
                this.ExcutedCommand(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }


}
