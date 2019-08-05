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
    public partial class Faltas : Form
    {
        public Faltas()
        {
            InitializeComponent();
        }

        private void Faltas_Load(object sender, EventArgs e)
        {
            int count = 0;
            for(int i = 1; i < VerificarFaltas.vetor.Length; i++)
            {
                if(VerificarFaltas.vetor[i] == 0)
                {                   
                    textBox1.AppendText(i.ToString());
                    textBox1.AppendText(", ");
                    count++;
                }
            }
            textBox1.AppendText("\n");
            textBox1.AppendText("\n");
            textBox1.AppendText("Faltam: " + count + " pulseiras");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
