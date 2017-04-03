using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceAnalyzer
{
    static class ImageManipulator
    {
        static public List<Color> GetColorHistory(List<Bitmap> fullHistory, int x, int y)
        {

            List<Color> output = new List<Color>();
            foreach (Bitmap bitmap in fullHistory)
            {
                output.Add(bitmap.GetPixel(x, y));
            }

            return output;
        }

        static public void SetOutputPixelByHistory(Bitmap output, List<Bitmap> fullHistory, int x, int y)
        {
            List<Color> colorHistory = ImageManipulator.GetColorHistory(fullHistory, x, y);

            output.SetPixel(x, y, colorHistory.GroupBy(value => value).OrderByDescending(group => group.Count()).First().First());

            Debug.Print("Finished for " + x + "," + y);
        }

        static private double GetRatioOfColor(Color color, List<Color> colorHistory)
        {
            int countOfChosenColor = colorHistory.Where(a => a == color).Count();
            int total = colorHistory.Count();

            return ((double)countOfChosenColor) / ((double)total);

        }
        
        static public Color GetColorByColorHistory(List<Color> colorHistory)
        {
            Color outputColor = colorHistory.GroupBy(value => value).OrderByDescending(group => group.Count()).Select(g => g.Key).First();

            int total = colorHistory.Count();

            double colorRatio = GetRatioOfColor(outputColor, colorHistory);
            if (colorRatio < 0.95 && total > 4)
            {
                Color smallerSetColor = GetColorByColorHistory(colorHistory.Skip(total/4).Take(total - total/4).ToList<Color>());

                double smallerSetColorRatio = GetRatioOfColor(smallerSetColor, colorHistory.Skip(total / 4).Take(total - total / 4).ToList<Color>());

                if(colorRatio < 0.8 && smallerSetColorRatio < 0.8)
                {
                    return GetColorByColorHistory(colorHistory.Skip(total / 2).Take(total - total / 2).ToList<Color>());
                }
                else if(colorRatio > smallerSetColorRatio)
                {
                    return outputColor;
                }
                else
                {
                    return smallerSetColor;
                }
            }


            return outputColor;
        }
    }
}
