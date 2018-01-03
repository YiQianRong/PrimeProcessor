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
    public class ShapeProcessor
    {
        protected Bitmap image;
        protected string FileName;
        protected string FileExt;
        protected int SizeOfShape;

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

        //The size of a shape is the number of pixels in a shape
        //To find out the maximum size of shape in a bitmap - Shape0.bmp
        public int GetMaxSizeOfShape()
        {
            LoadImage();
            var sizeList = new MicrosoftResearch.Infer.Collections.SortedSet<int>();
            int size = 0;

            var pict = image.Size;
            int shapeNumber = 1;
            for(int j = 0; j< pict.Height; j++)
            {
                for(int i = 0; i< pict.Width; i++)
                {
                    int currShapeSize = FindShapeSize(i, j);

                    if(currShapeSize > 0 && !sizeList.Contains(currShapeSize))
                    {
                        sizeList.Add(currShapeSize);
                        string filename = String.Format("{0}_{1}_{2}.{3}", FileName, shapeNumber, currShapeSize, FileExt);
                        shapeNumber++;
                        Save(filename);
                    }
                }
            }

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

        protected bool Previous_Row_Has_Any_Red_Pixel(int j, int iFirst, int iEnd)
        {
            //To check previous row has one red pixel
            bool prevRowHasRedPixel = false;
            for (int i = iFirst; i <= iEnd; i++)
            {
                if (j > 0 && CompareColors(image.GetPixel(i, j - 1), Color.Red))
                {
                    prevRowHasRedPixel = true;
                    break;
                }
            }

            return prevRowHasRedPixel;
        }

        protected void Find_Start_End_Pixel_On_A_Line(int x, int j, ref int iFirst, ref int iEnd)
        {
            var pict = image.Size;
            if (!CompareColors(image.GetPixel(x, j), Color.Black))
            {
                //To find one black pixel in a row
                for (int f = x; f < pict.Width; f++)
                {
                    if (CompareColors(image.GetPixel(f, j), Color.Black))
                    {
                        x = f;
                        break;
                    }
                }
            }

            if (CompareColors(image.GetPixel(x, j), Color.Black))
            {
                //Find first black pixel in a row
                for (int b = x; b >= 0; b--)
                {
                    if (CompareColors(image.GetPixel(b, j), Color.Black))
                    {
                        iFirst = b;
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
                        iEnd = e;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        protected void Count_Pixels_On_A_Line_Change_It_To_Red(int j, int iFirst, int iEnd)
        {
            for (int i = iFirst; i <= iEnd; i++)
            {
                //To count black pixel, then set it to red
                if (CompareColors(image.GetPixel(i, j), Color.Black))
                {
                    SizeOfShape++;
                    image.SetPixel(i, j, Color.Red);
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
            //Return 0 this pattern
            //W current row
            //R current row
            if (CompareColors(image.GetPixel(x, y), Color.White) || CompareColors(image.GetPixel(x, y), Color.Red))
            {
                return 0;
            }

            SizeOfShape = 0;
            var pict = image.Size;
            for (int j = y; j < pict.Height; j++)
            {
                int iFirst = x;
                int iEnd = x;

                Find_Start_End_Pixel_On_A_Line(x, j, ref iFirst, ref iEnd);

                //Start from 2nd line
                if (j > y)
                {
                    //To check previous row has one red pixel
                    bool prevRowHasRedPixel = Previous_Row_Has_Any_Red_Pixel(j, iFirst, iEnd);

                    //Previous row does not have any red pixel, return.
                    if (!prevRowHasRedPixel)
                    {
                        return SizeOfShape;
                    }
                }

                Count_Pixels_On_A_Line_Change_It_To_Red(j, iFirst, iEnd);
            }

            return SizeOfShape; 
       
        }

    }
}
