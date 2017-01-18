using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PictureList
{
    public partial class CustomListBox : ResourceDictionary
    {
        public CustomListBox()
        {
            InitializeComponent();
        }
        private void ListBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            ListBoxItem targetItem = sender as ListBoxItem;
            targetItem.IsSelected = true;
            //ListBoxItem sourceItem = (ListBoxItem)e.Data.GetData(typeof(ListBoxItem));
            //if (sourceItem != null && targetItem != null)
            //{
            //    Picture temPicture = new Picture();
            //    temPicture = (Picture)sourceItem.Content;
            //    sourceItem.Content = targetItem.Content;
            //    targetItem.Content = temPicture;
            //}
        }

        private void ListBoxItem_DragLeave(object sender, DragEventArgs e)
        {
            ListBoxItem targetItem = sender as ListBoxItem;
            targetItem.IsSelected = false;
            //ListBoxItem sourceItem = (ListBoxItem)e.Data.GetData(typeof(ListBoxItem));
            //if (sourceItem != null && targetItem != null)
            //{
            //    Picture temPicture = new Picture();
            //    temPicture = (Picture)targetItem.Content;
            //    targetItem.Content = sourceItem.Content;
            //    sourceItem.Content = temPicture;
            //}
        }

        private void ListBoxItem_MouseMove(object sender, MouseEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            if (listBoxItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(listBoxItem, listBoxItem, DragDropEffects.All);
            }
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            ListBoxItem sourceItem = (ListBoxItem)e.Data.GetData(typeof(ListBoxItem));
            ListBoxItem targetItem = sender as ListBoxItem;
            if (sourceItem != null && targetItem != null)
            {
                Picture temPicture = new Picture();
                temPicture = (Picture)sourceItem.Content;
                sourceItem.Content = targetItem.Content;
                targetItem.Content = temPicture;
            }

        }

        private void ListBoxItem_DragOver(object sender, DragEventArgs e)
        {
            
        }
    }
}
