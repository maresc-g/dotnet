using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class ImageMedia : Media
    {
        #region Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

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
