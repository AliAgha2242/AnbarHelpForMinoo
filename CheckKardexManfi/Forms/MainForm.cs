using additions;
using CheckKardexManfi;
using Dapper;
using Database.Connnection;
using DifffMalIAndAnbar.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnbarHelp.Forms
{
    public partial class MainForm : Form
    {
        public IMemoryCache memory { get; set; }
        public TotalQuery Queries { get; set; }
        public DatabaseConfigure Db { get; set; }
        public static List<string> result = new List<string>();
        public MainForm()
        {
            Db = new DatabaseConfigure();
            Queries = new TotalQuery();
            InitializeComponent();
        }

        private void KardexManfiFrmBtn_Click(object sender, EventArgs e)
        {
            KardexManfiFrm kardexManfiFrm = new KardexManfiFrm();
            kardexManfiFrm.Show();
            //this.WindowState = FormWindowState.Minimized;
        }

        private void DiffMaliAndAnbar_Click(object sender, EventArgs e)
        {
            DiffMaliAndAndAnbarFrm frm = new DiffMaliAndAndAnbarFrm();
            frm.Show();
        }

        private void GetEmptyUserCode_Click(object sender, EventArgs e)
        {
            string BtnText = GetEmptyUserCode.Text;

            if (result.Count() == 0)
            {
                Db.CSBuilder("bis", "gbid");
                IDbConnection con;
                if (!Db.checkConnection(out con).IsSuccess)
                {
                    MessageBox.Show("اتصال شما برقرار نیست");
                    return;
                }
                using (con)
                {
                    result = con.Query<string>(Queries.GetEmptyUserCode).ToList();  
                }
            }
            Random rand = new Random();
            GetEmptyUserCode.Text = result.ToList()[rand.Next(result.Count())];
        }
    }
}
