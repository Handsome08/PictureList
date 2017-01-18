using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureList
{
    public class MyCommandBinding : CommandBinding
    {
        public ICommand DelegatedCommand { get; set; }

        public MyCommandBinding()
        {
            this.Executed += new ExecutedRoutedEventHandler(DelegatedCommand_Executed);
            this.CanExecute += new CanExecuteRoutedEventHandler(DelegatedCommand_CanExecute);
        }

        private void DelegatedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            if (this.DelegatedCommand != null)
            {
                canExecuteRoutedEventArgs.CanExecute =  this.DelegatedCommand.CanExecute(canExecuteRoutedEventArgs.Parameter);
            }
        }

        private void DelegatedCommand_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (this.DelegatedCommand != null)
            {
               this.DelegatedCommand.Execute(executedRoutedEventArgs.Parameter);
            }
        }
    }
}
