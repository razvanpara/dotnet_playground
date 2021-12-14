using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageToASCII
{
    class Program
    {
        static Bitmap Resize(Bitmap src, int width, int height)
        {
            var result = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(src, 0, 0, width, height);
            }
            return result;
        }
        static char PixelToChar(Color color)
        {
            var characters = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\" ^`'. ";
            var gradient = ((float)color.A + color.B + color.G + color.A) / (255 * 4);
            return characters[(int)Math.Floor((gradient * (characters.Length - 1)))];
        }
        static void ConvertImageToAscii(string srcPath, string srcDest, double sizeModifier = 1)
        {
            using (var bmp = new Bitmap(Image.FromFile(srcPath)))
            using (var resized = Resize(bmp, (int)Math.Floor(sizeModifier * bmp.Width), (int)Math.Floor(sizeModifier * bmp.Height)))
            {
                var sb = new StringBuilder();
                for (int row = 0; row < resized.Height; row++)
                {
                    for (int col = 0; col < resized.Width; col++)
                        sb.Append(PixelToChar(resized.GetPixel(col, row)));
                    sb.Append(Environment.NewLine);
                }
                File.WriteAllText(srcDest, sb.ToString());
            }
        }
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                if (File.Exists(Path.GetFullPath(args[0])))
                {
                    double sizeModifier = 1;
                    if (args.Length > 2)
                    {
                        double.TryParse(args[2], out sizeModifier);
                    }

                    ConvertImageToAscii(args[0], args[1], sizeModifier);
                    return;
                }
            }
            Console.WriteLine("Conversion did not work, file wasn't created");
        }
    }
}
