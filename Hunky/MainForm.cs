using Config;
using Learning;
using Recognizer;
using RoutineTasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hunky
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_GetIn_Click(object sender, EventArgs e)
        {
            var proxyfier = Process.GetProcessesByName(ConfigResolver.GetSetting("PROXY"));
            if (proxyfier.Length == 0)
            {
                MessageBox.Show("Proxyfier not started, You cannot proceed");
                Console.WriteLine("Proxyfier not started, You cannot proceed");
                return;
            }

            GameActions.Start();
            PlayerActions.GetInWithAnyPlayer();
        }

        private void btn_Learn_Click(object sender, EventArgs e)
        {
            LearnSetCreator lsc = new LearnSetCreator();
            lsc.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(10, 800);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Classifier mf = new Classifier();
            mf.ShowDialog();
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            CardsRecognition cr = new CardsRecognition();
            cr.ShowDialog();
        }
    }
}
