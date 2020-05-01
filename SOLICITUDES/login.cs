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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection("Server= localhost\\localsqlbd; Database= solicitudes_bd;Integrated Security=SSPI");
        private SqlCommand command;
        private Int32 levelID = -1;


        private void button1_Click(object sender, EventArgs e)
        {
            Verificar();
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
        private void Verificar()
        {
            con.Close();
            if (txtUsuario.Text.Length > 0)
                if (txtContrasena.Text.Length > 0)
                {
                    try
                    {
                        con.Open();
                        string sql = null;
                        SqlDataReader dataReader;
                        sql = @"SELECT PASSWORD, 
                                      LEVELID 
                                 FROM USUARIOS
                                WHERE USERNAME = '" + txtUsuario.Text.Trim() + "'";

                        command = new SqlCommand(sql, con);

                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (txtContrasena.Text == dataReader.GetValue(0).ToString())
                            {
                                levelID = Convert.ToInt32(dataReader.GetValue(1));
                            }
                            else
                            {
                                MessageBox.Show("Contraseña incorrecta");
                                txtContrasena.Text = "";
                                txtUsuario.Text = "";
                            }
                        }

                        dataReader.Close();
                        dataReader.Dispose();
                        con.Close();

                        if (levelID != -1)
                        {
                            Principal frm = new Principal(txtUsuario.Text.Trim(), levelID, con);
                            frm.ShowDialog(this);
                            this.Close();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    string message = "Debe ingresar una contraseña. Desea continuar?";
                    string caption = "Contraseña inválida";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.No)
                    {
                        // Closes the parent form.
                        this.Close();
                    }

                }
            else
            {
                string message = "Debe ingresar un usuario. Desea continuar?";
                string caption = "Usuario inválido";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    // Closes the parent form.
                    this.Close();
                }
            }
        }
    }
}
