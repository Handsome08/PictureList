using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictureList
{
    class ImageFactory
    {
        public ImageFactory()
        {
            
        }
       

        public static ObservableCollection<Picture> LoadImages()
        {
            string[] fileNames = Directory.GetFiles(@"..\..\Pictures");
            //string path = Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            ObservableCollection<Picture> result = new ObservableCollection<Picture>();
            foreach (string fileName in fileNames)
            {
                //Console.WriteLine(fileName.Remove(0,15));
                //string temp = path + fileName;
                //Console.WriteLine(temp);
                //Image tempImage = new Image();
                //tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                Picture temPicture = new Picture();
                temPicture.Source = fileName.Remove(0, 6);
                result.Add(temPicture);
            }
             
            return result;
        }
    }
}
