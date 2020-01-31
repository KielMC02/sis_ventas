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
            if (file.ShowDialog() == DialogResult.OK) ;
            {
                //Cargamos la imagen al picturebox con la variable PicImagen
                PicImagen.Image = Image.FromFile(file.FileName);
                //Obtenemos el nombre de la imagen
                TxtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\")+1);
                //Aqui obtenemos la ruta de la imagen
                this.RutaOrigen = file.FileName;
            }
        }

        private void BtnGeneralCodigo_Click(object sender, EventArgs e)
        {
            if(TxtCodigo.Text != string.Empty) { 
            //Declaramos un objeto de tipo Barcodelib. Con su clase Barcode.
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            //Especificamos que debe inluir el texto debajo del codigo.
            Codigo.IncludeLabel = true;
            //Cargamos la imagen del codigo de barras dentro del panel, utilizamos el metodo Encode para codificar y luego establecemos le tipo de codigo que queremos, donde queremos que aparezca y los atributos como color de barras, fondo y tamano.
            PanelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128,TxtCodigo.Text,Color.Black,Color.White,213,123);
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
                if (CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtStock.Text == string.Empty || TxtPrecio_Venta.Text == string.Empty || TxtCodigo.Text == string.Empty || TxtDescripcion.Text ==  string.Empty )
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
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(),TxtNombre.Text.Trim(),Convert.ToDecimal(TxtPrecio_Venta.Text.Trim()), Convert.ToInt32(TxtStock.Text.Trim()), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
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
                /*Obtenemos el Id del registro seleccionado*/
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                CboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                TxtCodigo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtPrecio_Venta.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                TxtStock.Text =  Convert.ToString(DgvListado.CurrentRow.Cells["Stock"].Value);
                TxtDescripcion.Text =  Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                string Imagen;
                Imagen = Convert.ToString(DgvListado.CurrentRow.Cells["Imagen"].Value);
                if(Imagen != string.Empty)
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
            catch(Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: " + ex.Message);
            }
        }
    }
}
