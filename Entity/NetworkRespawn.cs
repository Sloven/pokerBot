using AForge.Neuro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class NetworkRespawn
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        [MaxLength]
        public byte[] NetworkStream { get; set; }

        public Network Get()
        {
            Network RestoredNet;
            using (var ms = new MemoryStream(NetworkStream))
            {
                RestoredNet = Network.Load(ms);
            }

            return RestoredNet;
        }

        public void Set(Network nwk)
        {
            using (var ms = new MemoryStream())
            {
                nwk.Save(ms);
                NetworkStream = new byte[ms.Length];
                ms.ToArray().CopyTo(NetworkStream, 0);
            }
        }



        //    public int variables { get; set; }
        //    public int samples { get; set; }
        //    public double SigmoidAlpha { get; set; }
        //    public List<LayerRespawn> Layers { get; set; }


        //    public Network RestoreBipolarSigmoidNetwork()
        //    {
        //        var restoredNet = new ActivationNetwork(new BipolarSigmoidFunction(SigmoidAlpha), variables, samples);

        //        for(int i = 0 ;i<restoredNet.Layers.Length; i++ )
        //        {
        //            for(int j=0;j<restoredNet.Layers[i].Neurons.Length;j++)
        //            {
        //                restoredNet.Layers[i].Neurons[j].Weights = Layers[i].neurons[j].GetWeights();
        //            }
        //        }
        //        foreach (var layer in restoredNet.Layers)
        //        {
        //            foreach (var neuron in layer.Neurons)
        //            {
        //                neuron.
        //            }
        //        }
        //        return null;
        //    }

        //    public NetworkRespawn(string Name)
        //    {
        //    }

        //}

        //public class NeuronRespawn
        //{
        //    public int InputsCount{get;set;}
        //    public double Output{get;set;}
        //    private byte[] bWeights;
        //    private double[] dWeights;

        //    public double[] GetWeights()
        //    {
        //        if (dWeights != null)
        //            return dWeights;
        //        else if (bWeights != null)
        //        {
        //            var stringBuf = Encoding.UTF8.GetString(bWeights);
        //            string[] sDoubles = stringBuf.Split(',');
        //            int count = sDoubles.Length;
        //            dWeights = new double[count];

        //            for (int i = 0; i < count; i++)
        //                dWeights[i] = Double.Parse(sDoubles[i]);

        //            return dWeights;
        //        }
        //        else
        //            throw new Exception("Entity wasn't restored properly from DB");
        //    }

        //    public void SetGetWeights(double[] DWeights)
        //    {
        //        dWeights = new double[DWeights.Length];
        //        DWeights.CopyTo(dWeights,0);

        //        var strDouble = String.Join(",", DWeights);
        //        bWeights = Encoding.UTF8.GetBytes(strDouble);
        //    }
        //}

        //public class LayerRespawn 
        //{
        //    public int InputsCount {get;set;}
        //    public List<NeuronRespawn> neurons { get; set; }
        //    public int neuronsCount;
        //}
    }
}
