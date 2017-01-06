using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PictureList
{
    class PictureListViewModel : INotifyPropertyChanged
    {
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
            delete = new DelegateCommand(DeleteExcuted, DeleteCanExcuteCommand);

            //放大图片的按钮
            zoom = new DelegateCommand(ZoomExcuted, ZoomCanExcuteCommand);

            //左移图片
            moveLeft = new DelegateCommand(MoveLeftExcuted, MoveLeftCanExcuteCommand);

            //右移图片
            moveRight = new DelegateCommand(MoveRightExcuted, MoveRightCanExcuteCommand);
        }

        private void DeleteExcuted(object obj)
        {
            //int index = selectedIndex;
            while (selectedItem != null)
            {
                lstPictures.RemoveAt(SelectedIndex);
            }
            
        }

        private bool DeleteCanExcuteCommand(object obj)
        {
            if (SelectedItem != null)
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
        //obj是ListBoxItem对象,通过CommandParameter传入
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

        private void AddExcuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Filter = "图片|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                //FileName返回的是文件的绝对路径
                var length = lstPictures.Count;
                lstPictures.RemoveAt(length - 1);
                foreach (var fileName in openFileDialog.FileNames)
                {
                    Picture temPicture = new Picture();
                    temPicture.Source = fileName;
                    lstPictures.Add(temPicture);
                }
                
                lstPictures.Add(AddButton);
            }
        }


        //命令和绑定数据
        private object selectedItem;
        private int selectedIndex;
        private DelegateCommand add;
        private DelegateCommand delete;
        private DelegateCommand zoom;
        private DelegateCommand moveLeft;
        private DelegateCommand moveRight;
        private IList selectedItems;

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

        public DelegateCommand DeleteCommand
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

        
        
        private ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>();
        public ObservableCollection<Picture> LstPictures
        {
            get { return lstPictures; }
        } 

        private Picture AddButton;

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
