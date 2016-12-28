using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace PictureList
{
    public partial class Button : ResourceDictionary
    {
        public Button()
        {
            InitializeComponent();
        }

        private void AddButtonOnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Filter = "PNG图片|*.png|JPG图片|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                //FileName返回的是文件的绝对路径
                var length = MainWindow.lstObject.Count;
                MainWindow.lstObject.RemoveAt(length - 1);
                Image tempImage = new Image();
                tempImage.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                MainWindow.lstObject.Add(tempImage);
                MainWindow.lstObject.Add(MainWindow.addButton);
            }
        }
    }

}
