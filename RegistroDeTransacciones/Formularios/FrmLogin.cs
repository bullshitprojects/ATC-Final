using RegistroDeTransacciones.Clases;
using SistemaDePagoEmpleados;
using System;
using System.Windows.Forms;

namespace RegistroDeTransacciones.Formularios
{
    public partial class FrmLogin : Form
    {
        Empresa empresa;
        public FrmLogin()
        {
            InitializeComponent();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            empresa = new Empresa();
            if (empresa.Login(txtUser.Text, txtContra.Text) == 1)
            {
                FormPrincipal F = new FormPrincipal();
                F.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Invalida", "Fallo al inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Al registrar una nueva empresa se borrarán todos los registros actuales...\n\n¿Desea proceder?", "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                empresa = new Empresa();
                empresa.EliminarEmpresa();
                FrmAgregarEmpresa FormularioRegistro = new FrmAgregarEmpresa();
                FormularioRegistro.Show();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void txtContra_KeyDown(object sender, KeyEventArgs e)
        {
            empresa = new Empresa();
            if (e.KeyCode == Keys.Enter)
            {
                if (empresa.Login(txtUser.Text, txtContra.Text) == 1)
                {
                    FormPrincipal F = new FormPrincipal();
                    F.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Invalida\nRevisa tus credenciales o contacta al administrador del sistema", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        private void label36_Click(object sender, EventArgs e)
        {

        }
    }
}
