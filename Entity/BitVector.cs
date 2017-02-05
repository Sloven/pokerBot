using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BitVector
    {
        public int id { get; set; }
        public byte[] ByteArray { get; set; }
        public byte[] CropedImg { get; set; }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public CardOwner Owner {get;set;}

        public double[] GetBiPolarVector()
        {
            double[] darr = new double[ByteArray.Length];
            for (int i = 0; i < ByteArray.Length; i++)
            {
                darr[i] = ByteArray[i] > 0 ? 0.5 : -0.5;
            }
            return darr;
        }

        public void SetBitArray(List<double> vector)
        {
            ByteArray = new byte[vector.Count];
            for (int i = 0; i < vector.Count; i++)
                ByteArray[i] = vector[i] > 0 ? (byte)1 : (byte)0;
        }

        public static byte[] SetImage(System.Drawing.Image ImageIn)
        {
            MemoryStream ms = new MemoryStream();
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image GetImage(byte[] imArr)
        {
            MemoryStream ms = new MemoryStream(imArr);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
