using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOLICITUDES
{
    public partial class Principal : Form
    {
        private SqlConnection con;
        private string userFrm;
        public Principal(string user, int level, SqlConnection conParametro)
        {
            InitializeComponent();
            if (level == 0)
            {
                aprobarToolStripMenuItem.Enabled = false;
                activarToolStripMenuItem.Enabled = false;
            }
            con = conParametro;
            userFrm = user;

        }

        private void solicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aprobarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aprobar frm = new Aprobar(userFrm, con);
            frm.ShowDialog(this);
        }

        private void activarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activar frm = new Activar(userFrm, con);
            frm.ShowDialog(this);
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearSolicitudes frm = new CrearSolicitudes(userFrm, con);
            frm.ShowDialog(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActualizarAnuladas();
        }

        private void ActualizarAnuladas()
        {
            con.Open();
            string sql = null;
            sql = "update SOLICITUDES_CLIENTES set ESTADOID = 4  where DATEDIFF (DAY, fecha_creada , '" + DateTime.Today.Date + "' )  > 30";

            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Se han actualizado las solicitudes con más de 30 días a anuladas");

        }

        private void activasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoliActivas frm = new SoliActivas(userFrm, con);
            frm.ShowDialog(this);
        }

        private void anuladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anuladas frm = new Anuladas(userFrm, con);
            frm.ShowDialog(this);
        }
    }
}
