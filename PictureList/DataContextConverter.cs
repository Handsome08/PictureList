using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PictureList
{
    [ValueConversion(typeof(PictureListViewModel), typeof(DelegateCommand))]
    class DataContextConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PictureListViewModel pictureListViewModel = (PictureListViewModel)value;
            if (parameter != null && parameter.ToString() == "MoveLeftCommand")
            {
                
                return pictureListViewModel.MoveLeftCommand;
            }
            Console.WriteLine(parameter.ToString());
            return pictureListViewModel.MoveRightCommand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new PictureListViewModel();
        }
        
    }
}
