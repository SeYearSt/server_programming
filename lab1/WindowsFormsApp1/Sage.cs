using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;

namespace WindowsFormsApp1
{
    public class Sage
    { 
        public Sage() {
            this.Books = new HashSet<Book>();
        }
        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public byte[] photo { get; set; }
        public string city { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        static public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                return ms.ToArray();
            }
        }

        static public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);

                return returnImage;
            }

        }
    }
}
