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
using Microsoft.Win32;

namespace PictureList
{
    class ImageFactory
    {
        public ImageFactory()
        {
            InitializeCommands();
            LoadImages();
        }

        //初始化自定义命令
        private void InitializeCommands()
        {
            //添加按钮的命令
            add = new DelegateCommand();
            add.ExcutedCommand = new Action<object>(AddExcuted);
            //删除按钮的命令
            delete = new DelegateCommand();
            delete.CanExcuteCommand = new Func<object, bool>(DeleteCanExcuteCommand);
            delete.ExcutedCommand = new Action<object>(DeleteExcuted);
        }

        private DelegateCommand add;
        private DelegateCommand delete;

        public DelegateCommand AddCommand
        {
            get { return add; }
        }

        public DelegateCommand DeleteCommand
        {
            get { return delete; }
        }
        private void DeleteExcuted(object o)
        {
            if ((o as ListBox) == null)
            {
                return;
            }
            int index = (o as ListBox).SelectedIndex;
            lstPictures.RemoveAt(index);
        }

        private bool DeleteCanExcuteCommand(object o)
        {
            if (o is ListBox)
            {
                if ((o as ListBox).SelectedItem != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void AddExcuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Filter = "PNG图片|*.png|JPG图片|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                //FileName返回的是文件的绝对路径
                var length = lstPictures.Count;
                lstPictures.RemoveAt(length - 1);
                Picture temPicture = new Picture();
                temPicture.Source = openFileDialog.FileName;
                lstPictures.Add(temPicture);
                lstPictures.Add(AddButton);
            }
        }


        //加载文件夹中的图片
        private void LoadImages()
        {
            string[] fileNames = Directory.GetFiles(@"..\..\Pictures");
            //string path = Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            //ObservableCollection<Picture> result = new ObservableCollection<Picture>();
            foreach (string fileName in fileNames)
            {
                Console.WriteLine(fileName.Remove(0,15));
                //string temp = path + fileName;
                //Console.WriteLine(temp);
                //Image tempImage = new Image();
                //tempImage.Source = new BitmapImage(new Uri(fileName.Remove(0,6),UriKind.Relative));
                Picture temPicture = new Picture();
                temPicture.Source = fileName.Remove(0, 6);
                lstPictures.Add(temPicture);
            }
            lstPictures.Add(AddButton);
        }
        private Picture AddButton = new Picture("+");
        private ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>();

        public ObservableCollection<Picture> LstPictures
        {
            get { return lstPictures; }
        }
    }
}
