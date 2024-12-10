using CheckKardexManfi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnbarHelp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
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
    }
}
