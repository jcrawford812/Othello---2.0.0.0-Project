namespace Othello
{
    partial class Board
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Board));
            this.btnPvp = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAIEasy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPvp
            // 
            this.btnPvp.Location = new System.Drawing.Point(107, 12);
            this.btnPvp.Name = "btnPvp";
            this.btnPvp.Size = new System.Drawing.Size(197, 23);
            this.btnPvp.TabIndex = 0;
            this.btnPvp.Text = "Start Player Vs Player Game";
            this.btnPvp.UseVisualStyleBackColor = true;
            this.btnPvp.Click += new System.EventHandler(this.btnPvp_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(481, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(183, 138);
            this.textBox1.TabIndex = 1;
            // 
            // btnAIEasy
            // 
            this.btnAIEasy.Location = new System.Drawing.Point(354, 11);
            this.btnAIEasy.Name = "btnAIEasy";
            this.btnAIEasy.Size = new System.Drawing.Size(212, 23);
            this.btnAIEasy.TabIndex = 2;
            this.btnAIEasy.Text = "Player vs Computer: Easy";
            this.btnAIEasy.UseVisualStyleBackColor = true;
            this.btnAIEasy.Click += new System.EventHandler(this.btnEasyAi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 520);
            this.Controls.Add(this.btnAIEasy);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnPvp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Othello";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnPvp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAIEasy;
    }
}

