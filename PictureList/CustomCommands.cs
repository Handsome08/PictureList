using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureList
{
    class CustomCommands
    {
        static CustomCommands()
        {
            add = new RoutedUICommand("Add","Add",typeof(CustomCommands));

            InputGestureCollection delete_input = new InputGestureCollection();
            delete_input.Add(new KeyGesture(Key.Delete));
            delete = new RoutedUICommand("Delete","Delete",typeof(CustomCommands),delete_input);

            InputGestureCollection zoom_input = new InputGestureCollection();
            zoom_input.Add(new MouseGesture(MouseAction.LeftDoubleClick));
            zoom = new RoutedUICommand("Zoom","Zoom",typeof(CustomCommands),zoom_input);

            InputGestureCollection close_input = new InputGestureCollection();
            close_input.Add(new MouseGesture(MouseAction.LeftClick));
            close_input.Add(new MouseGesture(MouseAction.RightClick));
            close = new RoutedUICommand("Close","Close",typeof(CustomCommands),close_input);
           
            moveleft = new RoutedUICommand("MoveLeft","MoveLeft",typeof(CustomCommands));

            moveright = new RoutedUICommand("MoveRight","MoveRight",typeof(CustomCommands));
        }
        
        private static RoutedUICommand delete;
        private static RoutedUICommand add;
        private static RoutedUICommand zoom;
        private static RoutedUICommand close;
        private static RoutedUICommand moveleft;
        private static RoutedUICommand moveright;

        public static RoutedUICommand Delete
        {
            get { return delete; }
        }

        public static RoutedUICommand Add
        {
            get { return add; }
        }

        public static RoutedUICommand Zoom
        {
            get { return zoom; }
        }

        public static RoutedUICommand Close
        {
            get { return close; }
        }

        public static RoutedUICommand MoveLeft
        {
            get { return moveleft; }
        }

        public static RoutedUICommand MoveRight
        {
            get { return moveright; }
        }
    }
}
