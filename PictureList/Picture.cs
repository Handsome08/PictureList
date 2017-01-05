using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureList
{
    class Picture
    {
        public string Source { get; set; }
        public Picture() { }

        public Picture(string str)
        {
            this.Source = str;
        }
        
    }
}
