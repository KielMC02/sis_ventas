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
    public partial class FrmRol : Form
    {
        public FrmRol()
        {
            InitializeComponent();
        }
        private void Formato()
        {
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[0].HeaderText = "ID";
            DgvListado.Columns[1].Width = 2000;
            DgvListado.Columns[1].HeaderText = "Nombre";
        }
        private void Listar()
        {
            try
            {
                //Llenamos el DataGrid con el metodo Listar de la Clase Ncategoria
                DgvListado.DataSource = NRol.Listar();
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
        private void FrmRol_Load(object sender, EventArgs e)
        {
            this.Listar();

        }
    }
}
