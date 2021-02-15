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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Usuario1, Usuario2;
            string Contraseña1, Contraseña2;

         // Almacenando Valores en las Variables

            Usuario1 = textBox1.Text; // Recuerda : siempre colocar el nombre correcto de la herramienta a utilisar. (Ejemplo: textBox1)
            Contraseña1 = textBox2.Text;

            Usuario2 = textBox3.Text;
            Contraseña2 = textBox4.Text;

         // Login a Usuario 1 y 2

            if (Usuario1 == "Empleado1" && Contraseña1 == "123")
            {
                this.Close();
            }
            else
            if (Usuario2 == "Empleado2" && Contraseña2 == "123")
            {
                this.Close();
            }
            else           
            {
                MessageBox.Show("Usuario o Contraseña Incorrecta");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
