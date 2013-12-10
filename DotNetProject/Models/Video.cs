﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class Video : Media
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

        public Video()
        {
        }
        public Video(string name, string artist, string path)
        {
            this.Name = name;
            this.Artist = artist;
            this.Path = path;
        }
    }
}
