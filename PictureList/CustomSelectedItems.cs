using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PictureList
{
    class CustomSelectedItems : DependencyObject
    {
        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(CustomSelectedItems), 
                new PropertyMetadata(OnSelectedItemsChange));

        private static void OnSelectedItemsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listbox = d as ListBox;
            if (listbox != null && listbox.SelectionMode == SelectionMode.Multiple)
            {
                listbox.SelectedItems.Clear();
                var collection = e.NewValue as IList;
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        listbox.SelectedItems.Add(item);
                    }
                }
            }
        }
    }
}
