using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;
using AForge.Controls;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Entity;

namespace Learning
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Classifier : System.Windows.Forms.Form
    {

        #region FormRoutine
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView dataList;
		private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox learningRateBox;
        private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView resultList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox iterationsBox;
        private System.Windows.Forms.Button stopButton;
        private TextBox tb_Error;
        private Label label2;
        private Button btn_try;
        private Button btn_Save;
        private Label lbl_saveResult;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		

		// Constructor
		public Classifier( )
		{
			InitializeComponent( );
			UpdateSettings( );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.dataList = new System.Windows.Forms.ListView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_saveResult = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_try = new System.Windows.Forms.Button();
            this.tb_Error = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.iterationsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.resultList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.learningRateBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadButton);
            this.groupBox1.Controls.Add(this.dataList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 484);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 450);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(90, 27);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "&Load";
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // dataList
            // 
            this.dataList.FullRowSelect = true;
            this.dataList.GridLines = true;
            this.dataList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dataList.Location = new System.Drawing.Point(12, 23);
            this.dataList.Name = "dataList";
            this.dataList.Size = new System.Drawing.Size(204, 421);
            this.dataList.TabIndex = 0;
            this.dataList.UseCompatibleStateImageBehavior = false;
            this.dataList.View = System.Windows.Forms.View.Details;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV (Comma delimited) (*.csv)|*.csv";
            this.openFileDialog.Title = "Select data file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_saveResult);
            this.groupBox2.Controls.Add(this.btn_Save);
            this.groupBox2.Controls.Add(this.btn_try);
            this.groupBox2.Controls.Add(this.tb_Error);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.stopButton);
            this.groupBox2.Controls.Add(this.iterationsBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.resultList);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.startButton);
            this.groupBox2.Controls.Add(this.learningRateBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(252, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 484);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training";
            // 
            // lbl_saveResult
            // 
            this.lbl_saveResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_saveResult.Location = new System.Drawing.Point(79, 136);
            this.lbl_saveResult.Name = "lbl_saveResult";
            this.lbl_saveResult.Size = new System.Drawing.Size(204, 26);
            this.lbl_saveResult.TabIndex = 15;
            this.lbl_saveResult.Text = "Network and output was saved!";
            this.lbl_saveResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_saveResult.Visible = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(289, 136);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(90, 26);
            this.btn_Save.TabIndex = 14;
            this.btn_Save.Text = "Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_try
            // 
            this.btn_try.Enabled = false;
            this.btn_try.Location = new System.Drawing.Point(289, 94);
            this.btn_try.Name = "btn_try";
            this.btn_try.Size = new System.Drawing.Size(90, 26);
            this.btn_try.TabIndex = 13;
            this.btn_try.Text = "Tr&y";
            this.btn_try.Click += new System.EventHandler(this.btn_try_Click);
            // 
            // tb_Error
            // 
            this.tb_Error.Location = new System.Drawing.Point(121, 96);
            this.tb_Error.Name = "tb_Error";
            this.tb_Error.ReadOnly = true;
            this.tb_Error.Size = new System.Drawing.Size(158, 22);
            this.tb_Error.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(66, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "Error:";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(289, 54);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(90, 26);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "S&top";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // iterationsBox
            // 
            this.iterationsBox.Location = new System.Drawing.Point(121, 58);
            this.iterationsBox.Name = "iterationsBox";
            this.iterationsBox.ReadOnly = true;
            this.iterationsBox.Size = new System.Drawing.Size(158, 22);
            this.iterationsBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(37, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Iterations:";
            // 
            // resultList
            // 
            this.resultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.resultList.FullRowSelect = true;
            this.resultList.GridLines = true;
            this.resultList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.resultList.Location = new System.Drawing.Point(15, 200);
            this.resultList.Name = "resultList";
            this.resultList.Size = new System.Drawing.Size(364, 277);
            this.resultList.TabIndex = 5;
            this.resultList.UseCompatibleStateImageBehavior = false;
            this.resultList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "CardId";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Classification result";
            this.columnHeader2.Width = 100;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Perceptron classification results:";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(289, 19);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(90, 26);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "&Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // learningRateBox
            // 
            this.learningRateBox.Location = new System.Drawing.Point(121, 23);
            this.learningRateBox.Name = "learningRateBox";
            this.learningRateBox.Size = new System.Drawing.Size(158, 22);
            this.learningRateBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Learning rate:";
            // 
            // Classifier
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(643, 502);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Classifier";
            this.Text = "Perceptron Classifier";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        // Delegates to enable async calls for setting controls properties
        private delegate void SetTextCallback( System.Windows.Forms.Control control, string text );
        private delegate void ClearListCallback( System.Windows.Forms.ListView control );
        private delegate ListViewItem AddListItemCallback( System.Windows.Forms.ListView control, string itemText );
        private delegate void AddListSubitemCallback( ListViewItem item, string subItemText );

        // Thread safe updating of control's text property
        private void SetText( System.Windows.Forms.Control control, string text )
        {
            if ( control.InvokeRequired )
            {
                SetTextCallback d = new SetTextCallback( SetText );
                Invoke( d, new object[] { control, text } );
            }
            else
            {
                control.Text = text;
            }
        }

        // Thread safe clearing of list view
        private void ClearList( System.Windows.Forms.ListView control )
        {
            if ( control.InvokeRequired )
            {
                ClearListCallback d = new ClearListCallback( ClearList );
                Invoke( d, new object[] { control } );
            }
            else
            {
                control.Items.Clear( );
            }
        }

        // Thread safe adding of item to list control
        private ListViewItem AddListItem( System.Windows.Forms.ListView control, string itemText )
        {
            ListViewItem item = null;

            if ( control.InvokeRequired )
            {
                AddListItemCallback d = new AddListItemCallback( AddListItem );
                item = (ListViewItem) Invoke( d, new object[] { control, itemText } );
            }
            else
            {
                item = control.Items.Add( itemText );
            }

            return item;
        }

        // Thread safe adding of subitem to list control
        private void AddListSubitem( ListViewItem item, string subItemText )
        {
            if ( this.InvokeRequired )
            {
                AddListSubitemCallback d = new AddListSubitemCallback( AddListSubitem );
                Invoke( d, new object[] { item, subItemText } );
            }
            else
            {
                item.SubItems.Add( subItemText );
            }
        }

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// check if worker thread is running
			if ( ( workerThread != null ) && ( workerThread.IsAlive ) )
			{
				needToStop = true;
                while ( !workerThread.Join( 100 ) )
                    Application.DoEvents( );
            }
		}

        #endregion

        private int samples = 0;
        private int variables = 0;
        private double[][] input = null;
        private double[][] output = null;

        private double learningRate = 0.0509f;
        private ActivationNetwork currentNet;


        private Thread workerThread = null;
        private volatile bool needToStop = false;

        EFCRUD crud = new EFCRUD();

		// On "Load" button click - load data
		private void loadButton_Click( object sender, System.EventArgs e )
		{
            List<BitVector> bvList = crud.GetStoredCards();
            int varCount = bvList[0].ByteArray.Length;
            int CartCount = 52;
            samples = CartCount;

            input = new double[bvList.Count][];
            output = new double[samples][];

            for (int i = 0; i < bvList.Count; i++)
            {
                input[i] = new double[varCount];
                bvList[i].GetBiPolarVector().CopyTo(input[i],0);

                output[i] = new double[samples];
                for (int j = 0; j < samples; j++)
                {
                    if (i == j)
                        output[i][j] = 0.5;
                    else
                        output[i][j] = -0.5;
                }
            }

            variables = bvList[0].ByteArray.Length;
				UpdateDataListView( );
				startButton.Enabled = true;
		}
 

		// Update settings controls
		private void UpdateSettings( )
		{
			learningRateBox.Text = learningRate.ToString( );
		}

		// Update data in list view
		private void UpdateDataListView( )
		{
			// remove all curent data and columns
			dataList.Items.Clear( );
			dataList.Columns.Clear( );

			// add columns
			for ( int i = 0, n = variables; i < n; i++ )
			{
				dataList.Columns.Add( string.Format( "X{0}", i + 1 ),
					52, HorizontalAlignment.Left );
			}
			dataList.Columns.Add( "Class", 52, HorizontalAlignment.Left );

			// add items
			for ( int i = 0; i < samples; i++ )
			{
				dataList.Items.Add(input[i][0].ToString( ) );

				for ( int j = 1; j < variables; j++ )
				{
					dataList.Items[i].SubItems.Add( input[i][j].ToString( ) );
                }

                dataList.Items[i].SubItems.Add(i.ToString());
			}
		}

        // Delegates to enable async calls for setting controls properties
        private delegate void EnableCallback( bool enable );

        // Enable/disale controls (safe for threading)
        private void EnableControls( bool enable )
		{
            if ( InvokeRequired )
            {
                EnableCallback d = new EnableCallback( EnableControls );
                Invoke( d, new object[] { enable } );
            }
            else
            {
			    learningRateBox.Enabled	= enable;
			    loadButton.Enabled		= enable;
			    startButton.Enabled		= enable;
			    stopButton.Enabled		= !enable;
                btn_try.Enabled = !enable;
                if (currentNet != null)
                    btn_try.Enabled = true;

                btn_Save.Enabled = currentNet != null;
                lbl_saveResult.Visible = false;
		    }
        }

		private void startButton_Click(object sender, System.EventArgs e)
		{
			// get learning rate
			try
			{
				learningRate = Math.Max( 0.00001, Math.Min( 1, double.Parse( learningRateBox.Text ) ) );
			}
			catch
			{
				learningRate = 0.1;
			}
			
            // update settings controls
			UpdateSettings( );

			// disable all settings controls
			EnableControls( false );

			// run worker thread
			needToStop = false;
            workerThread = new Thread(new ThreadStart(Learning));
			workerThread.Start( );
		}

		// On button "Stop" - stop learning procedure
		private void stopButton_Click(object sender, System.EventArgs e)
		{
			// stop worker thread
			needToStop = true;

            while ( !workerThread.Join( 100 ) )
                Application.DoEvents( );
            workerThread = null;
		}

        private void testNet(Network net, double[] input)
        {
            var res = net.Compute(input);
        }

        // Worker thread
        private void Learning()
        {
            // create perceptron
            //ActivationNetwork network = new ActivationNetwork(new BipolarSigmoidFunction(0.09f), variables, samples);
            currentNet = new ActivationNetwork(new BipolarSigmoidFunction(0.09f), variables, samples);
            currentNet.Randomize();

            // create network teacher
            MyBackPropagationLearning teacher = new MyBackPropagationLearning(currentNet);
            //teacher.LearningRate = 0.999f;
            teacher.LearningRate = 0.0509f;
            teacher.LearningRate = learningRate;
            learningRateBox.Text = learningRate.ToString();
            
            int iteration = 1;

            try
            {
                while (!needToStop)
                {
                    // run epoch of learning procedure
                    double error = teacher.RunEpoch(input, output);

                    // show current iteration
                    SetText(iterationsBox, iteration.ToString());

                    if((iteration % 10) == 0)
                        SetText(tb_Error, error.ToString());

                    // stop if no error
                    if (error < 0.05)
                        return;
                
                    iteration++;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Failed writing file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            // enable settings controls
            EnableControls(true);

            return;
        }

        private void btn_try_Click(object sender, EventArgs e)
        {
            if (currentNet != null)
            {
                resultList.Items.Clear();
                double[] result;
                ListViewItem item = null;
                foreach (var card in crud.GetStoredCards())
                {
                    result = currentNet.Compute(card.GetBiPolarVector());
                    item = AddListItem(resultList, string.Format("Card {0}", card.id));

                    int maxIndex = 0;
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] > result[maxIndex])
                            maxIndex = i;
                    }

                    AddListSubitem(item, maxIndex.ToString());
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (currentNet != null)
            {
                double[] result;
                List<RankSuitOutputRelation> rsoList = new List<RankSuitOutputRelation>();

                foreach (var card in crud.GetStoredCards())
                {
                    result = currentNet.Compute(card.GetBiPolarVector());
                    int maxIndex = 0;
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] > result[maxIndex])
                            maxIndex = i;
                    }

                    rsoList.Add(new RankSuitOutputRelation() { 
                        Rank = card.Rank,
                        Suit = card.Suit,
                        NetworkClass = maxIndex
                    });
                }
                crud.OverwriteCardOutputRealation(rsoList);
                crud.SaveNetwork(currentNet, "default");

                lbl_saveResult.Visible = true;
            }
        }
	}
}
