﻿using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronet
{
    public class Classifier
    {
        public void main()
        {
            // initialize input and output values
            double[][] input = new double[4][] {
                new double[] {0, 0}, new double[] {0, 1},
                new double[] {1, 0}, new double[] {1, 1}
            };
            
            double[][] output = new double[4][] {
                new double[] {0}, new double[] {1},
                new double[] {1}, new double[] {0}
            };

            // create neural network
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(2),
                2, // two inputs in the network
                2, // two neurons in the first layer
                1); // one neuron in the second layer
            
            // create teacher
            BackPropagationLearning teacher =
                new BackPropagationLearning(network);

            bool needToStop = false;
            // loop
            while (!needToStop)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input, output);
                // check error value to see if we need to stop
                needToStop = error < 0.5;
            }
        }
    }
}
