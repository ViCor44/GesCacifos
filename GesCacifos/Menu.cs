using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GesCacifos
{
    public partial class GesPulseira : Form
    {
        public GesPulseira()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inserir frm = new Inserir();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VerificarFaltas frm = new VerificarFaltas();
            frm.Show();
        }
    }
}
