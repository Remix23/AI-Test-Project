using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class IMGObj
    {
        public byte[] pixels;

        public int width;
        public int height;

        public byte label; // correct answer

        public IMGObj(byte[] pixels, int width, int height, byte label)
        {
            this.pixels = pixels;
            this.width = width;
            this.height = height;
            this.label = label;
        }

        public (int, int) GetPixelXY (int index)
        {
            return (index % width, index / height);
        }
        public int GetPixelByXY (int x, int y)
        {
            return pixels[y * width + x];
        }
    }
}
