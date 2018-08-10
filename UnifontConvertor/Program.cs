using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnifontConvertor
{
    class Program
    {
        static Color background = Color.Transparent;
        static Color foreground = Color.White;
        static void Main(string[] args)
        {
            Bitmap unifont = new Bitmap("unifont-11.0.01.bmp");

            Dictionary<string, string> hexes = new Dictionary<string, string>();

            using (var reader = File.OpenText(@"unifont-10.0.07\font\plane00\unifont-base.hex"))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    var split = str.Split(':');
                    hexes.Add(split[0], split[1]);
                }
            }

            string directory = @"D:\Users\thegreatpl\Desktop\personal projects\Unicode images";// $"{Directory.GetCurrentDirectory()}/output";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            CreatePlane(directory, unifont, hexes);


            var plane1 = new Bitmap("unifont_upper-11.0.01.bmp");

            CreatePlane(directory, plane1, new Dictionary<string, string>(), "1"); 

        }

        static void CreatePlane(string directory, Bitmap unifont, Dictionary<string, string> hexes, string plane = "")
        { 
            //31, 63. 
            int x = 0; 
            for (int xdx = 32; xdx < unifont.Width; xdx += 16)
            {
                int y = 0; 
                for (int ydx = 64; ydx < unifont.Height; ydx += 16)
                {
                    var name = $"{plane}{y.ToString("X2")}{x.ToString("X2")}";
                    Console.WriteLine(name); 
                    var character = unifont.Clone(new Rectangle(xdx, ydx, 16, 16), unifont.PixelFormat);
                    //var pallete = character.Palette;
                    //pallete.Entries[0] = foreground;
                    //pallete.Entries[1] = background;
                    //character.Palette = pallete;
                    character = ConvertColours(character); 

                    if (hexes.ContainsKey(name) && hexes[name].Length < 33)
                    {
                        character = Move16x8ToCenter(character); 
                    }

                    

                    character.Save($"{directory}/U+{name}.png", System.Drawing.Imaging.ImageFormat.Png);
                    y++; 
                }
                x++; 
            }
        }

        public static Bitmap Move16x8ToCenter(Bitmap original)
        {
            var newbitmap = new Bitmap(16, 16); 
           // newbitmap.Palette = original.Palette; 
            for (int xdx = 0; xdx <=8; xdx++)
            {
                for (int ydx = 0; ydx < original.Height; ydx++)
                {
                    newbitmap.SetPixel(xdx + 4, ydx, original.GetPixel(xdx, ydx)); 
                }
            }

            return newbitmap; 
        }


        public static Bitmap ConvertColours(Bitmap oldbitmap)
        {
            var bitmap = new Bitmap(16, 16);
            for (int xdx = 0; xdx < oldbitmap.Width; xdx++)
            {
                for (int ydx = 0; ydx < oldbitmap.Height; ydx++)
                {
                    var oldpix = oldbitmap.GetPixel(xdx, ydx);
                    var white = Color.White; 
                    if (oldpix.Name == "ff000000")
                        bitmap.SetPixel(xdx, ydx, foreground);
                    else
                        bitmap.SetPixel(xdx, ydx, background); 
                }
            }


            return bitmap; 
        }
    }

    
}
