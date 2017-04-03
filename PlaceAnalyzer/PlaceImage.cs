using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceAnalyzer
{
    class PlaceImage
    {

        private Bitmap bmp;
        private List<Bitmap> history = new List<Bitmap>();

        public PlaceImage()
        {
        }
        
        public PlaceImage(string filename)
        {
            bmp = (Bitmap)Image.FromFile(filename);
        }

        public void UpdatePixelsFromImage(string filename)
        {
            if(bmp != null)
            {
                history.Add(bmp);
            }
            bmp = (Bitmap)Image.FromFile(filename);
        }

        public Bitmap GetBitmap()
        {
            return bmp;
        }

        public List<Bitmap> GetFullHistory()
        {
            List<Bitmap> output = new List<Bitmap>();
            foreach(Bitmap bitmap in history)
            {
                output.Add(bitmap);
            }
            if(bmp != null)
            {
                output.Add(bmp);
            }
            return output;
        }

    }
}
