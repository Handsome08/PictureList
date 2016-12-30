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
            AddButton.Source = "+";
            lstPictures.Add(AddButton);

            Listbox.ItemsSource = lstPictures;
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
        private Picture AddButton = new Picture();
        //用于移动窗口的变量
        private bool CanWinMove = false;
        Point pos = new Point();
        private double Win_Left,Win_Top;
    }
}
