using DotNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject
{
    [Serializable]
    public class Song : Media
    {
        #region Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion
        #region Artist
        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }
        #endregion
        #region Number
        private int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        #endregion
        #region Album
        private string _album;
        public string Album
        {
            get { return _album; }
            set { _album = value; }
        }
        #endregion

        public Song()
        {
        }
        public Song(int number, string name, string artist, string album, string path)
        {
            this.Number = number;
            this.Name = name;
            this.Artist = artist;
            this.Album = album;
            this.Path = path;
        }
    }
}
