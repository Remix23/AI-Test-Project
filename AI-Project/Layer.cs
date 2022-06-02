using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class Layer
    {
        public int NumOfNeurons;
        public Neuron[] Neurons;
        public double[][] Weights;

        public bool IsOut;
        public bool IsIn;

        private double _niu;

        public Layer (int num_of_neurons, Neuron[] neurons, double[][] weighs, bool is_in, bool is_out, double niu)
        {
            NumOfNeurons = num_of_neurons;
            Neurons = neurons;
            Weights = weighs;
            IsIn = is_in;
            IsOut = is_out;

            _niu = niu;
        }

        public void Activate (Neuron[] in_neutrons)
        {
            for (int neuron_count = 0; neuron_count < NumOfNeurons; neuron_count++)
            {
                Neurons[neuron_count].Activate(Neuron.DotProduct(in_neutrons, Weights[neuron_count]));
            }
        }

        public void CollectErr (Layer from_layer)
        {
            for (int desc_n_counter = 0; desc_n_counter < NumOfNeurons; desc_n_counter++)
            {
                double err_sum = 0;
                for (int from_n_counter = 0; from_n_counter < from_layer.NumOfNeurons; from_n_counter++)
                {
                    err_sum += from_layer.Neurons[from_n_counter].err * Weights[desc_n_counter][from_n_counter];
                }
                Neurons[desc_n_counter].err = err_sum;
            }
        }

        public void UpdateWeights (Neuron[] previous_layer_neurons)
        {
            for (int neuron_counter = 0; neuron_counter < NumOfNeurons; neuron_counter++)
            {
                for (int weight_counter = 0; weight_counter < Weights[neuron_counter].Count(); weight_counter++)
                {
                    Weights[neuron_counter][weight_counter] += Neurons[neuron_counter].CalcDeltaWeight(previous_layer_neurons[weight_counter].value, _niu);
                }
            }
        }
    }
}
