using additions;
using AnbarHelp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckKardexManfi
{
    public partial class KardexManfiFrm : Form
    {
        public CheckKardex Sanad { get; set; }
        public KardexManfiFrm()
        {
            InitializeComponent();
        }

        private void SendSanads_Click(object sender, EventArgs e)
        {
            string[] sanads;
            if (string.IsNullOrWhiteSpace(SanadSTxtBox.Text))
            {
                MessageBox.Show("باید مقداری را وارد کنید");
                return;
            }
            if (Regex.Match(SanadSTxtBox.Text, "[a-z]").Success || Regex.Match(SanadSTxtBox.Text, "[!@#$%^&*()_-]").Success)
            {
                MessageBox.Show("مقدار فقط عدد میباشد");
                return;
            }
            if (SanadSTxtBox.Text.EndsWith(',') || SanadSTxtBox.Text.StartsWith(','))
            {
               
                var STRArr = SanadSTxtBox.Text.ToArray();
                SanadSTxtBox.Text = STRArr.First() == ',' ? SanadSTxtBox.Text.Remove(0, 1) : SanadSTxtBox.Text;
                SanadSTxtBox.Text = STRArr.Last() == ',' ? SanadSTxtBox.Text.Remove(SanadSTxtBox.Text.Count()-1, 1) : SanadSTxtBox.Text;
            }


            this.SendSanads.Text = "کمی صبر نمایید";
            this.SendSanads.Enabled = false;

            Sanad = new CheckKardex();
            sanads = SanadSTxtBox.Text.Split(',');
            
            BaseResult<(string, string, double)> ServerAndDbNameAndVahedeTejari = Sanad.GetServerAndDbNameBySanad(sanads.Select(s => double.Parse(s)).ToArray());
            if (ServerAndDbNameAndVahedeTejari.IsSuccess == false)
            {
                MessageBox.Show(ServerAndDbNameAndVahedeTejari.ErrorMessage);
                this.SendSanads.Enabled = true;
                this.SendSanads.Text = "ارسال اسناد";
                return;
            }

            var result = Sanad.CheckKardexMain(sanads.Select(S=>double.Parse(S)).ToArray(), ServerAndDbNameAndVahedeTejari.TObject.Item1, ServerAndDbNameAndVahedeTejari.TObject.Item2);

            if(result.IsSuccess == false)
            {
                MessageBox.Show(result.ErrorMessage);
                return;
            }
            else if (result.TObject.Count < 0)
            {
                MessageBox.Show("سند برای حذف یا موقت شدن مانعی ندارد");
                return;
            }
            this.Opacity = 0.5;
            Form2 form2 = new Form2();
            form2.Show();
            form2.dataGridView1.DataSource = result.TObject;

            this.SendSanads.Text = "ارسال اسناد";
            this.SendSanads.Enabled = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void backToMain_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MainForm main = Application.OpenForms[0] as MainForm;
            main.WindowState = FormWindowState.Normal;

        }
    }
}
