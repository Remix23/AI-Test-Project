using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal static class Extensions
    {

        /*
         extensions -> funny way to add methods to a given class without recompaling it or creating a child class 
         */
        public static int ProcessReadInt32 (this BinaryReader br)
        {
            byte[] bytes = br.ReadBytes(sizeof(int));

            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
