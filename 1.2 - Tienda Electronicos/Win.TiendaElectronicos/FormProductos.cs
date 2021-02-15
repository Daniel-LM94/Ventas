using BL.Tecnologia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.TiendaElectronicos
{
    public partial class FormProductos : Form
    {
        ProductosBL Productos;

        public FormProductos()
        {
            InitializeComponent();

            Productos = new ProductosBL();

            listaProductosBindingSource.DataSource = Productos.ObtenerProductos();
        }
    }
}
