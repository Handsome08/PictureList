using System;
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

        private void DeleteExcuted(object o)
        {
            //int index = selectedIndex;
            lstPictures.RemoveAt(SelectedIndex);
        }

        private bool DeleteCanExcuteCommand(object o)
        {
            if (SelectedItem != null)
            {
                return true;
            }

            return false;
        }
        //obj是ListBoxItem对象
        private void MoveRightExcuted(object obj)
        {
            int index = SelectedIndex;
            Picture temp = lstPictures[(index + 1)];
            lstPictures[index + 1] = lstPictures[index];
            lstPictures[index] = temp;
            //SelectedItem = Listbox.Items[index + 1];
        }

        private bool MoveRightCanExcuteCommand(object arg)
        {
            if (arg is ListBoxItem)
            {
                (arg as ListBoxItem).IsSelected = true;
            }
            if (SelectedItem != null && SelectedIndex == lstPictures.Count - 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void MoveLeftExcuted(object obj)
        {
            int index = SelectedIndex;
            Picture temp = lstPictures[(index - 1)];
            lstPictures[index - 1] = lstPictures[index];
            lstPictures[index] = temp;
        }

        private bool MoveLeftCanExcuteCommand(object arg)
        {
            if (arg is ListBoxItem)
            {
                (arg as ListBoxItem).IsSelected = true;
            }
            if (SelectedItem != null && SelectedIndex == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ZoomExcuted(object obj)
        {
            var fullPicture = new FullPicture();
            fullPicture.DataContext = SelectedItem;
            fullPicture.Show();
        }

        private bool ZoomCanExcuteCommand(object arg)
        {
            if (SelectedItem != null)
            {
                return true;
            }

            return false;
        }

        //命令和绑定数据
        private object selectedItem;
        private int selectedIndex;
        private DelegateCommand add;
        private DelegateCommand delete;
        private DelegateCommand zoom;
        private DelegateCommand moveLeft;
        private DelegateCommand moveRight;


        //绑定界面的选择项
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

        

        private void AddExcuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Filter = "图片|*.png,*.jpg";
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
