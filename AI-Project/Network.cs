using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class Network
    {
        List<List<Neuron>> _layers;

        int _numOfLayers;
        int[] _numOfNeurons;
        int[] _possibleOutputs;

        Random _random = new Random();

        public Network (int num_of_layers, int[] num_of_neurons, int[] possible_outputs)
        {
            if (num_of_neurons.Length != num_of_layers) throw new ArgumentException("The number o layers does not match with the lenght of list");

            _numOfLayers = num_of_layers;
            _numOfNeurons = num_of_neurons;
            _possibleOutputs = possible_outputs;

            _genNetwork();
        }

        public int _process (double[] inputs)
        {
            foreach (List<Neuron> layer in _layers)
            {
                foreach (Neuron neuron in layer)
                {
                    if (neuron.IsInput) { neuron.value = inputs[layer.IndexOf(neuron)]; continue; }

                    neuron.Activate();                    
                }
            }
            Neuron result_neuron = _layers.Last().First();
            foreach (Neuron out_neuron in _layers.Last().Skip(1))
            {
                if (out_neuron.value > result_neuron.value) result_neuron = out_neuron;
            }
            return _possibleOutputs[_layers.Last().IndexOf(result_neuron)];
        }

        public void Learn (List<double[]> inputs, List<short> outputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                double[] entry = inputs[i];
                short desired_out = outputs[i];

                int result = _process(entry);

            }
        }

        public void PrintLayers ()
        {
            foreach (List<Neuron> layer in _layers)
            {
                foreach (Neuron n in layer)
                {
                    Console.WriteLine($"ID: {n.ID} Layer: {_layers.IndexOf(layer)} WasActivated: {n.wasActivated} Value: {n.value}");
                }
            }
        }

        private int _propagate ()
        {
            return 0;
        }
        
        private void _genNetwork ()
        {
            _layers = new List<List<Neuron>>();
            for (int i = 0; i < _numOfLayers; i++)
            {
                _genLayer (i);
            }
        }

        private void _genLayer (int layer_num)
        {
            List<Neuron> layer = new List<Neuron>();
            for (int i = 0; i < _numOfNeurons[layer_num]; i++)
            {
                // gen neuron 
                List<Neuron> inputs = layer_num != 0 ? _layers[layer_num - 1] : new List<Neuron>();

                // gen wages 
                List<double> wages = new List<double>();
                for (int j = 0; j < _numOfNeurons[layer_num]; j++)
                {
                    wages.Add (_random.NextDouble ());
                }

                // check if the neuron is in the first or last layer
                Neuron n = new Neuron(inputs, wages, (float) _random.NextDouble(), layer_num == 0, layer_num == _numOfLayers - 1);

                layer.Add(n);
            }
            _layers.Add(layer);
        }
    }
}
