//MIT License

//Copyright(c) 2018 thegreatpl

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

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
        /// <summary>
        /// Place where the image files will be created. 
        /// </summary>
        static string PngDirectory = @"D:\Users\thegreatpl\Desktop\personal projects\Unicode images\blackbackground";

        /// <summary>
        /// Background colour of the resulting png images. 
        /// </summary>
        static Color background = Color.Black;

        /// <summary>
        /// foreground colour of the resulting png images. 
        /// </summary>
        static Color foreground = Color.White;

        /// <summary>
        /// Some characters are designed for drawing graphs, and are in the 8*16 block of characters, so we want to 
        /// extend the last column on either side to the new edge. 
        /// </summary>
        static List<string> ExpandEdges = new List<string>()
        {
            "U+2500",
            "U+2501",
            "U+2502",
            "U+2503",
            //"U+2504",
            //"U+2505",
            //"U+2506",
            //"U+2507",
            //"U+2508",
            //"U+2509",
            //"U+250A",
            //"U+250B",
            "U+250C",
            "U+250D",
            "U+250E",
            "U+250F",
            "U+2510",
            "U+2511",
            "U+2512",
            "U+2513",
            "U+2514",
            "U+2515",
            "U+2516",
            "U+2517",
            "U+2518",
            "U+2519",
            "U+251A",
            "U+251B",
            "U+251C",
            "U+251D",
            "U+251E",
            "U+251F",
            "U+2520",
            "U+2521",
            "U+2522",
            "U+2523",
            "U+2524",
            "U+2525",
            "U+2526",
            "U+2527",
            "U+2528",
            "U+2529",
            "U+252A",
            "U+252B",
            "U+252C",
            "U+252D",
            "U+252E",
            "U+252F",
            "U+2530",
            "U+2531",
            "U+2532",
            "U+2533",
            "U+2534",
            "U+2535",
            "U+2536",
            "U+2537",
            "U+2538",
            "U+2539",
            "U+253A",
            "U+253B",
            "U+253C",
            "U+253D",
            "U+253E",
            "U+253F",
            "U+2540",
            "U+2541",
            "U+2542",
            "U+2543",
            "U+2544",
            "U+2545",
            "U+2546",
            "U+2547",
            "U+2548",
            "U+2549",
            "U+254A",
            "U+254B",
            //"U+254C",
            //"U+254D",
            //"U+254E",
            //"U+254F",
            "U+2550",
            "U+2551",
            "U+2552",
            "U+2553",
            "U+2554",
            "U+2555",
            "U+2556",
            "U+2557",
            "U+2558",
            "U+2559",
            "U+255A",
            "U+255B",
            "U+255C",
            "U+255D",
            "U+255E",
            "U+255F",
            "U+2560",
            "U+2561",
            "U+2562",
            "U+2563",
            "U+2564",
            "U+2565",
            "U+2566",
            "U+2567",
            "U+2568",
            "U+2569",
            "U+256A",
            "U+256B",
            "U+256C",
            "U+256D",
            "U+256E",
            "U+256F",
            "U+2574",
            "U+2575",
            "U+2576",
            "U+2577",
            "U+2578",
            "U+2579",
            "U+257A",
            "U+257B",
            "U+257C",
            "U+257D",
            "U+257E",
            "U+257F",

        };  


        static void Main(string[] args)
        {
            //for(int idx = 9472;  idx< 9584; idx++)
            //{
            //    Console.WriteLine($"U+{idx.ToString("x")}");
            //}
            //Console.ReadLine(); 




            //first plance of unicode. 
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


            if (!Directory.Exists(PngDirectory))
                Directory.CreateDirectory(PngDirectory);

            CreatePlane(PngDirectory, unifont, hexes);

            //second plane. 
            var plane1 = new Bitmap("unifont_upper-11.0.01.bmp");

            CreatePlane(PngDirectory, plane1, new Dictionary<string, string>(), "1"); 

        }

        /// <summary>
        /// Creates png images of all characters in a plane. 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="unifont"></param>
        /// <param name="hexes"></param>
        /// <param name="plane"></param>
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
                    character = ConvertColours(character); 

                    if (hexes.ContainsKey(name) && hexes[name].Length < 33)
                    {
                        character = Move16x8ToCenter(character); 
                    }

                    

                    character.Save($"{directory}/U+{name}.png", System.Drawing.Imaging.ImageFormat.Png);

                    if (ExpandEdges.Contains("U+"+ name))
                    {
                        ExtendFromCenter(character).Save($"{directory}/U+{name}_extended.png", System.Drawing.Imaging.ImageFormat.Png);
                    }

                    y++; 
                }
                x++; 
            }
        }

        /// <summary>
        /// Some of the characters are 16*8 instead of 16*16. This will center the characters so that they are always in the middle. 
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap Move16x8ToCenter(Bitmap original)
        {
            var newbitmap = new Bitmap(16, 16);
            for (int xdx = 0; xdx < newbitmap.Width; xdx++)
                for (int ydx = 0; ydx < newbitmap.Height; ydx++)
                    newbitmap.SetPixel(xdx, ydx, background);

            for (int xdx = 0; xdx <=8; xdx++)
            {
                for (int ydx = 0; ydx < original.Height; ydx++)
                {
                    newbitmap.SetPixel(xdx + 4, ydx, original.GetPixel(xdx, ydx)); 
                }
            }

            return newbitmap; 
        }

        /// <summary>
        /// Some of the tiles can be used for walls or graph drawing, but are in the 8*16 section. Since we expanded these out to 16*16, 
        /// they don't reach the edge of the tile. This will take the last row and fill the remaining spots. 
        /// </summary>
        /// <param name="centered"></param>
        /// <returns></returns>
        public static Bitmap ExtendFromCenter(Bitmap centered)
        {
            for(int ydx = 0; ydx < 16; ydx++)
            {
                for (int xdx = 0; xdx < 4; xdx++)
                {
                    centered.SetPixel(xdx, ydx, centered.GetPixel(4, ydx)); 
                }
                for (int xdx = 11; xdx < 16; xdx++)
                {
                    centered.SetPixel(xdx, ydx, centered.GetPixel(11, ydx));
                }
            }
            return centered; 
        }

        /// <summary>
        /// Switches the colors into the ones given at the top. 
        /// </summary>
        /// <param name="oldbitmap"></param>
        /// <returns></returns>
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
