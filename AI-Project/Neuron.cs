using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class Neuron
    {

        public double value;

        public double err;

        public int ID;

        private double _beta;

        static int neuron_counter = 0;

        public Neuron(float beta)
        {
            ID = neuron_counter++;
            value = 0;
            _beta = beta;
            err = 0;
        }

        static public double DotProduct (Neuron[] in_neurons, double[] in_weights)
        {
            double inputs_sum = 0;

            for (int i = 0; i < in_neurons.Count(); i++)
            {
                inputs_sum += in_neurons[i].value * in_weights[i];
            }
            return inputs_sum;
        }

        static public double Sigmoid (double value, double beta)
        {
            return 1.0 / (1.0 + Math.Exp(-value * beta));
        }

        static public double SidmoidDerr (double value, double beta)
        {
            return Sigmoid(value, beta) * (1 - Sigmoid(value, beta));
        }

        public double Activate (Neuron[] in_neurons, double[] in_weighs)
        {
            value = Sigmoid(DotProduct(in_neurons, in_weighs), _beta);
            return value;
        }

        public double CalcDeltaWeight (double raw_input, double niu)
        {
            return niu * err * SidmoidDerr(value, _beta) * raw_input;
        }
    }
}
