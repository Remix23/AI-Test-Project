using System;
using System.Linq;

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
        private double _bias;

        public Layer (int num_of_neurons, Neuron[] neurons, double[][] weighs, bool is_in, bool is_out, double niu, double bias)
        {
            NumOfNeurons = num_of_neurons;
            Neurons = neurons;
            Weights = weighs; // [i][j] i - numbr of neuron in this layer; j - number of neuron in the previous layer 
            IsIn = is_in;
            IsOut = is_out;

            _niu = niu;
            _bias = bias;
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
            for (int desc_n_counter = 0; desc_n_counter < NumOfNeurons; desc_n_counter++) // desc - destination (neurons in the currently processing layer)
            {
                double err_sum = 0;
                for (int from_n_counter = 0; from_n_counter < from_layer.NumOfNeurons; from_n_counter++)
                {
                    err_sum += from_layer.Neurons[from_n_counter].err * from_layer.Weights[from_n_counter][desc_n_counter];
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
                    Weights[neuron_counter][weight_counter] += Neurons[neuron_counter].CalcDeltaWeight(previous_layer_neurons[weight_counter].value, _niu); // err value comes from the current neuron
                }
            }
        }

        public double CalcFinalErr (int correct_index)
        {
            if (!IsOut) return 0.0;
            double total_cost = 0;

            for (int i = 0; i < NumOfNeurons; i++)
            {
                double cost;
                if (i == correct_index)
                {
                    cost = (Neurons[i].value - 1.0) * (Neurons[i].value - 1.0); // network gave correct answer -> the cost is lower
                } else
                {
                    cost = Neurons[i].value * Neurons[i].value;
                }
                total_cost += cost;
                Neurons[i].err = cost;
            }
            return total_cost;
        }

        public void PrintWeights (int layer_num)
        {
            for (int i = 0; i < NumOfNeurons; i++)
            {
                for (int j = 0; j < Weights[i].Count(); j++)
                {
                    Console.WriteLine($"layer: {layer_num - 1} neuron: {j} <- {Weights[i][j]} -> neuron: {i} layer: {layer_num}");
                }
            }
        }
    }
}
