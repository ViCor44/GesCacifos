using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlServerCe;

namespace GesCacifos
{
    class Utility
    {
        // Conexão à base de dados
        public static SqlCeConnection DataBaseConnection()
        {
            string pastaBd = Application.StartupPath + @"\";
            string nomeBD = "GesCacifos.sdf";
            string source = "Data Source=" + pastaBd + nomeBD;
            SqlCeConnection con = new SqlCeConnection(source);
            return con;
        }
        //------------------------------------------------
        // Message box para fechar a aplicação
        public static bool CloseApliccation()
        {
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("Quer mesmo fechar a aplicação?", "Alerta!", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        //---------------------------------------------------
    }
}
