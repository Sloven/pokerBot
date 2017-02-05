using AForge.Imaging;
using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recognizer
{
    public partial class CardsRecognition : Form
    {
        private delegate ListViewItem AddListItemCallback(System.Windows.Forms.ListView control, string itemText);
        private delegate void AddListSubitemCallback(ListViewItem item, string subItemText);

        List<BitVector> Cards = new List<BitVector>();
        Bitmap filteredImage;
        Bitmap originalImage;
        Recognitor rcgntr;
        EFCRUD efcrud = new EFCRUD();

        public CardsRecognition()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            originalImage = rcgntr.GrabSnapshot();
            if (originalImage != null)
            {
                Bitmap filteredBmp = rcgntr.ApplyFilters(originalImage);
                filteredImage = filteredBmp;
                var blbs = rcgntr.DefineBlobs(filteredImage, _MinHeight: 20, _MinWidth: 30, _MaxHeight: 70, _MaxWidth: 70, _FilterBlobs: true);
                var newCards = BlobsHelpers.ExtractBlobs(filteredImage, blbs);
                rcgntr.RecognizeCards(newCards, "default");

                cardsList.Items.Clear();
                ListViewItem item = null;
                foreach (var card in newCards)
                {
                    item = AddListItem(cardsList, card.Owner.ToString());
                    AddListSubitem(item, string.Concat(card.Rank.ToString(), card.Suit.ToString()));
                }
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
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

        private ListViewItem AddListItem(System.Windows.Forms.ListView control, string itemText)
        {
            ListViewItem item = null;

            if (control.InvokeRequired)
            {
                AddListItemCallback d = new AddListItemCallback(AddListItem);
                item = (ListViewItem)Invoke(d, new object[] { control, itemText });
            }
            else
            {
                item = control.Items.Add(itemText);
            }

            return item;
        }

        // Thread safe adding of subitem to list control
        private void AddListSubitem(ListViewItem item, string subItemText)
        {
            if (this.InvokeRequired)
            {
                AddListSubitemCallback d = new AddListSubitemCallback(AddListSubitem);
                Invoke(d, new object[] { item, subItemText });
            }
            else
            {
                item.SubItems.Add(subItemText);
            }
        }
    }
}
