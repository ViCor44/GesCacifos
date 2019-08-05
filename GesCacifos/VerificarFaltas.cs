using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace GesCacifos
{
    public partial class VerificarFaltas : Form
    {
        static public int[] vetor = new int[541];
        public VerificarFaltas()
        {
            InitializeComponent();
        }

        private void VerificarFaltas_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        string indata;
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //textBox2.Text = "";
            SerialPort sp = (SerialPort)sender;
            indata = sp.ReadLine();
            this.Invoke(new EventHandler(displayText));

        }
        private void displayText(object o, EventArgs e)
        {
            try
            {
                textBox1.Text = " ";
                textBox2.Text = " ";
                textBox1.AppendText(indata);
                int codigoPulseira = Int32.Parse(textBox1.Text);
                SqlCeConnection con = Utility.DataBaseConnection();
                SqlCeDataAdapter sda = new SqlCeDataAdapter("SELECT * FROM cacifos WHERE codigo_pulseira  = " + codigoPulseira, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                    MessageBox.Show("PULSEIRA NÃO REGISTADA!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (DataRow r in dt.Rows)
                {
                    if (vetor[Int32.Parse(r["id_cacifo"].ToString())] == 1)
                        MessageBox.Show("PULSEIRA JÁ LIDA!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    vetor[Int32.Parse(r["id_cacifo"].ToString())] = 1;
                    string cacifo = r["id_cacifo"].ToString();
                    textBox2.Text = cacifo;

                }            
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void VerificarFaltas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Faltas frm = new Faltas();
            frm.ShowDialog();
        }
    }
}
