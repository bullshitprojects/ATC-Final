using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDeTransacciones.Clases
{
    class Persona
    {
        //Declaración de Variables
        private string tipo;
        private string nombre;
        private string descripcion;
        private string telefono;
        private string correo;


        private MySqlCommand command;
        private MySqlDataReader reader;
        private List<Persona> clientes;
        private List<Persona> proveedores;
        private StringBuilder query = new StringBuilder();
        private Conexionbd connect;

        // Metodos de Acceso Get y Set
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        //Metodo obtener los clientes
        public List<Persona> CargarClientes()
        {
            try
            {
                connect = new Conexionbd();
                clientes = new List<Persona>();
                query.Append("SELECT tipo, nombre, descripcion, telefono, correo FROM persona WHERE tipo='Cliente' ORDER BY nombre");
                connect.openCon();
                command = new MySqlCommand(query.ToString(), connect.Conectarbd);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Persona Transaccion = new Persona();
                    Transaccion.Tipo = reader.GetString(0);
                    Transaccion.Nombre = reader.GetString(1);
                    Transaccion.Descripcion = reader.GetString(2);
                    Transaccion.Telefono = reader.GetString(3);
                    Transaccion.Correo = reader.GetString(4);
                    clientes.Add(Transaccion);
                }
                reader.Close();
                connect.closeCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al querer recuperar los datos. Detalles del error:\n " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return clientes;
        }

        //Metodo obtener los clientes
        public List<Persona> CargarProveedores()
        {
            try
            {
                connect = new Conexionbd();
                proveedores = new List<Persona>();
                query.Append("SELECT tipo, nombre, descripcion, telefono, correo FROM persona WHERE tipo='Proveedor' ORDER BY nombre");
                connect.openCon();
                command = new MySqlCommand(query.ToString(), connect.Conectarbd);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Persona Transaccion = new Persona();
                    Transaccion.Tipo = reader.GetString(0);
                    Transaccion.Nombre = reader.GetString(1);
                    Transaccion.Descripcion = reader.GetString(2);
                    Transaccion.Telefono = reader.GetString(3);
                    Transaccion.Correo = reader.GetString(4);
                    proveedores.Add(Transaccion);
                }
                reader.Close();
                connect.closeCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al querer recuperar los datos. Detalles del error:\n " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return proveedores;
        }

        //Metodo para Agregar una Persona
        public string InsertarPersona(string tipo, string nombre, string descripcion, string telefono, string correo)
        {
            string salida;
            try
            {
                //ojo
                query.Append("INSERT INTO persona (tipo, nombre, descripcion, telefono, correo) VALUES ('")
                    .Append(tipo).Append("','").Append(nombre).Append("','").Append(descripcion).Append("','").Append(telefono).Append("','").Append(correo).Append("')");

                connect = new Conexionbd();
                if (connect.executeQuery(query.ToString()))
                {
                    salida = "Asiento Insertado Con Exito";
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
