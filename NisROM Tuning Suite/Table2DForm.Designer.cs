namespace NisROM_Tuning_Suite
{
    partial class Table2DForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.staticYAxisView1 = new NisROM_Tuning_Suite.Controls.StaticYAxisView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(363, 549);
            this.dataGridView1.TabIndex = 0;
            // 
            // staticYAxisView1
            // 
            this.staticYAxisView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staticYAxisView1.Location = new System.Drawing.Point(0, 0);
            this.staticYAxisView1.Name = "staticYAxisView1";
            this.staticYAxisView1.Size = new System.Drawing.Size(363, 549);
            this.staticYAxisView1.TabIndex = 1;
            // 
            // Table2DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(363, 549);
            this.Controls.Add(this.staticYAxisView1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Table2DForm";
            this.Text = "Table2DForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Table2DForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Controls.StaticYAxisView staticYAxisView1;
    }
}