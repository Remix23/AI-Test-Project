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
            int[] num_of_neurons_layer = { 10, 3, 1 };
            int[] possible_outputs = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Network Test = new Network(num_of_layers, num_of_neurons_layer, possible_outputs);
            Test.PrintWeights();

/*            Random _rng = new Random();

            double[] inputs = new double[10];
            for (int i = 0; i < inputs.Count(); i++)
            {
                inputs[i] = (double)i / 100.0;
            }*/

            string imgs = "train-images.idx3-ubyte";
            string labels = "train-labels.idx1-ubyte";

            List<IMGObj> dataset = MNISTReader.ReadMNISTISet(imgs, labels, 50);

            Console.WriteLine($"Dataset lenght: {dataset.Count()}");

            Console.Read();
        }
    }
}
