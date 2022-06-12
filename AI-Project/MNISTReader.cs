using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal static class MNISTReader
    {


        public static List<IMGObj> ReadMNISTISet (string img_filepath, string labels_filepath, int num_of_img_to_read)
        {
            
            List<IMGObj> result = new List<IMGObj> ();

            BinaryReader img_reader = new BinaryReader (new FileStream (img_filepath, FileMode.Open));
            BinaryReader labels_reader = new BinaryReader(new FileStream(labels_filepath, FileMode.Open));

            int img_magic_number = img_reader.ProcessReadInt32();
            int num_of_imgs = img_reader.ProcessReadInt32();

            int img_width = img_reader.ProcessReadInt32();
            int img_height = img_reader.ProcessReadInt32();

            int label_magic_number = labels_reader.ProcessReadInt32();
            int label_num_of_items = labels_reader.ProcessReadInt32();

            for (int i = 0; i < num_of_imgs && i < num_of_img_to_read; i++)
            {
                byte[] data = img_reader.ReadBytes(img_width * img_height); 
                byte label = labels_reader.ReadByte();
                result.Add (new IMGObj(data, img_width, img_height, label));
            }

            img_reader.Close();
            labels_reader.Close();

            return result;
        }
    }
}
