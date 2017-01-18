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

        //加载默认文件夹中的图片
        public void LoadImages()
        {
            string defaultPath = "C:\\";
            string[] fileNames = Directory.GetFiles(defaultPath);
            string currentPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            //ObservableCollection<Picture> result = new ObservableCollection<Picture>();

            //启动时清空缓存
            if (Directory.Exists(currentPath + "\\Temp"))
            {
                Directory.Delete(currentPath + "\\Temp",true);
            }

            try
            {
                Directory.CreateDirectory("Temp");
            }
            catch
            {
                
            }
            
            
            foreach (string fileName in fileNames)
            {
                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
                {
                    Console.WriteLine(fileName.Remove(0, 15));
                    //string temp = path + fileName;
                    //Console.WriteLine(temp);
                    //Image tempImage = new Image();
                    //tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                    string thumbPath = currentPath + "\\Temp" + fileName.Replace(defaultPath, "");
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
            //语法糖，离开代码块时自动调用对象的dispose()
            using (sourceImage)
            {
                try
                {
                    //Bitmap占用内存，而且若发生异常内存会泄露，需要捕获异常
                    Bitmap thumb = new Bitmap(sourceImage, 192, 108);
                    Console.WriteLine(thumbPath);
                    thumb.Save(thumbPath);
                    thumb.Dispose();
                }
                catch
                {
                    
                }
            }
            
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
