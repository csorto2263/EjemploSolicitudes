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
    public partial class Activar : Form
    {
        private SqlConnection con;
        private string userFrm;
        private SqlCommand command;
        public Activar(string user, SqlConnection conParametro)
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

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            string sql;
            con.Open();
            if (gvListadoCreados.SelectedRows.Count > 0)
            {
                sql = "UPDATE SOLICITUDES_CLIENTES set estadoid = 3, username = '" + userFrm + "' where ID = '" + gvListadoCreados.SelectedRows[0].Cells[2].Value.ToString() + "' and solicitudid = " + gvListadoCreados.SelectedRows[0].Cells[0].Value.ToString() + " and estadoid = " + gvListadoCreados.SelectedRows[0].Cells[5].Value.ToString();

                command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
                LlenarGrid();
            }
            else
                MessageBox.Show("No ha seleccionado ningun registro, debe seleccionar uno para aprobarlo", "Seleccione un registro", MessageBoxButtons.YesNo);
        }

        private void LlenarGrid()
        {
            con.Open();
            string sql = null;

            sql = "select A.SOLICITUDID, c.DESCRIPTION, A.ID, b.nombre, b.apellido, a.estadoid from SOLICITUDES_CLIENTES A, clientes b, tipos_solicitudes c where a.ID = b.ID and a.SOLICITUDID = c.SOLICITUDID and a.estadoid = 2";

            command = new SqlCommand(sql, con);
            SqlDataAdapter DaRec2 = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            DaRec2.Fill(dt);

            gvListadoCreados.DataSource = dt;
        }
    }
}
