using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio = "D:\\Imagen\\";
        private string NombreAnt;
        public FrmArticulo()
        {
            InitializeComponent();
        }

        //Metodo para Listar los Datos
        private void Listar()
        {
            try
            {
                //Llenamos el DataGrid con el metodo Listar de la Clase Ncategoria
                DgvListado.DataSource = NArticulo.Listar();
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
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 50;
            DgvListado.Columns[1].Width = 500;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoria";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Codigo";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Venta";
            DgvListado.Columns[7].Width = 60;
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripcion";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;

        }
        //Este Metodo Limpia los campos
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            TxtImagen.Clear();
            TxtStock.Clear();
            TxtPrecio_Venta.Clear();
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
        //Este metodo carga las categorias existentes dentro del Combo box de las categoiras
        private void CargarCategoira()
        {
            try
            {
                //Obtenemos los datos
                CboCategoria.DataSource = NCategoria.Seleccionar();
                //Obtenemos el valor apartir del ID
                CboCategoria.ValueMember = "idcategoria";
                //Mostramos el texto a partir del nombre de la categoria
                CboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoira();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnCargarImagen_Click(object sender, EventArgs e)
        {
            //Este es un objeto de open file dialog, esta clase se utiliza para manejar archivos
            OpenFileDialog file = new OpenFileDialog();
            //Aqui establecemos que tipo de archivos va manejaar nuestra aplicacion
            file.Filter = "Image files (*.jpg,*.jpeg, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png;";
            // En caso de que el usuario cargue un archivo del tipo permitido
            if (file.ShowDialog() == DialogResult.OK)
            {
                //Cargamos la imagen al picturebox con la variable PicImagen
                PicImagen.Image = Image.FromFile(file.FileName);
                //Obtenemos el nombre de la imagen
                TxtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                //Aqui obtenemos la ruta de la imagen
                this.RutaOrigen = file.FileName;
            }
        }

        private void BtnGeneralCodigo_Click(object sender, EventArgs e)
        {
            if (TxtCodigo.Text != string.Empty)
            {
                //Declaramos un objeto de tipo Barcodelib. Con su clase Barcode.
                BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                //Especificamos que debe inluir el texto debajo del codigo.
                Codigo.IncludeLabel = true;
                //Cargamos la imagen del codigo de barras dentro del panel, utilizamos el metodo Encode para codificar y luego establecemos le tipo de codigo que queremos, donde queremos que aparezca y los atributos como color de barras, fondo y tamano.
                PanelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, TxtCodigo.Text, Color.Black, Color.White, 213, 123);
                //Habilitamos el boton Guardar Codigo
                BtnGuardarCodigo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Debre ingresar un codigo para poder generarlo", "Error al General Codigo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardarCodigo_Click(object sender, EventArgs e)
        {
            Image imgfinal = (Image)PanelCodigo.BackgroundImage.Clone();

            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.AddExtension = true;
            DialogoGuardar.Filter = "Image PNG (*.png)| *.png";
            DialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(DialogoGuardar.FileName))
            {
                imgfinal.Save(DialogoGuardar.FileName, ImageFormat.Png);
            }
            imgfinal.Dispose();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos que los campos necesarios esten llenos
                if (CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtStock.Text == string.Empty || TxtPrecio_Venta.Text == string.Empty || TxtCodigo.Text == string.Empty || TxtDescripcion.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(CboCategoria, "Ingrese un Categoria.");
                    Erroricono.SetError(TxtNombre, "Ingrese un Nombre.");
                    Erroricono.SetError(TxtStock, "Ingrese un Stock.");
                    Erroricono.SetError(TxtPrecio_Venta, "Ingrese un Precio de Venta.");
                    Erroricono.SetError(TxtCodigo, "Ingrese un Codigo de Barras.");
                    Erroricono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(), TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecio_Venta.Text.Trim()), Convert.ToInt32(TxtStock.Text.Trim()), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Articulo Creada Exitosamente");
                        if (TxtImagen.Text != string.Empty)
                        {
                            RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, RutaDestino);
                        }
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

        private void DgvListado_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                /*Obtenemos la informacion de cada celda (registro) y se lo asignamos a su respectivo textbox para poder editarlo*/
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                CboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                TxtCodigo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtPrecio_Venta.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                TxtStock.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Stock"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                string Imagen;
                Imagen = Convert.ToString(DgvListado.CurrentRow.Cells["Imagen"].Value);
                if (Imagen != string.Empty)
                {
                    PicImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    TxtImagen.Text = Imagen;
                }
                else
                {
                    PicImagen.Image = null;
                    TxtImagen.Text = "";
                }
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
                if (TxtId.Text == string.Empty || CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtStock.Text == string.Empty || TxtPrecio_Venta.Text == string.Empty || TxtCodigo.Text == string.Empty || TxtDescripcion.Text == string.Empty)
                {
                    this.MensajeError("Falta Ingresar Algunos Datos, Seran marcados.");
                    Erroricono.SetError(CboCategoria, "Ingrese un Categoria.");
                    Erroricono.SetError(TxtNombre, "Ingrese un Nombre.");
                    Erroricono.SetError(TxtStock, "Ingrese un Stock.");
                    Erroricono.SetError(TxtPrecio_Venta, "Ingrese un Precio de Venta.");
                    Erroricono.SetError(TxtCodigo, "Ingrese un Codigo de Barras.");
                    Erroricono.SetError(TxtDescripcion, "Ingrese una descripcion.");
                }
                //En caso de que no Ejecutamos el metodo Insertar
                else
                {
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(TxtId.Text), Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(), this.NombreAnt, TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecio_Venta.Text.Trim()), Convert.ToInt32(TxtStock.Text.Trim()), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se actualizo Correctamente");
                        if (TxtImagen.Text != string.Empty && this.RutaOrigen != string.Empty)
                        {
                            RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, RutaDestino);
                        }
                        this.Listar();
                        TabGeneral.SelectedIndex = 0;
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
            TabGeneral.SelectedIndex = 0;

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
                    string Imagen = "";
                    //Creamos un foreach que va recorrer todas las filas seleccionadas
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //Convertimos a Booleanos el valor de la casilla seleccionar
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Tomamos el ID y ese es el parametro que le enviaremos a nuestro metodo Eliminar.
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            
                            Imagen = Convert.ToString(row.Cells[9].Value);
                            Rpta = NArticulo.Eliminar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                //this.MensajeOK("Se elimino el Registro" + Convert.ToString(row.Cells[5].Value));
                                //Eliminamos la Imgen de la Carpeta
                                if (Imagen != string.Empty) { 
                                    File.Delete(this.Directorio + Imagen);
                                    this.MensajeOK("Se elimino el Registro " + Convert.ToString(row.Cells[5].Value));
                                }
                                else
                                {
                                    this.MensajeOK("Se elimino el Registro " + Convert.ToString(row.Cells[5].Value));
                                }
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
                            Rpta = NArticulo.Desactivar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Desactivo el Registro " + Convert.ToString(row.Cells[5].Value));
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
                            Rpta = NArticulo.Activar(codigo);
                            //Si la respuesta es satisfactora (OK) entonces se mostrara un mensaje de informacion
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Activo el Registro " + Convert.ToString(row.Cells[5].Value));
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

        private void CboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
