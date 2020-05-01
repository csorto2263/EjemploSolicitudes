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
    public partial class CrearSolicitudes : Form
    {
        private SqlConnection con;
        private string userFrm;
        private SqlCommand command;
        private bool existe = false;

        public CrearSolicitudes(string user, SqlConnection conParametro)
        {
            InitializeComponent();
            con = conParametro;
            userFrm = user;
        }

        private void CrearSolicitudes_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            con.Open();
            string sql = null;
            SqlDataReader dataReader;



            sql = "SELECT * FROM CLIENTES where id = '" + txtID.Text.Replace("-", "") + "'";

            command = new SqlCommand(sql, con);

            dataReader = command.ExecuteReader();
            while (dataReader.Read())//si lee, el cliente existe y llena los campos
            {
                txtNombre.Text = dataReader.GetValue(1).ToString();
                txtApellido.Text = dataReader.GetValue(2).ToString();
                dtFechaNacimiento.Value = Convert.ToDateTime(dataReader.GetValue(3));
                cbSexo.Text = dataReader.GetValue(4).ToString();
                existe = true;
            }
            dataReader.Close();
            dataReader.Dispose();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            con.Open();
            string sql = null;
            SqlDataReader dataReader;

            int solicitudId = comboBox1.SelectedIndex;

            if (!existe)
            {
                DialogResult dialogResult = MessageBox.Show("El cliente no existe en la base de datos, ¿Desea agregarlo?", "Cliente no existe", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)//y lo inserta automaticamente si responde que si.
                {
                    sql = "insert into CLIENTES (ID, NOMBRE, APELLIDO, FECHA_NACIMIENTO, SEXO) VALUES ('" + txtID.Text.Replace("-", "") + "', '" + txtNombre.Text + "', '" + txtApellido.Text + "', '" + dtFechaNacimiento.Value.Date + "', '" + cbSexo.Text + "')";
                    command = new SqlCommand(sql, con);
                    command.ExecuteNonQuery();

                    sql = "insert into SOLICITUDES_CLIENTES (SOLICITUDID, ID, ESTADOID, USERNAME) VALUES (" + solicitudId + ", '" + txtID.Text.Replace("-", "") + "', 0, '" + userFrm + "')";
                    command = new SqlCommand(sql, con);
                    command.ExecuteNonQuery();
                }
                else if (dialogResult == DialogResult.No)
                {
                    txtID.Text = "";
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                }
            }
            else//verificar que no tenga solicitudes pendientes
            {
                bool tienePendiente = false;

                sql = "select estadoid from SOLICITUDES_CLIENTES where id = '"+ txtID.Text.Replace("-", "") + "'";
                command = new SqlCommand(sql, con);

                dataReader = command.ExecuteReader();
                while (dataReader.Read())//Leerá hasta encontrar una pendiente
                {
                    if(Convert.ToInt32(dataReader.GetValue(0)) == 1)
                    {
                        tienePendiente = true;
                        break;
                    }
                }

                dataReader.Close();
                dataReader.Dispose();

                if (tienePendiente)
                {
                    txtID.Text = "";
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                    dtFechaNacimiento.Value = DateTime.Today;
                    cbSexo.Text = "";
                    comboBox1.Text = "";

                    MessageBox.Show("No se puede ingresar otra solicitud para el cliente especificado", "Cliente pendiente de aprobar solicitud", MessageBoxButtons.OK);
                }
                else
                {
                    sql = "insert into SOLICITUDES_CLIENTES (SOLICITUDID, ID, ESTADOID, USERNAME) VALUES (" + solicitudId + ", '" + txtID.Text.Replace("-", "") + "', 1, '" + userFrm + "')";
                    command = new SqlCommand(sql, con);
                    command.ExecuteNonQuery();

                    txtID.Text = "";
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                    dtFechaNacimiento.Value = DateTime.Today;
                    cbSexo.Text = "";
                    comboBox1.Text = "";

                    MessageBox.Show("Se ha ingresado la solicitud para el cliente especificado", "Solicitud Ingresada", MessageBoxButtons.OK);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
