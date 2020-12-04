using RegistroDeTransacciones.Clases;
using System;
using System.Windows.Forms;

namespace SistemaDePagoEmpleados
{
    public partial class Form2 : Form
    {
        Empresa oEmpresa;
        public Form2()
        {
            InitializeComponent();
            CargarDatos();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            oEmpresa = new Empresa();
            string Mensaje = oEmpresa.EliminarEmpresa();
            MessageBox.Show(Mensaje);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            oEmpresa = new Empresa();
            string Mensaje = oEmpresa.ModificarEmpresa(txtNombre.Text, txtNit.Text, txtRazonSocial.Text, txtEmail.Text, txtUser.Text, txtContra.Text);
            MessageBox.Show(Mensaje);
            this.Close();
        }

        public void CargarDatos() {
            oEmpresa = new Empresa();
            oEmpresa = oEmpresa.CargarEmpresa();
            txtNombre.Text = oEmpresa.NEmpresa;
            txtNit.Text = oEmpresa.NitEmpresa;
            txtRazonSocial.Text = oEmpresa.RazonSocial;
            txtEmail.Text = oEmpresa.Email;
            txtUser.Text = oEmpresa.Usuario;
            txtContra.Text = oEmpresa.Contra;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
