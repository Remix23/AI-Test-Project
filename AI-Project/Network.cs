using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project
{
    internal class Network
    {
        List<Layer> _layers;
        double[] _biases;

        int _numOfLayers;
        int[] _numOfNeurons;
        int[] _possibleOutputs;

        Random _random = new Random();

        public Network (int num_of_layers, int[] num_of_neurons, int[] possible_outputs, double[] biases = null)
        {
            if (num_of_neurons.Length != num_of_layers) throw new ArgumentException("The number of layers does not match with the lenght of list");

            _numOfLayers = num_of_layers;
            _numOfNeurons = num_of_neurons;
            _possibleOutputs = possible_outputs;

            if (biases != null)
            {
                _biases = biases;
            } else
            {
                _genBiases();
            }
            
            _genNetwork();
        }

        private void _getInput (byte[] inputs)
        {
            for (int i = 0; i < _numOfNeurons[0]; i++)
            {
                _layers.First().Neurons[i].value = (double)inputs[i];
            }
        }

        public int ProcessInput (byte[] inputs)
        {
            _getInput(inputs); // store the input in the first layer of the network 
            for (int i = 1; i < _numOfLayers; i++)
            {
                _layers[i].Activate(_layers[i - 1].Neurons);
            }

            // chose the biggest value in the last layer
            int max_value_index = 0;
            Layer last_layer = _layers.Last();
            for (int i = 1; i < last_layer.NumOfNeurons; i++)
            {
                if (last_layer.Neurons[i].value > last_layer.Neurons[max_value_index].value) max_value_index = i;
            }
            return max_value_index;
        }

        public double Learn (List<IMGObj> dataset)
        {
            double total_cost = 0;
            for (int i = 0; i < dataset.Count; i++)
            {
                int result_index = ProcessInput(dataset[i].pixels);
                int network_out = _possibleOutputs[result_index];

                int correct_answer_index = Array.IndexOf(_possibleOutputs, (int)dataset[i].label);
                _propagateErr(correct_answer_index);
                _updateWeights();
            }
            return total_cost / dataset.Count; // return the average cost (err) of test
        }

        public void PrintLayers ()
        {
            foreach (Layer layer in _layers)
            {
                foreach (Neuron n in layer.Neurons)
                {
                    Console.WriteLine($"ID: {n.ID} Layer: {_layers.IndexOf(layer)} Value: {n.value} Err: {n.err}");
                }
            }
        }

        public void PrintWeights ()
        {
            for (int i = 1; i < _layers.Count;i++)
            {
                _layers[i].PrintWeights(i);
            }
        }

        private void _propagateErr (int correct_answer_index)
        {
            _layers.Last().CalcFinalErr(correct_answer_index);
            for (int i = _numOfLayers - 2; i >= 0; i--)
            {
                _layers[i].CollectErr(_layers[i + 1]);
            }
        }

        private void _updateWeights ()
        {
            for (int i = 1; i < _numOfLayers; i++)
            {
                _layers[i].UpdateWeights(_layers[i - 1].Neurons);
            }
        }
        
        private void _genNetwork ()
        {
            _layers = new List<Layer> ();
            for (int i = 0; i < _numOfLayers; i++)
            {
                _genLayer (i);
            }
        }

        private void _genLayer (int layer_num)
        {
            // gen beta attribute
            double beta = _random.NextDouble();

            Neuron[] neurons = new Neuron[_numOfNeurons[layer_num]];

            double[][] weighs = new double[_numOfNeurons[layer_num]][];
            for (int i = 0; i < _numOfNeurons[layer_num]; i++)
            {
                // gen neuron 
                neurons[i] = new Neuron((float)beta);

                // gen wages 
                
                if (layer_num > 0)
                {
                    weighs[i] = new double[_numOfNeurons[layer_num - 1]];
                    for (int j = 0; j < _numOfNeurons[layer_num - 1]; j++)
                    {
                        weighs[i][j] = _random.NextDouble();
                        if (_random.NextDouble() > 0.5) weighs[i][j] *= -1;
                        
                    }
                } else
                {
                    weighs[i] = new double[0];
                }
            }
            bool is_in = layer_num == 0;
            bool is_out = layer_num == _numOfLayers - 1;
            _layers.Add(new Layer(_numOfNeurons[layer_num], neurons, weighs, is_in, is_out, 0.5, _biases[layer_num]));
        }

        private void _genBiases ()
        {
            _biases = new double[_numOfLayers];
            for (int i = 0; i < _numOfLayers; i++)
            {
                _biases[i] = _random.NextDouble();
            }
        }

        public void PrintStructure ()
        {
            for (int i = 0; i < _numOfLayers;i++)
            {
                Layer l = _layers[i];
                Console.WriteLine($"Layer nr: {_layers.IndexOf(l)}");

                for (int j = 0; j < l.NumOfNeurons; j++)
                {
                    int total_connections = l.Weights[j].Length;
                    if (_layers.IndexOf(l) != _numOfLayers - 1) total_connections += _layers[i + 1].NumOfNeurons;
                    Console.WriteLine($"Neuron nr: {j} num of connection: {total_connections} ({l.Weights[j].Length} - input; {total_connections - l.Weights[j].Length} - output)");
                }
            }
        }
    }
}
