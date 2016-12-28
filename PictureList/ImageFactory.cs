using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictureList
{
    class ImageFactory : INotifyCollectionChanged
    {
        public ImageFactory()
        {
            
        }
       

        public static ObservableCollection<object> LoadImages()
        {
            string[] fileNames = Directory.GetFiles(@"..\..\Pictures");
            string path = Directory.GetCurrentDirectory();
            ObservableCollection<object> result = new ObservableCollection<object>();
            foreach (string fileName in fileNames)
            {
                //Console.WriteLine(fileName.Remove(0,15));
                Image tempImage = new Image();
                tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                result.Add(tempImage);
            }
             
            return result;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
