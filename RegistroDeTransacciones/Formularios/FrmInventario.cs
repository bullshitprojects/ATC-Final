using RegistroDeTransacciones.Clases;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RegistroDeTransacciones.Reportes;

namespace SistemaDePagoEmpleados
{
    public partial class Form3 : Form
    {
        Movimientos movimientos = new Movimientos();

        Inventario oInventario;
        List<Inventario> lInventario = new List<Inventario>();

        Persona oPersona;
        List<Persona> lPersona = new List<Persona>();

        public Form3()
        {
            InitializeComponent();
            cargarBase();
            CargarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMovimiento.Text=="Compra")
                {
                    MessageBox.Show(movimientos.InsertarMovimiento(txtFecha.Value.ToShortDateString(), txtMovimiento.Text, txtNombre.Text, txtConcepto.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToDouble(txtCostoUnitario.Text), Convert.ToDouble(txtTotal.Text)));

                }
                else
                {
                    MessageBox.Show(movimientos.InsertarMovimiento(txtFecha.Value.ToShortDateString(), txtMovimiento.Text, txtNombre.Text, txtConcepto.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToDouble(oInventario.promedioflotante), (Convert.ToDouble(txtCantidad.Text) * oInventario.promedioflotante)));

                }

                CargarTabla();
                txtConcepto.Text = "";
                txtCantidad.Text = "";
                txtCostoUnitario.Text = "";
                txtTotal.Text = "";
                txtNombre.Text = "";
                txtMovimiento.Text = "";

            }
            catch (Exception)
            {
                throw;
            }

}

        public void CargarTabla()
        {
            oInventario = new Inventario();
            lInventario = oInventario.CargarInventario();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lInventario;
            dataGridView1.Visible = true;

            txtPromedioFlotante.Text = "$ "+ Math.Round(oInventario.promedioflotante,2).ToString();
            txtUnidadesFlotantes.Text = oInventario.existenciasflotantes.ToString();
            txtSaldoFlotante.Text = "$ " + Math.Round(oInventario.saldoflotante,2).ToString();
        }

        public class DataGridViewUtils
        {
            public static string GetValorCelda(DataGridView dgv, int num)
            {
                try
                {
                    string valor = "";
                    valor = dgv.Rows[dgv.CurrentRow.Index].Cells[num].Value.ToString();
                    return valor;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cargarBase();
        }

        public void cargarBase() {
            txtNombre.Items.Clear();
            oPersona = new Persona();
            if (radioButton1.Checked==true)
            {
                lPersona = oPersona.CargarClientes();
            }
            else
            {
                lPersona = oPersona.CargarProveedores();
            }
            foreach (var item in lPersona)
            {
                txtNombre.Items.Add(item.Nombre);
            }
        }

        public void BotonTotal() {
            double total = 0;
            try
            {
                if (txtCostoUnitario.Text != "" & txtCantidad.Text != "")
                {
                    total = Convert.ToDouble(txtCostoUnitario.Text) * Convert.ToDouble(txtCantidad.Text);
                    txtTotal.Text = total.ToString();
                    if (total > 0)
                    {
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cargarBase();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            BotonTotal();
        }

        private void txtCostoUnitario_TextChanged(object sender, EventArgs e)
        {
            BotonTotal();
        }

        private void txtMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMovimiento.Text == "Venta" | txtMovimiento.Text == "Devolución")
            {
                txtCostoUnitario.Text = Math.Round(oInventario.promedioflotante, 2).ToString();
            }
            else
            {
                txtCostoUnitario.Text = "";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                oInventario.ReiniciarInvetario();
                oInventario.ReiniciarPersona();
                CargarTabla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtNombre_MouseClick(object sender, MouseEventArgs e)
        {
            cargarBase();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    MessageBox.Show(movimientos.InsertarMovimiento(txtFecha.Value.ToShortDateString(), txtMovimiento.Text, txtNombre.Text, txtConcepto.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToDouble(txtCostoUnitario.Text), Convert.ToDouble(txtTotal.Text)));
                    CargarTabla();
                    txtConcepto.Text = "";
                    txtCantidad.Text = "";
                    txtCostoUnitario.Text = "";
                    txtTotal.Text = "";
                    txtNombre.Text = "";
                    txtMovimiento.Text = "";

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btnImprimirInventario_Click(object sender, EventArgs e)
        {
            // Aqui ira lo del reporte
            Reporte reporte = new Reporte();
            try
            {
                reporte.PrintReport(lInventario, txtUnidadesFlotantes.Text, txtPromedioFlotante.Text, txtSaldoFlotante.Text);
                MessageBox.Show("Reporte guardado correctamente en: " + reporte.path, "Informe completado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(reporte.path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al intentar guardar el documento" + ex, "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
