
namespace AnbarHelp.Forms
{
    partial class DiffMaliAndAndAnbarFrm
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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LblServerSelect = new System.Windows.Forms.Label();
            this.ServerComBox = new System.Windows.Forms.ComboBox();
            this.ServerSelected = new System.Windows.Forms.Button();
            this.FromDateTxt = new System.Windows.Forms.TextBox();
            this.ToDateTxt = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GVDiffAnbarAndMali = new System.Windows.Forms.DataGridView();
            this.GVDiffMaliAndAnbar = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDiffAnbarAndMali)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDiffMaliAndAnbar)).BeginInit();

            this.SuspendLayout();
            // 
            // LblServerSelect
            // 
            this.LblServerSelect.AutoSize = true;
            this.LblServerSelect.Location = new System.Drawing.Point(786, 166);
            this.LblServerSelect.Name = "LblServerSelect";
            this.LblServerSelect.Size = new System.Drawing.Size(54, 15);
            this.LblServerSelect.TabIndex = 0;
            this.LblServerSelect.Text = ": نام سرور";
            // 
            // ServerComBox
            // 
            this.ServerComBox.FormattingEnabled = true;
            this.ServerComBox.Location = new System.Drawing.Point(540, 193);
            this.ServerComBox.Name = "ServerComBox";
            this.ServerComBox.Size = new System.Drawing.Size(526, 23);
            this.ServerComBox.TabIndex = 1;
            // 
            // ServerSelected
            // 
            this.ServerSelected.Location = new System.Drawing.Point(786, 378);
            this.ServerSelected.Name = "ServerSelected";
            this.ServerSelected.Size = new System.Drawing.Size(75, 23);
            this.ServerSelected.TabIndex = 2;
            this.ServerSelected.Text = "انتخاب";
            this.ServerSelected.UseVisualStyleBackColor = true;
            this.ServerSelected.Click += new System.EventHandler(this.ServerSelected_Click);
            // 
            // FromDateTxt
            // 
            this.FromDateTxt.Location = new System.Drawing.Point(710, 239);
            this.FromDateTxt.Name = "FromDateTxt";
            this.FromDateTxt.Size = new System.Drawing.Size(214, 23);
            this.FromDateTxt.TabIndex = 3;
            // 
            // ToDateTxt
            // 
            this.ToDateTxt.Location = new System.Drawing.Point(710, 278);
            this.ToDateTxt.Name = "ToDateTxt";
            this.ToDateTxt.Size = new System.Drawing.Size(214, 23);
            this.ToDateTxt.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1560, 200);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MaximumSize = new System.Drawing.Size(1560,200);
            // 
            // GVDiffAnbarAndMali
            // 
            this.GVDiffAnbarAndMali.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.GVDiffAnbarAndMali.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVDiffAnbarAndMali.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.GVDiffAnbarAndMali.Location = new System.Drawing.Point(12, 220);
            this.GVDiffAnbarAndMali.Name = "GVDiffAnbarAndMali";
            this.GVDiffAnbarAndMali.RowTemplate.Height = 25;
            this.GVDiffAnbarAndMali.Size = new System.Drawing.Size(775, 250);
            this.GVDiffAnbarAndMali.TabIndex = 0;
            this.GVDiffAnbarAndMali.MaximumSize = new System.Drawing.Size(775, 250);
            // 
            // GVDiffMaliAndAnbar
            // 
            this.GVDiffMaliAndAnbar.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.GVDiffMaliAndAnbar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVDiffMaliAndAnbar.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.GVDiffMaliAndAnbar.Location = new System.Drawing.Point(794, 220);
            this.GVDiffMaliAndAnbar.Name = "GVDiffMaliAndAnbar";
            this.GVDiffMaliAndAnbar.RowTemplate.Height = 25;
            this.GVDiffMaliAndAnbar.Size = new System.Drawing.Size(775, 250);
            this.GVDiffMaliAndAnbar.TabIndex = 0;
            this.GVDiffMaliAndAnbar.MaximumSize = new System.Drawing.Size(775, 250);
            // 
            // DiffMaliAndAndAnbarFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 800);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GVDiffAnbarAndMali);
            this.Controls.Add(this.GVDiffMaliAndAnbar);

            this.Controls.Add(this.ToDateTxt);
            this.Controls.Add(this.FromDateTxt);
            this.Controls.Add(this.ServerSelected);
            this.Controls.Add(this.ServerComBox);
            this.Controls.Add(this.LblServerSelect);
            this.Name = "DiffMaliAndAndAnbarFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DiffMaliAndAndAnbarFrm";
            this.Load += new System.EventHandler(this.DiffMaliAndAndAnbarFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDiffAnbarAndMali)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDiffMaliAndAnbar)).EndInit();


            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblServerSelect;
        private System.Windows.Forms.ComboBox ServerComBox;
        private System.Windows.Forms.Button ServerSelected;
        private System.Windows.Forms.TextBox FromDateTxt;
        private System.Windows.Forms.TextBox ToDateTxt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView GVDiffAnbarAndMali;
        private System.Windows.Forms.DataGridView GVDiffMaliAndAnbar;

    }
}