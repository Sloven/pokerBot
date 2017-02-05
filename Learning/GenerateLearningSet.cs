using AForge.Imaging;
using AForge.Imaging.Filters;
using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    //public class GenerateLearningSet
    //{
    //    public static List<BitVector> PassThruBlobs(Bitmap filteredImage, List<Blob> blobs)
    //    {
    //        List<BitVector> CardSet = new List<BitVector>();

    //        foreach (var blob in blobs)
    //        {
    //            //check if this is full size card
    //            double sconst = (double)(blob.Rectangle.Height) / (double)blob.Rectangle.Width;

    //            if ((sconst > 1.4 && sconst < 1.5d) || (sconst > 0.65 && sconst < 0.9))
    //            {
    //                double topLeftSquareWidthConst = 0.34375;
    //                double topLeftSquareHeightConst = 2.2;

    //                double topLeftSquareWidth = blob.Rectangle.Width * topLeftSquareWidthConst;
    //                double topLeftSquareHeight = (topLeftSquareWidth * topLeftSquareHeightConst) -1 ;

    //                Crop crop = new Crop(new Rectangle(blob.Rectangle.X, blob.Rectangle.Y, (int)topLeftSquareWidth, (int)topLeftSquareHeight));
    //                var bmp = crop.Apply(filteredImage);
    //                var unrecognizedVector = Recognizer.GetPlaingCardsSet.GetBitVector(bmp);
    //                CardSet.Add(new BitVector(){
    //                    ByteArray = unrecognizedVector,
    //                    CropedImg = BitVector.SetImage(bmp),
    //                    Rank = Rank.NOT_RECOGNIZED,
    //                    Suit = Suit.NOT_RECOGNIZED
    //                });
                       
    //            }
    //        }
    //        return CardSet;
    //    }

    //    //private static byte[] GetBitVector(Bitmap cropedImage)
    //    //{
    //    //    if (cropedImage.PixelFormat.Equals(PixelFormat.Format32bppArgb))
    //    //    {
    //    //        var argbData = new byte[cropedImage.Width * cropedImage.Height * 4];
    //    //        BitmapData bmpData = cropedImage.LockBits(new Rectangle(0, 0, cropedImage.Width, cropedImage.Height), ImageLockMode.ReadOnly, cropedImage.PixelFormat);
    //    //        System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, argbData, 0, cropedImage.Width * cropedImage.Height * 4);
    //    //        cropedImage.UnlockBits(bmpData);

    //    //        List<byte> vector = new List<byte>();

    //    //        //PixelFormat == Format32bppArgb
    //    //        for (int i = 0; i < argbData.Length; i = i + 4)
    //    //        {
    //    //            if (argbData[i] + argbData[i + 1] + argbData[i + 2] > 200)
    //    //                vector.Add(0);
    //    //            else
    //    //                vector.Add(1);
    //    //        }

    //    //        return vector.ToArray();
    //    //    }
    //    //    return null;
    //    //}

    //    public static bool IsVectorAlreadyExists(List<BitVector> Cards, byte[] newVector)
    //    {
    //        if (Cards != null && newVector != null)
    //        {
    //            foreach (BitVector c in Cards)
    //            {
    //                bool isExists = true;
    //                for (int i = 0; i < c.ByteArray.Length; i++)
    //                {
    //                    if (c.ByteArray[i] != newVector[i])
    //                    {
    //                        isExists = false;
    //                        break;
    //                    }
    //                }
    //                if (isExists)
    //                    return isExists;
    //            }
    //        }
    //        if (Cards.Count == 0)
    //            return false;

    //        return false;
    //    }


    //}
}
