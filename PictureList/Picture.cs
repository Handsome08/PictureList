using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureList
{
    class Picture
    {
        //大图路径
        public string Source { get; set; }
        //缩略图路径
        public string ThumbPath { get; set; }
        public Picture() { }

        //用于初始化添加按钮"+"
        public Picture(string path1,string path2)
        {
            this.Source = path1;
            this.ThumbPath = path2;
        }
        
    }
}
