namespace NisROM_Tuning_Suite
{
    partial class KMultiplierForm
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
            this.kMultiplierView1 = new NisROM_Tuning_Suite.Controls.KMultiplierView();
            this.SuspendLayout();
            // 
            // kMultiplierView1
            // 
            this.kMultiplierView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kMultiplierView1.Location = new System.Drawing.Point(0, 0);
            this.kMultiplierView1.Name = "kMultiplierView1";
            this.kMultiplierView1.Size = new System.Drawing.Size(286, 107);
            this.kMultiplierView1.TabIndex = 0;
            // 
            // KMultiplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 107);
            this.Controls.Add(this.kMultiplierView1);
            this.Name = "KMultiplierForm";
            this.Text = "K-Value";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KMultiplierForm_FormClosing);
            this.Load += new System.EventHandler(this.KMultiplierForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.KMultiplierView kMultiplierView1;
    }
}