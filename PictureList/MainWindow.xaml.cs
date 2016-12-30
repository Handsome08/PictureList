using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            lstPictures = ImageFactory.LoadImages();
            lstPictures.Add(AddButton);

            Listbox.ItemsSource = lstPictures;

            CommandBinding binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += AddPicture_Executed;
            binding.CanExecute += BindingOnCanExecute;
            this.CommandBindings.Add(binding);

            
        }

        private void BindingOnCanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = CanOpen;
        }

        private void AddPicture_Executed(object sender, ExecutedRoutedEventArgs e)
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
        private ObservableCollection<Picture> lstPictures = new ObservableCollection<Picture>(); 
        private Picture AddButton = new Picture("+");

        //控制添加按钮命令是否可用
        private bool CanOpen = true;

        private RoutedUICommand AddPicture;

        //用于移动窗口的变量
        private bool CanWinMove = false;
        Point pos = new Point();
        private double Win_Left,Win_Top;
    }
}
