using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class Media
    {
        #region Path
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        #endregion

        public Media()
        {
        }
    }
}
