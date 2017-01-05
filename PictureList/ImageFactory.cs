using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PictureList
{
    class ImageFactory 
    {
        public ImageFactory()
        {
            LoadImages();
        }

        //加载文件夹中的图片
        public void LoadImages()
        {
            string[] fileNames = Directory.GetFiles(@"..\..\Pictures");
            //string path = Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            //ObservableCollection<Picture> result = new ObservableCollection<Picture>();
            foreach (string fileName in fileNames)
            {
                Console.WriteLine(fileName.Remove(0, 15));
                //string temp = path + fileName;
                //Console.WriteLine(temp);
                //Image tempImage = new Image();
                //tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                Picture temPicture = new Picture();
                temPicture.Source = fileName.Remove(0, 6);
                lstPictures.Add(temPicture);
            }
            lstPictures.Add(addButton);
        }

        private  Picture addButton = new Picture("+");

        private  ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>();

        public ObservableCollection<Picture> LstPictures
        {
            get { return lstPictures; }
        }

        public Picture AddButton
        {
            get { return addButton; }
        }
    }
}
