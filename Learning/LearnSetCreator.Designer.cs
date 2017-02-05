namespace Learning
{
    partial class LearnSetCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pb_screenshot = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btn_screen = new System.Windows.Forms.Button();
            this.grid_Vectors = new System.Windows.Forms.DataGridView();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb_screenshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Vectors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_screenshot
            // 
            this.pb_screenshot.Location = new System.Drawing.Point(12, 4);
            this.pb_screenshot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_screenshot.Name = "pb_screenshot";
            this.pb_screenshot.Size = new System.Drawing.Size(393, 347);
            this.pb_screenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_screenshot.TabIndex = 9;
            this.pb_screenshot.TabStop = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btn_screen
            // 
            this.btn_screen.Location = new System.Drawing.Point(12, 356);
            this.btn_screen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_screen.Name = "btn_screen";
            this.btn_screen.Size = new System.Drawing.Size(92, 38);
            this.btn_screen.TabIndex = 11;
            this.btn_screen.Text = "Gather";
            this.btn_screen.UseVisualStyleBackColor = true;
            this.btn_screen.Click += new System.EventHandler(this.btn_screen_Click);
            // 
            // grid_Vectors
            // 
            this.grid_Vectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Vectors.Location = new System.Drawing.Point(412, 4);
            this.grid_Vectors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grid_Vectors.Name = "grid_Vectors";
            this.grid_Vectors.Size = new System.Drawing.Size(591, 610);
            this.grid_Vectors.TabIndex = 13;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(903, 622);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(100, 28);
            this.btn_Save.TabIndex = 14;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // LearnSetCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 665);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.grid_Vectors);
            this.Controls.Add(this.btn_screen);
            this.Controls.Add(this.pb_screenshot);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LearnSetCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LearnSetCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_screenshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Vectors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource cardBindingSource;
        private System.Windows.Forms.PictureBox pb_screenshot;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btn_screen;
        private System.Windows.Forms.DataGridView grid_Vectors;
        private System.Windows.Forms.Button btn_Save;
    }
}

