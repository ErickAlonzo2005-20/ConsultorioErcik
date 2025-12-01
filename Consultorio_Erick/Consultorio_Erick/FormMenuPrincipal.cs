
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consultorio_Erick
{
    public partial class FormMenuPrincipal : Form
    {

        public FormMenuPrincipal()
        {
            InitializeComponent();
            


        }

        private void citaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCita frm = new FormCita();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dentistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDentistas frm = new FormDentistas();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
