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
    public partial class SoliActivas : Form
    {
        private SqlConnection con;
        private string userFrm;
        private SqlCommand command;
        public SoliActivas(string user, SqlConnection conParametro)
        {
            InitializeComponent();
            con = conParametro;
            userFrm = user;
        }

        private void Aprobar_Load(object sender, EventArgs e)
        {
            LlenarGrid();
            con.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LlenarGrid()
        {
            con.Open();
            string sql = null;

            sql = "select A.SOLICITUDID, c.DESCRIPTION, A.ID, b.nombre, b.apellido, a.estadoid from SOLICITUDES_CLIENTES A, clientes b, tipos_solicitudes c where a.ID = b.ID and a.SOLICITUDID = c.SOLICITUDID and a.estadoid = 3";

            command = new SqlCommand(sql, con);
            SqlDataAdapter DaRec2 = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            DaRec2.Fill(dt);

            gvListadoCreados.DataSource = dt;
        }
    }
}
