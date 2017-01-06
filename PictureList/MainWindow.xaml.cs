using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PictureList
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TitleBar.MouseLeftButtonDown += TitleBar_MouseLeftButtonDown;
            TitleBar.MouseMove += TitleBar_MouseMove;
            TitleBar.MouseLeftButtonUp += TitleBar_MouseLeftButtonUp;

            
            
            //lstPictures.Add(AddButton);
            PictureListViewModel pictureListViewModel = new PictureListViewModel();
            DataContext = pictureListViewModel;

            //Listbox.ItemsSource = lstPictures;

            ////添加自定义命令的绑定
            //CommandBinding bindingAdd = new CommandBinding(CustomCommands.Add);
            //bindingAdd.Executed += AddPicture_Executed;

            //CommandBinding bindingDelete = new CommandBinding(CustomCommands.Delete);
            //bindingDelete.Executed += DeleteCommand_Excuted;
            //bindingDelete.CanExecute += DeleteCommand_CanExcute;

            //CommandBinding bindingZoom = new CommandBinding(CustomCommands.Zoom);
            //bindingZoom.Executed += BindingZoom_Executed;
            //bindingZoom.CanExecute += BindingZoom_CanExecute;

            //CommandBinding bindingMoveLeft = new CommandBinding(CustomCommands.MoveLeft);
            //bindingMoveLeft.Executed += BindingMoveLeft_Executed;
            //bindingMoveLeft.CanExecute += BindingMoveLeft_CanExecute;

            //CommandBinding bindingMoveRight = new CommandBinding(CustomCommands.MoveRight);
            //bindingMoveRight.Executed += BindingMoveRight_Executed;
            //bindingMoveRight.CanExecute += BindingMoveRight_CanExecute;

            //this.CommandBindings.Add(bindingAdd);
            //this.CommandBindings.Add(bindingDelete);
            //this.CommandBindings.Add(bindingZoom);
            //this.CommandBindings.Add(bindingMoveLeft);
            //this.CommandBindings.Add(bindingMoveRight);
        }
        

        //用于移动窗口的变量
        private bool CanWinMove = false;
        Point pos = new Point();
        private double Win_Left, Win_Top;
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CanWinMove = true;
            pos = PointToScreen(e.GetPosition(null));
            Win_Left = this.Left;
            Win_Top = this.Top;
            TitleBar.CaptureMouse();
        }

        private void TitleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CanWinMove = false;
            TitleBar.ReleaseMouseCapture();
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanWinMove)
            {
                Point tempPos = PointToScreen(e.GetPosition(null));
                this.Left = tempPos.X - pos.X + Win_Left;
                this.Top = tempPos.Y - pos.Y + Win_Top;
                Win_Left = this.Left;
                Win_Top = this.Top;
                pos = tempPos;
            }
            
        }


        private void ButtonX_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        
        //public static System.Windows.Controls.Button addButton = new System.Windows.Controls.Button();

        //private ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>();
        //private Picture AddButton = new Picture("+");


        //自定义Delete命令
        //    private void DeleteCommand_Excuted(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        int index = Listbox.SelectedIndex;
        //        lstPictures.RemoveAt(index);
        //    }


        //    private void DeleteCommand_CanExcute(object sender, CanExecuteRoutedEventArgs e)
        //    {
        //        if (Listbox.SelectedItem == null)
        //        {
        //            e.CanExecute = false;
        //        }
        //        else
        //        {
        //            e.CanExecute = true;
        //        }
        //    }

        //    //自定义Add命令
        //    private void AddPicture_Executed(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        OpenFileDialog openFileDialog = new OpenFileDialog();
        //        openFileDialog.InitialDirectory = @"D:\";
        //        openFileDialog.Filter = "PNG图片|*.png|JPG图片|*.jpg";
        //        if (openFileDialog.ShowDialog() == true)
        //        {
        //            //FileName返回的是文件的绝对路径
        //            var length = lstPictures.Count;
        //            lstPictures.RemoveAt(length - 1);
        //            Picture temPicture = new Picture();
        //            temPicture.Source = openFileDialog.FileName;
        //            lstPictures.Add(temPicture);
        //            lstPictures.Add(ImageFactory.AddButton);
        //        }
        //    }
        //自定义Zoom命令
        //    private void BindingZoom_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        //    {
        //        //Window win = new Window();
        //        //win.Style = (Style)this.FindResource("FullPictureWindowStyle");
        //        //Image image = new Image();
        //        //Binding binding = new Binding("Source");
        //        //binding.Source = Listbox.SelectedItem;
        //        //image.SetBinding(Image.SourceProperty, binding);
        //        //win.Content = image;
        //        //win.Show();

        //        var fullPicture = new FullPicture();
        //        fullPicture.DataContext = Listbox.SelectedItem;
        //        fullPicture.Show();
        //    }
        //    private void BindingZoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //    {
        //        if (Listbox.SelectedItem == null)
        //        {
        //            e.CanExecute = false;
        //        }
        //        else
        //        {
        //            e.CanExecute = true;
        //        }
        //    }

        //    //自定义MoveRight命令
        //    private void BindingMoveRight_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //    {
        //        if (Listbox.SelectedItem != null && Listbox.SelectedIndex == Listbox.Items.Count - 2)
        //        {
        //            e.CanExecute = false;
        //        }
        //        else
        //        {
        //            e.CanExecute = true;
        //        }
        //    }

        //    private void BindingMoveRight_Executed(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        int index = Listbox.SelectedIndex;
        //        Picture temp = lstPictures[(index + 1)];
        //        lstPictures[index + 1] = lstPictures[index];
        //        lstPictures[index] = temp;
        //        Listbox.SelectedItem = Listbox.Items[index + 1];
        //    }

        //    //自定义MoveLeft命令
        //    private void BindingMoveLeft_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //    {
        //        if (Listbox.SelectedItem != null && Listbox.SelectedIndex == 0)
        //        {
        //            e.CanExecute = false;
        //        }
        //        else
        //        {
        //            e.CanExecute = true;
        //        }
        //    }
        //    private void BindingMoveLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        int index = Listbox.SelectedIndex;
        //        Picture temp = lstPictures[(index - 1)];
        //        lstPictures[index - 1] = lstPictures[index];
        //        lstPictures[index] = temp;
        //        Listbox.SelectedItem = Listbox.Items[index - 1];
        //    }
    }
}
