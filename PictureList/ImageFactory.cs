using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using Image = System.Drawing.Image;

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
            string[] fileNames = Directory.GetFiles(@"C:\Users\user\Documents\Visual Studio 2015\Projects\PictureList\PictureList\bin\Debug\Pictures");
            string currentPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            //ObservableCollection<Picture> result = new ObservableCollection<Picture>();
            Directory.CreateDirectory("Temp");
            
            foreach (string fileName in fileNames)
            {
                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
                {
                    Console.WriteLine(fileName.Remove(0, 15));
                    //string temp = path + fileName;
                    //Console.WriteLine(temp);
                    //Image tempImage = new Image();
                    //tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                    string thumbPath = currentPath + "\\Temp" + fileName.Replace(currentPath + "\\Pictures", "");
                    Picture temPicture = CheckImagePixel(fileName, thumbPath);

                    lstPictures.Add(temPicture);
                }
            }
            lstPictures.Add(addButton);
        }

        private  Picture addButton = new Picture("+","+");

        //fileName是全路径,thumbPath是保存缩略图的位置：可执行程序目录下的Temp文件夹（包括缩略图名称扩展名）
        public static Picture CheckImagePixel(string fileName,string thumbPath)
        {
            System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(fileName);
            if (sourceImage.Height < 324 || sourceImage.Width < 576)
            {
                return new Picture(fileName, fileName);
            }
            Bitmap thumb = new Bitmap(sourceImage, 192, 108);
            Console.WriteLine(thumbPath);
            sourceImage.Dispose();
            thumb.Save(thumbPath);
            thumb.Dispose();
            return new Picture(fileName, thumbPath);
        }

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
