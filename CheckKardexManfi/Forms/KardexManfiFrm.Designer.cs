
namespace CheckKardexManfi
{
    partial class KardexManfiFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SanadSTxtBox = new System.Windows.Forms.TextBox();
            this.SanadSN = new System.Windows.Forms.Label();
            this.SendSanads = new System.Windows.Forms.Button();
            this.backToMain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SanadSTxtBox
            // 
            this.SanadSTxtBox.Location = new System.Drawing.Point(2, 92);
            this.SanadSTxtBox.Multiline = true;
            this.SanadSTxtBox.Name = "SanadSTxtBox";
            this.SanadSTxtBox.Size = new System.Drawing.Size(382, 50);
            this.SanadSTxtBox.TabIndex = 0;
            this.SanadSTxtBox.Text = "1497688.441,";
            // 
            // SanadSN
            // 
            this.SanadSN.Location = new System.Drawing.Point(139, 74);
            this.SanadSN.Name = "SanadSN";
            this.SanadSN.Size = new System.Drawing.Size(200, 15);
            this.SanadSN.TabIndex = 1;
            this.SanadSN.Text = "سند هارا با , جدا کنید ";
            // 
            // SendSanads
            // 
            this.SendSanads.Location = new System.Drawing.Point(129, 160);
            this.SendSanads.Name = "SendSanads";
            this.SendSanads.Size = new System.Drawing.Size(124, 23);
            this.SendSanads.TabIndex = 2;
            this.SendSanads.Text = "ارسال اسناد";
            this.SendSanads.UseVisualStyleBackColor = true;
            this.SendSanads.Click += new System.EventHandler(this.SendSanads_Click);
            // 
            // backToMain
            // 
            this.backToMain.Location = new System.Drawing.Point(5, 4);
            this.backToMain.Name = "backToMain";
            this.backToMain.Size = new System.Drawing.Size(96, 23);
            this.backToMain.TabIndex = 3;
            this.backToMain.Text = "بازگشت به منوی اصلی";
            this.backToMain.UseVisualStyleBackColor = true;
            this.backToMain.Click += new System.EventHandler(this.backToMain_Click);
            // 
            // KardexManfiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 231);
            this.Controls.Add(this.backToMain);
            this.Controls.Add(this.SendSanads);
            this.Controls.Add(this.SanadSN);
            this.Controls.Add(this.SanadSTxtBox);
            this.Name = "KardexManfiFrm";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SanadSTxtBox;
        private System.Windows.Forms.Label SanadSN;
        private System.Windows.Forms.Button SendSanads;
        private System.Windows.Forms.Button backToMain;
    }
}

