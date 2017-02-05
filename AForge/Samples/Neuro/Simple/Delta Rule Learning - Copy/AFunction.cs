using AForge.Neuro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classifier
{
    public class AFunction: IActivationFunction
    {

        public double Derivative(double x)
        {
            return 0;
        }

        public double Derivative2(double y)
        {
            return 0;
        }

        public double Function(double x)
        {
            return x;
        }
    }
}
