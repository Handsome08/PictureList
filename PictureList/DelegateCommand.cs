using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PictureList
{
    public class DelegateCommand : ICommand
    {
        private Action<object> ExcutedCommand;

        private Func<object,bool> CanExcuteCommand;

        //实现才可以在值变化时触发CanExcuteCommand
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public DelegateCommand() { }

        public DelegateCommand(Action<object> excute, Func<object, bool> canexcute)
        {
            ExcutedCommand = excute;
            CanExcuteCommand = canexcute;
        } 
        

        
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


    }


}
