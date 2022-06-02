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

       public double Sigmoid ()
        {
            return 1.0 / (1.0 + Math.Exp(-value * _beta));
        }

        public double SidmoidDerr ()
        {
            return Sigmoid() * (1 - Sigmoid());
        }

        public double Activate (double result)
        {
            value = result;
            return Sigmoid();
        }

        public double CalcDeltaWeight (double raw_input, double niu)
        {
            return niu * err * SidmoidDerr() * raw_input;
        }
    }
}
