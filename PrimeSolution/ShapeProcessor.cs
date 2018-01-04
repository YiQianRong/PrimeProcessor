using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using MicrosoftResearch.Infer.Collections;
using System.Drawing.Imaging;

namespace Processor
{
    public class Line
    {
        public int FirstPixel { get; set; }
        public int LastPixel { get; set; }
    }

    public class BlackPixel : IComparer<BlackPixel>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BlackPixel(int i, int j)
        {
            X = i;
            Y = j;
        }

        public int Compare(BlackPixel one, BlackPixel two)
        {
            int compareResult = 0;

            compareResult = one.Y.CompareTo(two.Y);
            if(compareResult == 0)
            {
                compareResult = one.X.CompareTo(two.X);
            }

            return compareResult;
        }
    }

    // Defines a comparer to create a sorted set
    // that is sorted by the BlackPixel
    public class SortedByBlackPixel : IComparer<BlackPixel>
    {
        public int Compare(BlackPixel x, BlackPixel y)
        {
            return x.Compare(x, y);
        }
    }

    public class ShapeProcessor
    {
        protected Bitmap image;
        protected string FileName;
        protected string FileExt;
        protected int SizeOfShape;
        protected MicrosoftResearch.Infer.Collections.SortedSet<BlackPixel> BlackPixelList;

        public ShapeProcessor(string filename, string fileExt)
        {
            FileName = filename;
            FileExt = fileExt;
        }

        public void LoadImage()
        {
            String executablePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = String.Format(@"{0}\..\..\{1}.{2}", executablePath, FileName, FileExt);
            image = new Bitmap(path);
            SizeOfShape = 0;
        }

        public void Save(string filename)
        {
            String executablePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = String.Format(@"{0}\..\..\{1}", executablePath, filename);
            image.Save(path);
        }

        protected bool Fine_A_Black_Pixel(ref int x, ref int y)
        {
            bool bFound = false;
            var pict = image.Size;
            for (int j = y; j < pict.Height; j++)
            {
                for (int i = 0; i < pict.Width; i++)
                {
                    if (CompareColors(image.GetPixel(i, j), Color.Black))
                    {
                        x = i;
                        y = j;
                        bFound = true;
                        return bFound;
                    }
                }
            }

            return bFound;
        }

        //The size of a shape is the number of pixels in a shape
        //To find out the maximum size of shape in a bitmap - Shape0.bmp
        public int GetMaxSizeOfShape()
        {
            LoadImage();
            //To store size of shapes
            var sizeList = new MicrosoftResearch.Infer.Collections.SortedSet<int>();
            //To store black pixels on top of red lines
            BlackPixelList = new MicrosoftResearch.Infer.Collections.SortedSet<BlackPixel>(new SortedByBlackPixel());

            int size = 0;
            var pict = image.Size;
            int shapeNumber = 1;
            int i = 0;
            int j = 0;
            bool bFound = false;
            do
            {
                bFound = Fine_A_Black_Pixel(ref i, ref j);

                if (bFound)
                {
                    int currShapeSize = FindShapeSize(i, j);
                    if (currShapeSize > 0 && !sizeList.Contains(currShapeSize))
                    {
                        sizeList.Add(currShapeSize);
                        string filename = String.Format("{0}_{1}_{2}.{3}", FileName, shapeNumber, currShapeSize, FileExt);
                        shapeNumber++;
                        Save(filename);
                    }
                }

            } while (bFound);

            if (sizeList.Count > 0)
            {
                size = sizeList[sizeList.Count - 1];
            }
            return size;
        }

        protected bool CompareColors(Color one, Color two)
        {
            return one.ToArgb().Equals(two.ToArgb());
        }

        protected bool Previous_Row_Has_Any_Red_Pixel(int j, Line line)
        {
            //To check previous row has one red pixel
            for (int i = line.FirstPixel; i <= line.LastPixel; i++)
            {
                if (j > 0 && CompareColors(image.GetPixel(i, j - 1), Color.Red))
                {
                    return true;
                }
            }
            return false;
        }

        protected List<Line> Find_Start_End_Pixel_On_Lines(int j, int y)
        {
            int x = 0;
            var list = new List<Line>();
            var pict = image.Size;
            //To find one black pixel in a row
            for (int f = 0; f < pict.Width; f++)
            {
                if (CompareColors(image.GetPixel(f, j), Color.Black))
                {
                    x = f;
                    break;
                }
            }

            do
            {
                var line = new Line();
                if (CompareColors(image.GetPixel(x, j), Color.Black))
                {
                    //Find first black pixel in a row
                    for (int b = x; b >= 0; b--)
                    {
                        if (CompareColors(image.GetPixel(b, j), Color.Black))
                        {
                            line.FirstPixel = b;
                            line.LastPixel = b;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //Find last black pixel in a row
                    for (int e = x; e < pict.Width; e++)
                    {
                        if (CompareColors(image.GetPixel(e, j), Color.Black))
                        {
                            line.LastPixel = e;
                        }
                        else
                        {
                            x = e - 1;
                            break;
                        }
                    }

                    bool prevRowHasRedPixel = true;
                    //Start from 2nd line
                    if (j > y)
                    {
                        //To check previous row has one red pixel
                        prevRowHasRedPixel = Previous_Row_Has_Any_Red_Pixel(j, line);
                    }

                    if (prevRowHasRedPixel)
                    {
                        list.Add(line);
                    }
                }

                x++;
            } while (x < pict.Width && j > y);


            return list;
        }

        protected void Count_Pixels_On_A_Line_Change_It_To_Red(int j, Line line)
        {
            for (int i = line.FirstPixel; i <= line.LastPixel; i++)
            {
                if (CompareColors(image.GetPixel(i, j), Color.Black))
                {
                    //To count black pixel, then set it to red
                    SizeOfShape++;
                    image.SetPixel(i, j, Color.Red);

                    //To find out if a black pixel on top of red one, then store it to BlackPixelList
                    if (j > 0 && CompareColors(image.GetPixel(i, j - 1), Color.Black))
                    {
                        var cell = new BlackPixel(i, j - 1);
                        BlackPixelList.Add(cell);
                    }

                    if(BlackPixelList.Count > 0)
                    {
                        //To find out if this pixel is in BlackPixelList, then remove it
                        var cell = new BlackPixel(i, j);
                        if (BlackPixelList.Contains(cell))
                        {
                            BlackPixelList.Remove(cell);
                        }
                    }
                }
                //End when it is while pixel
                else if (CompareColors(image.GetPixel(i, j), Color.White))
                {
                    break;
                }
            }
        }

        public int FindShapeSize(int x, int y)
        {
            //Return 0 this pattern when White or Red
            if (CompareColors(image.GetPixel(x, y), Color.White) || CompareColors(image.GetPixel(x, y), Color.Red))
            {
                return 0;
            }

            SizeOfShape = 0;
            var pict = image.Size;
            for (int j = y; j < pict.Height; j++)
            {
                var lineList  = Find_Start_End_Pixel_On_Lines(j, y);

                foreach (var line in lineList)
                {
                    Count_Pixels_On_A_Line_Change_It_To_Red(j, line);
                }
            }

            //To count black area on top of red lines like V, W shape
            Count_Black_Area();

            return SizeOfShape; 
       
        }

        //To count black area on top of red lines like V, W shape
        protected void Count_Black_Area()
        {
            while(BlackPixelList.Count > 0)
            {
                var cell = BlackPixelList[0];
                Line line = Find_Start_End_Pixel_On_A_Line(cell);
                Count_Pixels_On_A_Line_Change_It_To_Red(cell.Y, line);
            }
        }

        protected Line Find_Start_End_Pixel_On_A_Line(BlackPixel cell)
        {
            var pict = image.Size;
            var line = new Line();
            if (CompareColors(image.GetPixel(cell.X, cell.Y), Color.Black))
            {
                //Find first black pixel in a row
                for (int b = cell.X; b >= 0; b--)
                {
                    if (CompareColors(image.GetPixel(b, cell.Y), Color.Black))
                    {
                        line.FirstPixel = b;
                        line.LastPixel = b;
                    }
                    else
                    {
                        break;
                    }
                }

                //Find last black pixel in a row
                for (int e = cell.X; e < pict.Width; e++)
                {
                    if (CompareColors(image.GetPixel(e, cell.Y), Color.Black))
                    {
                        line.LastPixel = e;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return line;
        }

    }
}
