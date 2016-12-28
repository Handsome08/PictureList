using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PictureList
{
    class ListBoxItemContainerTemplateSelector : StyleSelector
    {
        public Style ImageTemplate { get; set; }
        public Style ButtonTemplate { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is Image)
            {
                return ImageTemplate;
            }
            else
            {
                return ButtonTemplate;
            }
        }
    }
}
