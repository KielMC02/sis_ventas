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
    public partial class FrmUsuario : Form
    {
        private string emailant;
        public FrmUsuario()
        {
            InitializeComponent();
        }



        //Metodo para Listar los Datos
        private void Listar()
        {
            try
            {
                //Llenamos el DataGrid con el metodo Listar de la Clase Ncategoria
                DgvListado.DataSource = NUsuario.Listar();
                //Aplicamos el formato al datagrid con el metodo "Formato"
                this.Formato();
                //Limpiamos campos
                this.Limpiar();
                //Le asignamos texto al Label total Registros
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        //Metodo para filtrar entre las categorias existentes
        private void Buscar()
        {
            try
            {
                //Llenamos el DataGrid con el metodo Listar de la Clase Ncategoria
                DgvListado.DataSource = NUsuario.Buscar(TxtBuscar.Text);
                //Aplicamos el formato al datagrid con el metodo "Formato"
                this.Formato();
                //Le asignamos texto al Label total Registros
                LblTotal.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        //Este metodo le da formato a los Campos del Datagrid
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[4].Width = 170;
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Numero Doc.";
            DgvListado.Columns[7].Width = 120;
            DgvListado.Columns[7].HeaderText = "Direccion";
            DgvListado.Columns[8].Width = 100;
            DgvListado.Columns[8].HeaderText = "Telefono";
            DgvListado.Columns[9].Width = 120;
        }
        //Este Metodo Limpia los campos
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtNumeroDocumento.Clear();
            TxtEmail.Clear();
            TxtDireccion.Clear();
            TxtTelefono.Clear();
            TxtClave.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            Erroricono.Clear();

            //Limpiar para seleccionar
            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        //Muestra un mensaje de error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CargarRol()
        {
            try
            {
                //Obtenemos los datos
                CboRol.DataSource = NRol.Listar();
                //Obtenemos el valor apartir del ID
                CboRol.ValueMember = "idrol";
                //Mostramos el texto a partir del nombre del rol
                CboRol.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarRol();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        //Boton para insertar registro
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos que los campos necesarios esten llenos
                if (CboRol.Text == string.Empty ||  TxtNombre.Text == string.Empty || CboTipoDocumento.Text == string.Empty || TxtNumeroDocumento.Text == string.Empty || TxtTelefono.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtEmail.Text == string.Empty || TxtClave.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(CboRol, "Ingrese un Rol.");
                    Erroricono.SetError(TxtNombre, "Ingrese un nombre.");
                    Erroricono.SetError(CboTipoDocumento, "Ingrese un tipo de documento.");
                    Erroricono.SetError(TxtNumeroDocumento, "Ingrese un numero de documento.");
                    Erroricono.SetError(TxtTelefono, "Ingrese un numero de telefono.");
                    Erroricono.SetError(TxtDireccion, "Ingrese una Direecion");
                    Erroricono.SetError(TxtEmail, "Ingrese un Email");
                    Erroricono.SetError(TxtClave, "Ingrese una Clave.");
                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {
                    
                    Rpta = NUsuario.Insertar(Convert.ToInt32(CboRol.SelectedValue), TxtNombre.Text.Trim(), CboTipoDocumento.Text.Trim(), TxtNumeroDocumento.Text.Trim(), TxtDireccion.Text.Trim(), TxtTelefono.Text.Trim(), TxtEmail.Text.Trim(),TxtClave.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Usuario Creado Exitosamente");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                /*Obtenemos la informacion de cada celda (registro) y se lo asignamos a su respectivo textbox para poder editarlo*/
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                CboRol.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idrol"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                //this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                CboTipoDocumento.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Tipo_Documento"].Value);
                TxtNumeroDocumento.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Numero_Documento"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Direccion"].Value);
                TxtTelefono.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);
                this.emailant = Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TxtEmail.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: " + ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos que los campos necesarios esten llenos
                if (CboRol.Text == string.Empty || TxtNombre.Text == string.Empty || CboTipoDocumento.Text == string.Empty || TxtNumeroDocumento.Text == string.Empty || TxtTelefono.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtDireccion.Text == string.Empty || TxtEmail.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(CboRol, "Ingrese un Rol.");
                    Erroricono.SetError(TxtNombre, "Ingrese un nombre.");
                    Erroricono.SetError(CboTipoDocumento, "Ingrese un tipo de documento.");
                    Erroricono.SetError(TxtNumeroDocumento, "Ingrese un numero de documento.");
                    Erroricono.SetError(TxtTelefono, "Ingrese un numero de telefono.");
                    Erroricono.SetError(TxtDireccion, "Ingrese una Direecion");
                    Erroricono.SetError(TxtEmail, "Ingrese un Email");

                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {

                    Rpta = NUsuario.Actualizar(Convert.ToInt32(TxtId.Text),Convert.ToInt32(CboRol.SelectedValue), TxtNombre.Text.Trim(), CboTipoDocumento.Text.Trim(), TxtNumeroDocumento.Text.Trim(), TxtDireccion.Text.Trim(), TxtTelefono.Text.Trim(), this.emailant,TxtEmail.Text.Trim(), TxtClave.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Usuario Actulizado Exitosamente");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            this.Close();
        }
    }
}
