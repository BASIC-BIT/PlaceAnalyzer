using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PlaceAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaceImage placeImage = new PlaceImage();
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\sjkneisler\Documents\Visual Studio 2017\Projects\PlaceAnalyzer\PlaceAnalyzer\PlaceImages");

            FileInfo[] files = d.GetFiles("*.png");

            foreach (FileInfo file in files)
            {
                Debug.Print("Processing File: " + file.Name);
                placeImage.UpdatePixelsFromImage(file.Directory + "/" + file.Name);
            }

            List<Bitmap> fullHistory = placeImage.GetFullHistory();

            int width = 1000;
            int height = 1000;

            int offsetX = 0;
            int offsetY = 0;

            Bitmap output = new Bitmap(width, height);
            
            
                
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    List<Color> colorHistory = new List<Color>();

                    foreach (Bitmap bitmap in fullHistory)
                    {
                        colorHistory.Add(bitmap.GetPixel(x+offsetX, y+offsetY));
                    }

                    output.SetPixel(x,y,ImageManipulator.GetColorByColorHistory(colorHistory));

                }
                Debug.Print("History Built for row: " + y);
            }

            output.Save("Z:/output.png");
        }
    }
}
