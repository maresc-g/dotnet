using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public abstract class Globals
    {
        public static string filterMusic = "*.mp3;*.wma;*.wav";
        public static string filterImg = "*.jpg;*.jpeg;*.bmp;*.png;*.gif";
        public static string filterVideo = "*.mp4";
        public static string[] tabImg = filterImg.Replace("*", String.Empty).Split(';');
        public static string[] tabMusic = filterMusic.Replace("*", String.Empty).Split(';');
        public static string[] tabVideo = filterVideo.Replace("*", String.Empty).Split(';');
        public static byte backR = 0xFF;
        public static byte backG = 0xFF;
        public static byte backB = 0xFF;
    }
}
