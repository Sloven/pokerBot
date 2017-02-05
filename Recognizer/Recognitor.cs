using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Neuro;
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recognizer
{
    public class Recognitor
    {
        FiltersSequence filtersSequence;
        private Process process;
        private User32.Rect rect;
        private int width;
        private int height;
        EFCRUD efcrud;

        public Recognitor(string processName, EFCRUD _efcrud)
        {
                process = Process.GetProcessesByName(processName)[0];
                User32.GetWindowRect(process.MainWindowHandle, ref rect);
                width = rect.right - rect.left;
                height = rect.bottom - rect.top;
                EFCRUD efcrud = _efcrud;
                filtersSequence = InitFilters();
        }

        public List<Blob> DefineBlobs(Bitmap image, int _MinHeight = 1, int _MinWidth = 1, int _MaxHeight = 2147483647, int _MaxWidth = 2147483647, bool _FilterBlobs = false)
        {
            List<Blob> blobs = new List<Blob>();

            BlobCounter blobCounter = new BlobCounter()
            {
                MinHeight = _MinHeight,
                MinWidth = _MinWidth,
                MaxWidth = _MaxWidth,
                MaxHeight = _MaxHeight,
                FilterBlobs = _FilterBlobs
            };

            image = AForge.Imaging.Image.Clone(image, PixelFormat.Format24bppRgb);
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            blobCounter.ProcessImage(image);
            blobs = blobCounter.GetObjectsInformation().ToList();

            return blobs;
        }

        public Bitmap GrabSnapshot()
        {
            User32.GetWindowRect(process.MainWindowHandle, ref rect);
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return bmp;
        }

        private FiltersSequence InitFilters()
        {
            FiltersSequence seq = new FiltersSequence();

            ColorFiltering filter = new ColorFiltering();
            // set color ranges to keep
            filter.Red = new IntRange(220, 255);
            filter.Green = new IntRange(220, 255);
            filter.Blue = new IntRange(220, 255);

            seq.Add(filter);

            //seq.Add(Grayscale.CommonAlgorithms.BT709);
            //seq.Add(new BrightnessCorrection(-120));
            //seq.Add(new ContrastCorrection(80));
            //seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter

            return seq;
        }

        public Bitmap ApplyFilters(Bitmap origSnap)
        {
            Bitmap recipient = origSnap.Clone() as Bitmap;
            recipient = filtersSequence.Apply(origSnap); // Apply filters on source image
            return recipient;
        }

        internal void RecognizeCards(List<Entity.BitVector> cards, string netName)
        {
            Network net = efcrud.RestoreNetwork(netName);
            double[] netResult;
            Rank rcgnRank = Rank.NOT_RECOGNIZED;
            Suit rcgnSuit = Suit.NOT_RECOGNIZED;

            foreach (var card in cards)
            {
                netResult = net.Compute(card.GetBiPolarVector());
                int maxIndex = 0;
                for (int i = 0; i < netResult.Length; i++)
                {
                    if (netResult[i] > netResult[maxIndex])
                        maxIndex = i;
                }
                SetRankSuitByClassificator(out rcgnRank,out rcgnSuit, maxIndex);
                card.Rank = rcgnRank;
                card.Suit = rcgnSuit;
            }
        }

        public void SetRankSuitByClassificator(out Rank rank, out Suit suit, int classId)
        {
            RankSuitOutputRelation rso = efcrud.getRSObyO(classId);
            if (rso != null)
            {
                rank = rso.Rank;
                suit = rso.Suit;
            }
            else
            {
                rank = Rank.NOT_RECOGNIZED;
                suit = Suit.NOT_RECOGNIZED;
            }

        }
    }
}
