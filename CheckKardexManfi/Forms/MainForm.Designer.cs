
namespace AnbarHelp.Forms
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
            this.KardexManfiFrmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KardexManfiFrmBtn
            // 
            this.KardexManfiFrmBtn.Location = new System.Drawing.Point(32, 27);
            this.KardexManfiFrmBtn.Name = "KardexManfiFrmBtn";
            this.KardexManfiFrmBtn.Size = new System.Drawing.Size(132, 36);
            this.KardexManfiFrmBtn.TabIndex = 0;
            this.KardexManfiFrmBtn.Text = "فرم کاردکس منفی";
            this.KardexManfiFrmBtn.UseVisualStyleBackColor = true;
            this.KardexManfiFrmBtn.Click += new System.EventHandler(this.KardexManfiFrmBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 450);
            this.Controls.Add(this.KardexManfiFrmBtn);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button KardexManfiFrmBtn;
    }
}