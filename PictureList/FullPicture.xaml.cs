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
using System.Windows.Shapes;

namespace PictureList
{
    /// <summary>
    /// FullPicture.xaml 的交互逻辑
    /// </summary>
    public partial class FullPicture : Window
    {
        public FullPicture()
        {
            InitializeComponent();
            
            this.Top = (screenHeight - this.Height)/2;
            this.Left = (screenWidth - this.Width)/2;

            //创建命令绑定，并添加到CommandBindings中
            CommandBinding bindingClose = new CommandBinding(CustomCommands.Close);
            bindingClose.Executed += BindingClose_Executed;
            this.CommandBindings.Add(bindingClose);

        }

        private void BindingClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        //获取显示器屏幕的高度和宽度
        private readonly double screenHeight = SystemParameters.FullPrimaryScreenHeight;
        private readonly double screenWidth = SystemParameters.FullPrimaryScreenWidth;
        //鼠标滚轮缩放图片
        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point centerPoint = e.GetPosition(null);
            this.sfr.CenterX = centerPoint.X;
            this.sfr.CenterY = centerPoint.Y;
            if (sfr.ScaleX < 0.3 && sfr.ScaleY < 0.3 && e.Delta < 0)
            {
                return;
            }
            sfr.ScaleX += (double)e.Delta / 3500;
            sfr.ScaleY += (double)e.Delta / 3500;
        }
    }
}
