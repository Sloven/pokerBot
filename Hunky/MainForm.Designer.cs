namespace Hunky
{
    partial class MainForm
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
            this.btn_GetIn = new System.Windows.Forms.Button();
            this.btn_Learn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_GetIn
            // 
            this.btn_GetIn.Location = new System.Drawing.Point(16, 15);
            this.btn_GetIn.Margin = new System.Windows.Forms.Padding(4);
            this.btn_GetIn.Name = "btn_GetIn";
            this.btn_GetIn.Size = new System.Drawing.Size(100, 28);
            this.btn_GetIn.TabIndex = 0;
            this.btn_GetIn.Text = "Get In";
            this.btn_GetIn.UseVisualStyleBackColor = true;
            this.btn_GetIn.Click += new System.EventHandler(this.btn_GetIn_Click);
            // 
            // btn_Learn
            // 
            this.btn_Learn.Location = new System.Drawing.Point(263, 15);
            this.btn_Learn.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Learn.Name = "btn_Learn";
            this.btn_Learn.Size = new System.Drawing.Size(100, 28);
            this.btn_Learn.TabIndex = 1;
            this.btn_Learn.Text = "Learning";
            this.btn_Learn.UseVisualStyleBackColor = true;
            this.btn_Learn.Click += new System.EventHandler(this.btn_Learn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 51);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Classifier";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Play
            // 
            this.btn_Play.Location = new System.Drawing.Point(16, 51);
            this.btn_Play.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(100, 28);
            this.btn_Play.TabIndex = 3;
            this.btn_Play.Text = "Play";
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 126);
            this.Controls.Add(this.btn_Play);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Learn);
            this.Controls.Add(this.btn_GetIn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_GetIn;
        private System.Windows.Forms.Button btn_Learn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Play;
    }
}