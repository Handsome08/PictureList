using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureList
{
    static class DelegatedCommandInit
    {
        static DelegatedCommandInit()
        {
            //delete = new DelegateCommand(DeleteExcuted, DeleteCanExcuteCommand);
        }

        private static DelegateCommand delete;

    }
}
