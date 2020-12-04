using RegistroDeTransacciones;
using RegistroDeTransacciones.Clases;
using RegistroDeTransacciones.Formularios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaDePagoEmpleados
{
    public partial class Form1 : Form
    {
        Persona persona = new Persona();
        public Form1()
        {
            InitializeComponent();
        }

        public void Limpiar()
        {
            txtTipo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                persona = new Persona();
                persona.InsertarPersona(txtTipo.Text, txtNombre.Text, txtDescripcion.Text, txtTelefono.Text, txtCorreo.Text);
                MessageBox.Show(txtTipo.Text + " Ingresado con exito");
                Limpiar();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    }

