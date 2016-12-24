using System;
using System.Collections.Generic;
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
            List<Image> lstImage = new List<Image>();
            Image image1 = new Image();
            image1.Source = new BitmapImage(new Uri("192_108_image1.png",UriKind.Relative));
            Image image2 = new Image();
            image2.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image3 = new Image();
            image3.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image4 = new Image();
            image4.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image5 = new Image();
            image5.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image6= new Image();
            image6.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image7 = new Image();
            image7.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            Image image8 = new Image();
            image8.Source = new BitmapImage(new Uri("192_108_image1.png", UriKind.Relative));
            lstImage.Add(image1);
            lstImage.Add(image2);
            lstImage.Add(image3);
            lstImage.Add(image4);
            lstImage.Add(image5);
            lstImage.Add(image6);
            lstImage.Add(image7);
            lstImage.Add(image8);


            Listbox.ItemsSource = lstImage;
        }

        private void ButtonX_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
