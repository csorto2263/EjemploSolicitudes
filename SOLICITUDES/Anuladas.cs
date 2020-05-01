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
    public partial class Anuladas : Form
    {
        private SqlConnection con;
        private string userFrm;
        private SqlCommand command;
        public Anuladas(string user, SqlConnection conParametro)
        {
            InitializeComponent();
            con = conParametro;
            userFrm = user;
        }

        private void Aprobar_Load(object sender, EventArgs e)
        {
            con.Close();
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

            sql = @"SELECT A.SOLICITUDID, 
                           C.DESCRIPTION, 
                           A.ID, 
                           B.NOMBRE, 
                           B.APELLIDO, 
                           A.ESTADOID
                      FROM SOLICITUDES_CLIENTES A, 
                           CLIENTES B, 
                           TIPOS_SOLICITUDES C 
                     WHERE A.ID = B.ID AND
                           A.SOLICITUDID = C.SOLICITUDID AND
                           A.ESTADOID = 4";

            command = new SqlCommand(sql, con);
            SqlDataAdapter DaRec2 = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            DaRec2.Fill(dt);

            gvListadoCreados.DataSource = dt;
        }
    }
}
