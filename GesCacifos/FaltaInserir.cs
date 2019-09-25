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
    public partial class FaltaInserir : Form
    {
        public FaltaInserir()
        {
            InitializeComponent();
            button1.Select();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FaltaInserir_Load(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 1; i < Inserir.faltaInserir.Length; i++)
            {
                if (Inserir.faltaInserir[i] == 0)
                {
                    textBox1.AppendText(i.ToString());
                    textBox1.AppendText(", ");
                    count ++;
                }
            }
            textBox1.AppendText("\n");
            textBox1.AppendText("\n");
            textBox1.AppendText("Faltam inserir: " + count + " pulseiras");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
