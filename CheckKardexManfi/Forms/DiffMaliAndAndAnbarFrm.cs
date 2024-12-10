using additions;
using DifffMalIAndAnbar.Classes;
using DifffMalIAndAnbar.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnbarHelp.Forms
{
    public partial class DiffMaliAndAndAnbarFrm : Form
    {
        private DiffMain diffMain;
        private ServerInfo serverInfo;
        private CalenderHelp calenderHelp;

        private static string FromDate = "14030101";

        private static string ToDate;
        public DiffMaliAndAndAnbarFrm()
        {
            diffMain = new DiffMain();
            InitializeComponent();
        }

        private void DiffMaliAndAndAnbarFrm_Load(object sender, EventArgs e)
        {
            diffMain = new DiffMain();
            this.dataGridView1.Visible = false;
            this.GVDiffMaliAndAnbar.Visible = false;
            this.GVDiffAnbarAndMali.Visible = false;


            if (string.IsNullOrWhiteSpace(FromDateTxt.Text) || string.IsNullOrWhiteSpace(ToDateTxt.Text))
            {
                (string, string) fromDateAndToDatedefault = CalenderHelp.MiladiToShamsiBoth(DateTime.Now);

                FromDate = fromDateAndToDatedefault.Item1;
                ToDate = fromDateAndToDatedefault.Item2;
                this.FromDateTxt.Text = FromDate;
                this.ToDateTxt.Text = ToDate;

            }
            var Items = diffMain.GetServerMain();
            if (!Items.IsSuccess)
            {
                MessageBox.Show(Items.ErrorMessage);
                return;
            }
            this.ServerComBox.DataSource = Items.TObject.Select(p => p.ServerFullName).ToList();
        }

        private void ServerSelected_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (FromDateTxt.Text.Length != 8 || Regex.IsMatch(FromDateTxt.Text, "[a-z]") || ToDateTxt.Text.Length != 8 || Regex.IsMatch(ToDateTxt.Text, "[a-z]"))
            {
                MessageBox.Show("تاریخ را به درستی وارد کنید ");
                this.Cursor = Cursors.Default;
                return;
            }
            FromDate = FromDateTxt.Text;
            ToDate = ToDateTxt.Text;


            var ServerName = this.ServerComBox.SelectedValue.ToString().Split('_')[0];

            var FindDbResult = diffMain.FindDb(ServerName);
            if (!FindDbResult.IsSuccess)
            {
                MessageBox.Show(FindDbResult.ErrorMessage);
                this.Cursor = Cursors.Default;
                return;
            }
            serverInfo = new ServerInfo()
            {
                serverName = ServerName,
                dbName = FindDbResult.TObject
            };


            var result = diffMain.ShowDiffrent(ServerName, FindDbResult.TObject, FromDateTxt.Text, ToDateTxt.Text);
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessage);
                this.Cursor = Cursors.Default;
                return;
            }
            if (result.TObject.Count() == 0)
            {
                MessageBox.Show(string.Format("در این سرور {0} هیچگونه مغایرتی وجود ندارد.",ServerName));
                this.Cursor = Cursors.Default;
                return;
            }
            this.dataGridView1.DataSource = result.TObject.ToList();
            var DiffDtoResult = diffMain.FindDiffrentReturnSanad(result.TObject.First().Anbar_TarakoneshDs, result.TObject.First().Anbar_SanadDate
                , serverInfo.serverName, serverInfo.dbName);

            if (!DiffDtoResult.IsSuccess)
            {
                MessageBox.Show(DiffDtoResult.ErrorMessage);
                this.Cursor = Cursors.Default;
                return;
            }

            var r = diffMain.EkhtelafMaliAndAnbarDetail(DiffDtoResult.TObject.difffMalIAndAnbar.Last().PeigiriNo, DiffDtoResult.TObject.difffMalIAndAnbar.Last().PeigiriDate
                , serverInfo.serverName, serverInfo.dbName);
          
            
            this.ServerComBox.Visible = false;
            this.LblServerSelect.Visible = false;
            this.ToDateTxt.Visible = false;
            this.FromDateTxt.Visible = false;
            this.ServerSelected.Visible = false;
            
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.Visible = true;

            if (diffMain.ControllTarakonesh111And61(result.TObject) == null)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(" تراکنس های 111 و 61 دارای بیزنس متفاوت هستند که با توجه محسابات انجام شده هیچگونه مغایرتی ندارند");
                return;
            }

            this.GVDiffAnbarAndMali.DataSource = DiffDtoResult.TObject.DiffAnbarAndMali.ToList();
            this.GVDiffAnbarAndMali.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.GVDiffMaliAndAnbar.DataSource = DiffDtoResult.TObject.difffMalIAndAnbar.ToList();
            this.GVDiffMaliAndAnbar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.GVDiffMaliAndAnbar.Visible = true;
            this.GVDiffAnbarAndMali.Visible = true;

            this.Cursor = Cursors.Default;
            return;
        }


    }

}
