using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PictureList
{
    class ListBoxItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate ButtonTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Picture temp = (Picture)item;
            if (temp != null && temp.Source == "+")
            {
                return ButtonTemplate;
            }
            else
            {
                return ImageTemplate;
            }
        }
    }
}
