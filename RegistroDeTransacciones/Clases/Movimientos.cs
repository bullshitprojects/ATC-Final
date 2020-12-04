using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDeTransacciones.Clases
{
    class Movimientos
    {
        //Declaración de Variables
        private string fecha;
        private string movimiento;
        private string nombre;
        private string concepto;
        private int cantidad;
        private double costoUnitario;
        private double total;

        private StringBuilder query = new StringBuilder();
        private Conexionbd connect;

        // Metodos de Acceso Get y Set
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Movimiento
        {
            get { return movimiento; }
            set { movimiento = value; }
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

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public double CostoUnitario
        {
            get { return costoUnitario; }
            set { costoUnitario = value; }
        }

        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        //Metodos

        //Metodo para Agregar una Persona
        public string InsertarMovimiento(string fecha, string movimiento, string nombre, string concepto, int cantidad, double costoUnitario, double total)
        {
            string salida;
            try
            {
                query = new StringBuilder();
            query.Append("INSERT INTO inventario (fecha, nombre, concepto, cantidad, costoUnitario, total, tipo) VALUES ('")
                    .Append(fecha).Append("','").Append(nombre).Append("','").Append(movimiento).Append("',").Append(cantidad).Append(", ").Append(costoUnitario).Append(", ").Append(total).Append(", 0)");

                connect = new Conexionbd();
                if (connect.executeQuery(query.ToString()))
                {
                    salida = "Movimiento Ingresado con exito";
            }
            else
                {
                salida = "Ocurrió un error al querer guardar los datos. Contacta al administrador del sistema";
                }
            }
            catch (Exception ex)
            {
                salida = "Ocurrió un error al querer guardar los datos. Detalles del error:\n" + ex.ToString();
            }
            return salida;
        }

        //Metodo para Eliminar una persona
        public string EliminarPersona(string asiento, string orden)
        {
            string salida = "Asiento Eliminado";
            try
            {
                connect = new Conexionbd();
                query.Append("DELETE FROM libro_diario WHERE telefono='").Append(asiento).Append("' AND correo ='").Append(orden).Append("'");
                connect.executeQuery(query.ToString());
            }
            catch (Exception ex)
            {
                salida = "Ocurrió un error al querer eliminar el telefono. Detalles del error:\n" + ex.ToString();
            }
            return salida;
        }
    }
}
