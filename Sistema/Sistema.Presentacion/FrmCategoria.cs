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
    public partial class FrmCategoria : Form
    {
        //Declaramos una variable de tipo String que guardara el nombre anterior de la Categoria
        private string NombreAnt;
        public FrmCategoria()
        {
            InitializeComponent();
        }



        //Metodo para Listar los Datos
        private void Listar()
        {
            try
            {
                //Llenamos el DataGrid con el metodo Listar de la Clase Ncategoria
                DgvListado.DataSource = NCategoria.Listar();
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
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
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

        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].Width = 400;
            DgvListado.Columns[3].HeaderText = "Descripción";
            DgvListado.Columns[4].Width = 100;

        }
        //Este Metodo Limpia los campos
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtDescripcion.Clear();
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

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            //Utilizamos el metodo de esta misma clase
            this.Listar();

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //Llamamos el metodo
            this.Buscar();
        }

        //Boton para insertar registro
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos que los campos necesarios esten llenos
                if (TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(TxtNombre,"Ingrese un nombre.");
                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {
                    Rpta = NCategoria.Insertar(TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Categoria Creada Exitosamente");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                    
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        //Cancelamos el agregar dato
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
   
        }
        //Metodo para el doble clic de una fila del datagrid
        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            this.Limpiar();
            //Deshabilitamos el boton insertar y habilitamos el Actulizar
            BtnActualizar.Visible = true;
            BtnInsertar.Visible = false;
            //Llenamos cada textbox con los datos de la fila seleccionada, como cada columnas es un objeto  hay que convertirlo a un String, utilizamos los nombres establecidos con AS en el Procedimiento "categoria_listar"
            TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
            //Akamcenamos el nombre en esa variable
            this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            TxtDescripcion.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
            //Pasamos automaticamente a la pestana mantenimiento
            TabGeneral.SelectedIndex = 1;
            }
            catch(Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos que los campos necesarios esten llenos
                if (TxtNombre.Text == string.Empty || TxtId.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(TxtNombre, "Ingrese un nombre.");
                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {
                    //La Variable respuesta almacena los datos que deben ser actulizados, 
                    Rpta = NCategoria.Actualizar(Convert.ToInt32(TxtId.Text),this.NombreAnt,TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Categoria Actualizada Exitosamente");
                        this.Limpiar();
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            //Utilizamos el estado del Checkbox seleccionar para habilitar o Deshabilitar Botones
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;

            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Utilizamos el objeto "e" que recibe el metodo para obtener la columna seleccionar
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                //NO ENTIENDO ESTE CODIGO
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos un mensaje de dailogo con las opciones necesarias
                DialogResult Opcion;
                //Mostramos el mensaje  y establecemos la opciones OK(para continuar)- Cancel(Para Cancelar), establecemos que es de tipo Cuestion
                Opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro?", "Sistema de Eventos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                //Si la Opcion es OK
                if (Opcion == DialogResult.OK)
                {
                    
                    int codigo;
                    string Rpta = "";
                    //Creamos un foreach que va recorrer todas las filas seleccionadas
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //Convertimos a Booleanos el valor de la casilla seleccionar
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Tomamos el ID y ese es el parametro que le enviaremos a nuestro metodo Eliminar.
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NCategoria.Eliminar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el Registro" + Convert.ToString(row.Cells[2].Value));
                            }
                            //En caso de que no mostrar el error.
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }

                    }
                    //Volvemos al listado.
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {

            try
            {
                //Creamos un mensaje de dailogo con las opciones necesarias
                DialogResult Opcion;
                //Mostramos el mensaje  y establecemos la opciones OK(para continuar)- Cancel(Para Cancelar), establecemos que es de tipo Question(Pregunta)
                Opcion = MessageBox.Show("Realmente deseas Activar el(los) registro?", "Sistema de Eventos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                //Si la Opcion es OK
                if (Opcion == DialogResult.OK)
                {

                    int codigo;
                    string Rpta = "";
                    //Creamos un foreach que va recorrer todas las filas seleccionadas
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //Convertimos a Booleanos el valor de la casilla seleccionar
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Tomamos el ID y ese es el parametro que le enviaremos a nuestro metodo Activar.
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NCategoria.Activar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Activo el Registro" + Convert.ToString(row.Cells[2].Value));
                            }
                            //En caso de que no mostrar el error.
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }

                    }
                    //Volvemos al listado.
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos un mensaje de dailogo con las opciones necesarias
                DialogResult Opcion;
                //Mostramos el mensaje  y establecemos la opciones OK(para continuar)- Cancel(Para Cancelar), establecemos que es de tipo Question(Pregunta)
                Opcion = MessageBox.Show("Realmente deseas Desactivar el(los) registro?", "Sistema de Eventos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                //Si la Opcion es OK
                if (Opcion == DialogResult.OK)
                {

                    int codigo;
                    string Rpta = "";
                    //Creamos un foreach que va recorrer todas las filas seleccionadas
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //Convertimos a Booleanos el valor de la casilla seleccionar
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Tomamos el ID y ese es el parametro que le enviaremos a nuestro metodo Activar.
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NCategoria.Desactivar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Desactivo el Registro " + Convert.ToString(row.Cells[2].Value));
                            }
                            //En caso de que no mostrar el error.
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }

                    }
                    //Volvemos al listado.
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

    }
    
}
