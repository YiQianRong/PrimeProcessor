using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Processor;

namespace Processor.Test
{
    class ShapeProcessorTest
    {
        //The size of a shap is the number of pixels in a shap
        //To find out the maximum size of shap in a bitmap - Shape0.bmp
        [Test]
        public void MaxShapeSizeTest()
        {
            var processor = new ShapeProcessor("Shape0", "bmp");
            int size = processor.GetMaxSizeOfShape();
            Assert.AreEqual(size, 6833);

            processor = new ShapeProcessor("Shape1", "bmp");
            size = processor.GetMaxSizeOfShape();
            Assert.AreEqual(size, 6544);
        }
    }
}
