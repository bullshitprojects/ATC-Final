using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDeTransacciones.Clases
{
    class Inventario
    {
        //Declaración de Variables
        private string fecha;
        private string nombre;
        private string concepto;
        private string entradas;
        private string salidas;
        private string existencias;
        private string unitario;
        private string promedio;
        private string debe;
        private string haber;
        private string saldo;


        private MySqlCommand command;
        private MySqlDataReader reader;
        private List<Inventario> movimientos;
        private StringBuilder query = new StringBuilder();
        private Conexionbd connect;

        public double promedioflotante=0;
        public double saldoflotante = 0;
        public int existenciasflotantes = 0;
        // Metodos de Acceso Get y Set
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }

        public string Entradas
        {
            get { return entradas; }
            set { entradas = value; }
        }

        public string Salidas
        {
            get { return salidas; }
            set { salidas = value; }
        }

        public string Existencias
        {
            get { return existencias; }
            set { existencias = value; }
        }
        public string Unitario
        {
            get { return unitario; }
            set { unitario = value; }
        }

        public string Promedio
        {
            get { return promedio; }
            set { promedio = value; }
        }

        public string Debe
        {
            get { return debe; }
            set { debe = value; }
        }

        public string Haber
        {
            get { return haber; }
            set { haber = value; }
        }

        public string Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }


        public List<Inventario> CargarInventario() {
            try
            {
                connect = new Conexionbd();
                movimientos = new List<Inventario>();
                query.Append("SELECT fecha, nombre, concepto, cantidad, costoUnitario, total FROM inventario");
                connect.openCon();
                command = new MySqlCommand(query.ToString(), connect.Conectarbd);
                reader = command.ExecuteReader();

               

                while (reader.Read())
                {
                 // Variables flotantes
                    Inventario Transaccion = new Inventario();
                    int entradas=0, salidas=0;
                    double debe=0, haber=0, precioUnitario=0;

                    Transaccion.Fecha = reader.GetString(0);
                    Transaccion.Nombre = reader.GetString(1);
                    Transaccion.Concepto = reader.GetString(2);
                    precioUnitario = Convert.ToDouble(reader.GetValue(4).ToString());

                    if (reader.GetString(2)== "Compra")
                    {
                        entradas= Convert.ToInt32(reader.GetValue(3).ToString());
                        salidas = 0;
                        Transaccion.Entradas = entradas.ToString();
                        Transaccion.Salidas = "";
                        debe= Convert.ToDouble(reader.GetValue(5).ToString());
                        Transaccion.Debe = "$ " +  Math.Round(debe, 2).ToString(); 
                        Transaccion.Haber = "";
                        Transaccion.unitario = "$ " + Math.Round(precioUnitario, 2).ToString();
                    }

                    if (reader.GetString(2) == "Venta")
                    {
                        salidas = Convert.ToInt32(reader.GetValue(3).ToString());
                        entradas = 0;
                        Transaccion.Entradas = "";
                        Transaccion.Salidas = salidas.ToString();
                        haber = Convert.ToDouble(reader.GetValue(5).ToString());
                        Transaccion.Haber = "$ " + Math.Round(haber, 2).ToString(); 
                        Transaccion.Debe = "";
                        Transaccion.unitario = "";
                    }

                    if (reader.GetString(2) == "Devolución")
                    {
                        entradas = Convert.ToInt32(reader.GetValue(3).ToString());
                        salidas = 0;
                        Transaccion.Entradas = entradas.ToString();
                        Transaccion.Salidas = "";
                        debe = Convert.ToDouble(reader.GetValue(5).ToString());
                        Transaccion.Debe = "$ " + Math.Round(debe, 2).ToString(); 
                        Transaccion.Haber = "";
                        Transaccion.unitario = "";
                    }                          

                    existenciasflotantes += entradas - salidas;

                    Transaccion.existencias = existenciasflotantes.ToString();


                    saldoflotante += debe - haber;

                    promedioflotante = saldoflotante / existenciasflotantes;

                    Transaccion.Promedio = "$ " + Math.Round(promedioflotante, 2).ToString();

                    Transaccion.Saldo = "$ " + Math.Round(saldoflotante, 2).ToString();

                    movimientos.Add(Transaccion);
                }

                reader.Close();
                connect.closeCon();

        }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al querer recuperar los datos. Detalles del error:\n " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return movimientos;
        }


        //Metodo para Eliminar La empresa y todos sus registros
        public string ReiniciarInvetario()
        {
            string salida = "Empresa y Todos sus registros fueron eliminados con exito";
            try
            {
                connect = new Conexionbd();
                query = new StringBuilder();
                query.Append("DELETE FROM inventario");
                connect.executeQuery(query.ToString());
        }
            catch (Exception ex)
            {
                salida = "Ocurrió un problema al eliminar los registros: \n" + ex.ToString();
            }
            connect.closeCon();
            return salida;
        }

        public string ReiniciarPersona()
        {
            string salida = "Empresa y Todos sus registros fueron eliminados con exito";
            try
            {
                connect = new Conexionbd();
                query = new StringBuilder();
                query.Append("DELETE FROM persona");
                connect.executeQuery(query.ToString());
            }
            catch (Exception ex)
            {
                salida = "Ocurrió un problema al eliminar los registros: \n" + ex.ToString();
            }
            connect.closeCon();
            return salida;
        }
    }
}
