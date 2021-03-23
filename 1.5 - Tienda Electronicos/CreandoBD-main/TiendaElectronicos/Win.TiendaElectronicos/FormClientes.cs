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
    public partial class FormClientes : Form
    {

        ClientesBL _clientesBL;       

        public FormClientes()

        {
            InitializeComponent();

            _clientesBL = new ClientesBL();      
            listaClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();
        }

       
        private void clienteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaClientesBindingSource.EndEdit();
            var cliente = (Cliente)listaClientesBindingSource.Current;

            var respuesta = _clientesBL.GuardarCliente(cliente);

            if (respuesta.Exitoso == true)
            {
                listaClientesBindingSource.ResetBindings(false);
                DesahibilarHabilitar(true);
                MessageBox.Show("Cliente guardado");
            }
            else
            {
                MessageBox.Show(respuesta.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clientesBL.AgregarCliente();
            listaClientesBindingSource.MoveLast();

            DesahibilarHabilitar(false);
        }

        private void DesahibilarHabilitar(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled    = valor;
            bindingNavigatorMoveLastItem.Enabled     = valor;
            bindingNavigatorMoveNextItem.Enabled     = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled     = valor;

            bindingNavigatorAddNewItem.Enabled       = valor;
            bindingNavigatorDeleteItem.Enabled       = valor;

            toolStripButtonCancelar.Visible          = !valor;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if(idTextBox.Text != "")
            {
                var respuesta = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }
            }
        }

        private void Eliminar(int Id)
        {
            var Respuesta = _clientesBL.EliminarCliente(Id);

            if (Respuesta == true)
            {
                listaClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el Cliente");
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            _clientesBL.CancelarCambios();
            DesahibilarHabilitar(true);
            
        }
    }
}
