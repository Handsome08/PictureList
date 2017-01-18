using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureList
{
    static class UiCommands
    {
        //一定要是路由命令，若非路由命令，命令无效
        public static RoutedUICommand Delete = new RoutedUICommand("Delete","Delete",typeof(UiCommands));
        //public static DelegateCommand Delete1 = new DelegateCommand();
    }
}
