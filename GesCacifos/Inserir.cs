using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace GesCacifos
{
    public partial class Inserir : Form
    {
        static public int[] faltaInserir = new int[541];
        public Inserir()
        {
            InitializeComponent();
        }

        private void Inserir_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedIndex = 0;
            SqlCeConnection con = Utility.DataBaseConnection();
            SqlCeDataAdapter sda = new SqlCeDataAdapter("SELECT * FROM cacifos ORDER BY id_cacifo", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int i = 1;            
            foreach (DataRow r in dt.Rows)
            {
                int j;
                if (int.TryParse(r["codigo_pulseira"].ToString(), out j))                   
                    faltaInserir[i] = 1;
                else
                    faltaInserir[i] = 0;
                i++;
            }

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
                button2.Enabled = true;

            }
        }

        private void Inserir_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

       
        string indata;
        private  void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                indata = sp.ReadLine();
                if (indata != "0")
                    this.Invoke(new EventHandler(displayText));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);              

            }

        }
        private void displayText(object o, EventArgs e)
        {
            textBox2.Text = " ";
            textBox2.AppendText(indata);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idCacifo = Int32.Parse(textBox1.Text);
                int codigoPulseira = Int32.Parse(textBox2.Text);
                SqlCeConnection con = Utility.DataBaseConnection();
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandText = "UPDATE cacifos SET codigo_pulseira = @codigo_pulseira Where id_cacifo = " + idCacifo;
                //cmd.Parameters.AddWithValue("id_cacifo", idCacifo);
                cmd.Parameters.AddWithValue("codigo_pulseira", codigoPulseira);
                con.Open();
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (i != 0)
                    MessageBox.Show("Inserida com sucesso");
                else
                    MessageBox.Show("Erro ao inserir");
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FaltaInserir frm = new FaltaInserir();
            frm.ShowDialog();
        }
    }
}
