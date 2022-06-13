using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num_of_layers = 3;
            int[] num_of_neurons_layer = { 10, 5, 10 };
            int[] possible_outputs = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Network Test = new Network(num_of_layers, num_of_neurons_layer, possible_outputs);

            string imgs = "train-images.idx3-ubyte";
            string labels = "train-labels.idx1-ubyte";

            List<IMGObj> dataset = MNISTReader.ReadMNISTISet(imgs, labels, 1);

            Console.WriteLine($"Number of test samples: {dataset.Count}");
            Console.WriteLine($"Picture size: {dataset[0].width} x {dataset.First().height}");
            Test.PrintWeights();
            /*            Console.WriteLine(Test.ProcessInput(dataset[1].pixels));*/
            Console.WriteLine(Test.Learn(dataset));

            Test.PrintWeights();

            Console.Read();
        }
    }
}
