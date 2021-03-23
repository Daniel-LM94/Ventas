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
    // CLASE - 1

    public partial class FormFactura : Form  // Video 34 - Tarea 7
    {
        FacturasBL _facturasBL;
        ClientesBL _clientesBL;
        ProductosBL _productosBL;

        public FormFactura()
        {
            InitializeComponent();

            _facturasBL = new FacturasBL();
            listaFacturasBindingSource.DataSource = _facturasBL.ObtenerFacturas();

            _clientesBL = new ClientesBL();
            ClienteBindingSource.DataSource = _clientesBL.ObtenerClientes();

            _productosBL = new ProductosBL();
            listaProductosBindingSource.DataSource = _productosBL.ObtenerProductos();
        }

        // METODO - 1 | Sin Uso

        private void FormFactura_Load(object sender, EventArgs e) 
        {
        }

        // METODO - 2 

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _facturasBL.AgregarFactura();
            listaFacturasBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        // METODO - 3 

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled    = valor;
            bindingNavigatorMoveLastItem.Enabled     = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled     = valor;
            bindingNavigatorPositionItem.Enabled     = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible    = !valor;
        }

        // METODO - 4 

        private void listaFacturasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaFacturasBindingSource.EndEdit();
            var factura = (Factura)listaFacturasBindingSource.Current;
            var resultado = _facturasBL.GuardarFactura(factura);

            if (resultado.Exitoso == true)
            {
                listaFacturasBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Factura Guardada");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        // METODO - 5

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            _facturasBL.CancelarCambios();
        }

        // METODO - 6

        private void button1_Click(object sender, EventArgs e)
        {
            var factura = (Factura)listaFacturasBindingSource.Current;

            _facturasBL.AgregarFacturaDetalle(factura);

            DeshabilitarHabilitarBotones(false);
        }

        // METODO - 7

        private void button2_Click(object sender, EventArgs e)
        {
            var factura = (Factura)listaFacturasBindingSource.Current;
            var facturaDetalle = (FacturaDetalle)facturaDetalleBindingSource.Current;

            _facturasBL.RemoverFacturaDetalle(factura, facturaDetalle);

            DeshabilitarHabilitarBotones(false);
        }
   
        private void facturaDetalleDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) 

        {
            e.ThrowException = false; // Video 36 - Tarea 7
        }

        private void clienteIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void facturaDetalleDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var factura = (Factura)listaFacturasBindingSource.Current;
            _facturasBL.CalcularFactura(factura);

            listaFacturasBindingSource.ResetBindings(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea anular esta factura?", "Anular", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Anular(id);
                }
            }
        }

        private void Anular(int id)
        {
            var resultado = _facturasBL.AnularFactura(id);

            if (resultado == true)
            {
                listaFacturasBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al anular la factura");
            }
        }

        private void listaFacturasBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var factura = (Factura)listaFacturasBindingSource.Current;

            if (factura != null && factura.Id != 0 && factura.Activo == false)
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
        } 
    }    
    
    
} // Name Space
