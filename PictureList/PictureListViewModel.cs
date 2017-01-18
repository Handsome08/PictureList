using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using static System.Threading.Thread;

namespace PictureList
{
    class PictureListViewModel : INotifyPropertyChanged
    {
        static PictureListViewModel()
        {
            delete = new DelegateCommand(DeleteExcuted, DeleteCanExcuteCommand);
        }
        public PictureListViewModel()
        {
            ImageFactory imageFactory = new ImageFactory();
            lstPictures = imageFactory.LstPictures;
            AddButton = imageFactory.AddButton;
            InitializeCommands();
        }
        //初始化自定义命令
        public void InitializeCommands()
        {
            //添加按钮的命令
            add = new DelegateCommand(AddExcuted, null);

            //删除按钮的命令
            //delete = new DelegateCommand(DeleteExcuted, DeleteCanExcuteCommand);

            //放大图片的按钮
            zoom = new DelegateCommand(ZoomExcuted, ZoomCanExcuteCommand);

            //左移图片
            moveLeft = new DelegateCommand(MoveLeftExcuted, MoveLeftCanExcuteCommand);

            //右移图片
            moveRight = new DelegateCommand(MoveRightExcuted, MoveRightCanExcuteCommand);
        }

        public static void DeleteExcuted(object obj)
        {
            //int index = selectedIndex;
            while (selectedItem != null)
            {
                lstPictures.RemoveAt(selectedIndex);
            }
            if (lstPictures.Count == 0)
            {
                lstPictures.Add(AddButton);
            }
            
        }

        public static bool DeleteCanExcuteCommand(object obj)
        {
            if (selectedItem != null)
            {
                return true;
            }

            return false;
        }
        //obj是ListBoxItem对象,通过CommandParameter传入
        private void MoveRightExcuted(object obj)
        {
            //Picture tempItem = (Picture)SelectedItem;
            
            int index = GetIndex((Picture)obj);
            Picture temp = lstPictures[(index + 1)];
            lstPictures[index + 1] = lstPictures[index];
            lstPictures[index] = temp;

            //SelectedItem = tempItem;
        }
        //obj是ListBoxItem对象,通过CommandParameter传入
        private bool MoveRightCanExcuteCommand(object obj)
        {
            
            int tempIndex = -1;
            
            var picture = obj as Picture;
            if (picture != null)
            {
                tempIndex = GetIndex(picture);
            }

            if (tempIndex == LstPictures.Count-2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //obj是ListBoxItem对象中的Picture,通过CommandParameter传入
        private void MoveLeftExcuted(object obj)
        {
            //Picture tempItem = (Picture)SelectedItem;
            IList tempItems = null;
            //if (obj is ListBoxItem)
            //{
            //    var listBoxItem = obj as ListBoxItem;
            //    var listBox = listBoxItem.Parent as ListBox;
            //    if (listBox != null)
            //    {
            //        tempItems = listBox.SelectedItems;
            //    }
            //}
            int index = GetIndex((Picture)obj);
            Picture temp = lstPictures[(index - 1)];
            lstPictures[index - 1] = lstPictures[index];
            lstPictures[index] = temp;

            SelectedItems = tempItems;
            //SelectedItem = tempItem;
        }
        //obj是ListBoxItem对象,通过CommandParameter传入
        private bool MoveLeftCanExcuteCommand(object obj)
        {
             
            int tempIndex = -1;

            var picture = obj as Picture;
            if (picture != null)
            {
                tempIndex = GetIndex(picture);
            }
          
            if (tempIndex == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //获取传入的ListBoxItem中Picture在LstPictures中的index
        private int GetIndex(Picture picture)
        {
            for (var i = 0; i < LstPictures.Count - 1; i++)
            {
                if (picture == LstPictures[i])
                {
                    return i;
                }
            }
            
            return -1;
        }
        

        private void ZoomExcuted(object obj)
        {
            var fullPicture = new FullPicture();
            fullPicture.DataContext = SelectedItem;
            fullPicture.Show();
        }
        
        private bool ZoomCanExcuteCommand(object obj)
        {
            if (SelectedItem != null)
            {
                return true;
            }

            return false;
        }

        private static string dialogDirectory;
        private static string[] fileNames;
        private void AddExcuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Filter = "图片|*.png;*.jpg";
            //string applicationPath = Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                
                //var length = lstPictures.Count;
                //lstPictures.RemoveAt(length - 1);
                //获取当前用户所选择的路径
                dialogDirectory = openFileDialog.FileNames[0].Replace(openFileDialog.SafeFileNames[0], "");
                //前9张图片一次性显示出来
                //int firstShowCount = 9;
                fileNames = openFileDialog.FileNames;
                ShowPictures(fileNames);
            }
        }

        public void ShowPictures(string[] fileNames)
        {
            var length = lstPictures.Count;
            lstPictures.RemoveAt(length - 1);
            //前9张图片一次性显示出来
            int firstShowCount = 9;
            if (fileNames.Length < 9)
            {
                firstShowCount = fileNames.Length;
            }
            for (int i = 0; i < firstShowCount; i++)
            {
                lstPictures.Add(GetPicture(fileNames[i]));
            }
            LoadImageBackground(fileNames, firstShowCount);
            Console.WriteLine("Add Complete!");
        }

        private async Task LoadImageBackground(string[] fileNames,int ignoreCount)
        {
            List<Picture> temPictures = new List<Picture>();
            await Task.Run(() =>
            {
                foreach (var fileName in fileNames)
                {
                    if (ignoreCount-- >= 0)
                    {
                        continue;
                    }
                    Picture tempPicture = GetPicture(fileName);
                    if (tempPicture != null)
                    {
                        temPictures.Add(tempPicture);
                    }
                }
                temPictures.Add(AddButton);
            });
            //由于不能在非主线程中对和界面绑定的集合进行操作，所以需要使用线程池去操作
            AddPicture(temPictures);
            
        }

        private void AddPicture(List<Picture> tempPictures)
        {
            //ThreadPool.QueueUserWorkItem(delegate
            //{
            //    SynchronizationContext.SetSynchronizationContext(new
            //        System.Windows.Threading.DispatcherSynchronizationContext(
            //            Application.Current.Dispatcher));
            //    SynchronizationContext.Current.Send(p1 =>
            //    {
            //        lstPictures.Add(tempPicture);
            //    },null);
            //});
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(() =>
            {
                foreach (var tempPicture in tempPictures)
                {
                    lstPictures.Add(tempPicture);
                }
            }));
        }

        //生成对应名称的图片对象
        private Picture GetPicture(string fileName)
        {

            string applicationPath = Directory.GetCurrentDirectory();
            //缩略图的名称按加入顺序定义， 确保不会重复
            string thumbPath = Path.Combine(applicationPath + "\\Temp\\" + index++);
            //if (File.Exists(thumbPath))
            //{
            //    MessageBox.Show("Picture you choose：" + fileName.Replace(dialogDirectory, "") + " is Existed!");
            //    return null;
            //}
            return ImageFactory.CheckImagePixel(fileName, thumbPath);
        }

        //命令和绑定数据
        private static object selectedItem;
        private static int selectedIndex;

        private DelegateCommand add;
        private static DelegateCommand delete;
        private DelegateCommand zoom;
        private DelegateCommand moveLeft;
        private DelegateCommand moveRight;
        private IList selectedItems;

        private static int index = 0;
        //绑定界面的选择项
        //SeletedItem并不是ListBoxItem，而是ListBoxItem中存放的数据的类型，在本实例中为Picture
        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChange("SelectedItem");
            }
        }
        
        //绑定选择项的序列号
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChange("SelectedIndex");
            }
        }

        public IList SelectedItems
        {
            get {return selectedItems;}
            set
            {
                selectedItems = value;
                OnPropertyChange("SelectedItems");
            }
        }

        public DelegateCommand AddCommand
        {
            get { return add; }
        }

        public static DelegateCommand DeleteCommand
        {
            get { return delete; }
        }

        public DelegateCommand ZoomCommand
        {
            get { return zoom; }
        }

        public DelegateCommand MoveLeftCommand
        {
            get { return moveLeft; }
        }

        public DelegateCommand MoveRightCommand
        {
            get { return moveRight; }
        }

        
        
        private static ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>();
        
        public ObservableCollection<Picture> LstPictures
        {
            get { return lstPictures; }
        } 

        private static Picture AddButton;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
