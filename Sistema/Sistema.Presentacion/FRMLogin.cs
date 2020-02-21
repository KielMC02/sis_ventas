using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FRMLogin : Form
    {
        public FRMLogin()
        {
            InitializeComponent();
        }

        private void BtnAcceder_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            try
            {

                tabla = NUsuario.Login(TxtEmail.Text.Trim(), TxtClave.Text.Trim());
                if (tabla.Rows.Count <= 0)
                {
                    MessageBox.Show("El email o la clave es incorrecta", "Accesso al Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (Convert.ToBoolean(tabla.Rows[0][4]) == false)
                {
                    MessageBox.Show("Este Usuario no esta Activo", "Accesso al Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Bienvenido/a " + Convert.ToString(tabla.Rows[0][3]), "Accesso al Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmPrincipal frm = new FrmPrincipal();
                    frm.IdUsuario = Convert.ToInt32(tabla.Rows[0][0]);
                    frm.Idrol = Convert.ToInt32(tabla.Rows[0][1]);
                    frm.Rol = Convert.ToString(tabla.Rows[0][2]);
                    frm.Nombre = Convert.ToString(tabla.Rows[0][3]);
                    frm.Estado = Convert.ToBoolean(tabla.Rows[0][4]);
                    frm.Show();
                    this.Hide();

                }
            }
            catch
            {

            }
        }
    }
}
