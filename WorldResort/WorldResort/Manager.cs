using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WorldResort
{
    public static class Manager
    {
        public static string token { get; set; }
        public static HttpClient client = new HttpClient();
        public static string userId { get; set; }
        public static Frame adminFrameWindow { get; set; }
        public static Frame userFrameWindow { get; set; }
        public static string role { get; set; }
        public static BitmapImage GetImageFromByteArray(byte[] imgByte)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(imgByte);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
