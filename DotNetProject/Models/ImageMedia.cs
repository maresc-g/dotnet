using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class ImageMedia : Media
    {
        public ImageMedia()
        {
        }
        public ImageMedia(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
