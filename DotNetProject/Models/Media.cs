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

        #region Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion
        
        public Media()
        {
        }
    }
}
