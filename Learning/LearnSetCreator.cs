using AForge.Imaging;
using Config;
using DataAccess;
using Entity;
using Recognizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Learning
{
    public partial class LearnSetCreator : Form
    {
        List<BitVector> Cards = new List<BitVector>();
        Bitmap filteredImage;
        Bitmap originalImage;
        Recognitor rcgntr;
        int currentIndex = 0;
        Blob blobs;
        EFCRUD efcrud = new EFCRUD();
        int VectorLength;
        
         

        public LearnSetCreator()
        {
            InitializeComponent();
            timer.Stop();

            grid_Vectors.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id" });
            grid_Vectors.Columns.Add(new DataGridViewImageColumn() { Name = "Vector"});
            grid_Vectors.Columns.Add(new DataGridViewComboBoxColumn() { Name = "Rank", ValueType = typeof(Rank),  DataSource = Enum.GetValues(typeof(Rank)) });
            grid_Vectors.Columns.Add(new DataGridViewComboBoxColumn() { Name = "Suit", ValueType = typeof(Suit), DataSource = Enum.GetValues(typeof(Suit)) });
            grid_Vectors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            grid_Vectors.EditMode = DataGridViewEditMode.EditOnEnter;

            VectorLength = Int32.Parse(ConfigResolver.GetSetting("VectorLength_Default"));

            Cards = efcrud.GetStoredCards();
            for(int i =0;i<Cards.Count;i++)
            {
                System.Drawing.Image CropedBmp = BitVector.GetImage(Cards[i].CropedImg);

                grid_Vectors.Rows.Add(i,
                    drawMatrix(CropedBmp.Width, CropedBmp.Height,Cards[i].ByteArray),
                    Rank.NOT_RECOGNIZED, Suit.NOT_RECOGNIZED);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            originalImage = rcgntr.GrabSnapshot();
            pb_screenshot.Image = originalImage;

            if (originalImage != null)
            {
                Bitmap filteredBmp = rcgntr.ApplyFilters(originalImage);
                filteredImage = filteredBmp;
                pb_screenshot.Image = filteredImage;

                var blbs = rcgntr.DefineBlobs(filteredImage, _MinHeight: 20, _MinWidth: 30, _MaxHeight:70, _MaxWidth:70, _FilterBlobs: true);
                var newCards = BlobsHelpers.ExtractBlobs(filteredImage, blbs);
                UpdateCardSet(newCards);
            }
            if (Cards.Count >= 52)
            {
                timer.Stop();
                MessageBox.Show("All cards collected");
            }
        }


        private void UpdateCardSet(List<BitVector> NewCards)
        {
            for (int i = 0; i < NewCards.Count; i++)
            {
                var nbarr = NewCards[i].ByteArray;

                if (nbarr.Length != VectorLength) //skip it
                {
                    byte[] temp = nbarr;
                    nbarr = new byte[VectorLength];
                    NewCards[i].ByteArray = new byte[VectorLength];
                    
                    temp.CopyTo(nbarr, 0);
                    temp.CopyTo(NewCards[i].ByteArray, 0);
                }

                if (!BlobsHelpers.IsVectorAlreadyExists(Cards, nbarr))
                {
                    Cards.Add(NewCards[i]);
                    System.Drawing.Image CropedBmp = BitVector.GetImage(NewCards[i].CropedImg);

                    grid_Vectors.Rows.Add(Cards.Count-1,
                        drawMatrix(CropedBmp.Width, CropedBmp.Height, nbarr),
                        Rank.NOT_RECOGNIZED,Suit.NOT_RECOGNIZED);
                }
            }
        }

        private void btn_screen_Click(object sender, EventArgs e)
        {
            timer.Stop();

            try
            {
                rcgntr = new Recognitor("PokerStars", efcrud);
                timer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("PokerStats not started");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            for(int i = 0 ; i < Cards.Count; i++)
            {
                result += string.Concat(string.Join(",", Cards[i].ByteArray), ",", i, ";");
            }
        }

        private System.Drawing.Image drawMatrix(int width, int heigth, byte[] bitVector)
        {
            Bitmap bmp = new Bitmap(width, heigth);

            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    int index = (i * width) + j;
                    bmp.SetPixel(j, i, bitVector[index] > 0 ? Color.Black : Color.White);
                }
            }

            return bmp;
        }

        private void LearnSetCreator_Load(object sender, EventArgs e)
        {
            this.Location = new Point(10, 700);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            BitVector bv;
            Rank trank;
            Suit tsuit;
            int cardId = 0;
            List<BitVector> vectorLst = new List<BitVector>();
            foreach (DataGridViewRow row in grid_Vectors.Rows)
            {
                bv = new BitVector();

                if (row.Cells[0].Value == null)
                    break;

                cardId = Int32.Parse(row.Cells[0].Value.ToString());

                if (Enum.TryParse(row.Cells[2].Value.ToString(), out trank))
                    bv.Rank = trank;
                else 
                    throw new Exception(string.Format("Not correct rank at {0}", cardId));
                if (Enum.TryParse(row.Cells[3].Value.ToString(), out tsuit))
                    bv.Suit = tsuit;
                else
                    throw new Exception(string.Format("Not correct rank at {0}", cardId));

                bv.ByteArray = Cards[cardId].ByteArray;
                bv.CropedImg = Cards[cardId].CropedImg;

                vectorLst.Add(bv);
            }

            efcrud.OverwriteBitVectors(vectorLst);

        }

       
    }
}