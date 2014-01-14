using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class Video : Media
    {
        #region Artist
        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }
        #endregion

        public Video()
        {
        }
        public Video(string name, string artist, string path)
        {
            this.Name = name;
            this.Artist = artist;
            this.Path = path;
        }

        public override bool contains(string toFind)
        {
            return (base.contains(toFind) || Artist.IndexOf(toFind) != -1);
        }
    }
}
