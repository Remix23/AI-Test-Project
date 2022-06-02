using System;
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
            Test.PrintLayers();

            Random _rng = new Random();

            double[] inputs = new double[10];
            for (int i = 0; i < inputs.Count(); i++)
            {
                inputs[i] = (double)i / 100.0;
            }

            Test._process(inputs);

            Test.PrintLayers();

            Console.Read();
        }
    }
}
