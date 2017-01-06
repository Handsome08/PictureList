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
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "MoveLeftCommand":
                        return pictureListViewModel.MoveLeftCommand;
                        
                    case "MoveRightCommand":
                        return pictureListViewModel.MoveRightCommand;

                    case "AddCommand":
                        return pictureListViewModel.AddCommand;

                    default:
                        return null;

                }
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new PictureListViewModel();
        }
        
    }
}
