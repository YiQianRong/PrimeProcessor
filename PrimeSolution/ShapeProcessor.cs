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

        public ShapeProcessor(string filename)
        {
            String executablePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            image = new Bitmap(executablePath + @"\..\..\" + filename);
        }

        public void Save()
        {
            String executablePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            image.Save(executablePath + @"\..\..\Shape2.bmp");
        }

        //The size of a shap is the number of pixels in a shap
        //To find out the maximum size of shap in a bitmap - Shape0.bmp
        public int GetMaxSizeOfShape()
        {
            var sizeList = new MicrosoftResearch.Infer.Collections.SortedSet<int>();
            int size = 0;

            var pict = image.Size;
            for(int j = 0; j< pict.Height; j++)
            {
                for(int i = 0; i< pict.Width; i++)
                {
                    int currShapSize = FindShapSize(i, j);

                    if(currShapSize > 0 && !sizeList.Contains(currShapSize))
                    {
                        sizeList.Add(currShapSize);
                        //Save();
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

        public int FindShapSize(int x, int y)
        {
            //Return 0 this pattern
            //W current row
            //R current row
            if (CompareColors(image.GetPixel(x, y), Color.White) || CompareColors(image.GetPixel(x, y), Color.Red))
            {
                return 0;
            }

            int size = 0;
            var pict = image.Size;
            for (int j = y; j < pict.Height; j++)
            {
                int iFirst = x;
                int iEnd = x;
                var color = image.GetPixel(x, j);

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
                        } else
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
                            for (int f = e; f < pict.Width; f++)
                            {
                                var color1 = image.GetPixel(f, j);
                                if (CompareColors(image.GetPixel(f, j), Color.Black))
                                {
                                    e = f;
                                }
                            }
                            break;
                        }
                    }
                }

                if (j > y)
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

                    //Previous row does not have any red pixel, return.
                    if (!prevRowHasRedPixel)
                    {
                        j = pict.Height;
                        break;
                    }

                }

                for (int i = iFirst; i <= iEnd; i++)
                {
                    //To count black pixel, then set it to red
                    if (CompareColors(image.GetPixel(i, j), Color.Black))
                    {
                        size++;
                        image.SetPixel(i, j, Color.Red);
                    }
                    //End when it is while pixel
                    else if (CompareColors(image.GetPixel(i, j), Color.White))
                    {
                        bool bContinue = false;
                        //Find next black for shap like five star
                        //for (int k = i+1; k < pict.Width; k++)
                        //{
                        //    if (CompareColors(image.GetPixel(k, j), Color.Black))
                        //    {
                        //        i = k - 1;
                        //        bContinue = true;
                        //        break;
                        //    }else 
                        //    if (CompareColors(image.GetPixel(k, j), Color.Black))
                        //    {
                        //        bContinue = false;
                        //        break;
                        //    }
                        //}

                        if (bContinue)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }

                }

                //End of Row
            }
            return size; 
       
        }

    }
}
