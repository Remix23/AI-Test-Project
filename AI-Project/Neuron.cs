using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    internal class Neuron
    {
        List<Neuron> _inputNeurons;
        List<double> _inputWages;

        List<Neuron> _outNeurons = new List<Neuron>();
        List<double> _outWages = new List<double>();

        public double value;
        public bool wasActivated;

        public double err;

        public bool IsInput;
        public bool IsOut;

        public int ID;

        private float _beta;

        static int neuron_counter = 0;

        public Neuron(List<Neuron> input_layer, List<double> in_wages, float beta, bool is_inut, bool is_out)
        {
            ID = neuron_counter++;
            _inputNeurons = input_layer;
            _inputWages = in_wages;
            IsInput = is_inut;
            IsOut = is_out;
            value = 0;
            wasActivated = false;
            _beta = beta;
            err = 0;
        }

        public void Activate ()
        {
            double inputs_sum = 0;

            for (int i = 0; i < _inputNeurons.Count; i++)
            {
                double n_value = _inputNeurons[i].value * _inputWages[i];
                inputs_sum += n_value;

                if (_inputNeurons.Count < _inputNeurons.Count)
                {
                    _inputNeurons.Add(_inputNeurons[i]);
                    _inputWages.Add(_inputWages[i]);
                }
            }

            value = 1 / (1 + Math.Exp(- _beta * inputs_sum));
            wasActivated = value > 0;
        }
    }
}
