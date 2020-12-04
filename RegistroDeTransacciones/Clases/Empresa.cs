using System;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RegistroDeTransacciones.Clases
{
    class Empresa
    {
        //Declaración de Variables
        private string nEmpresa;
        private string nitEmpresa;
        private string razonSocial;
        private string email;
        private string usuario;
        private string contra;
        Empresa oEmpresa;
        private MySqlDataReader reader;
        private Conexionbd connect;
        private MySqlCommand command;
        private StringBuilder query = new StringBuilder();

        // Metodos de Acceso Get y Set

        public string NEmpresa
        {
            get { return nEmpresa; }
            set { nEmpresa = value; }
        }

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string NitEmpresa
        {
            get { return nitEmpresa; }
            set { nitEmpresa = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Contra
        {
            get { return contra; }
            set { contra = value; }
        }


        public Empresa CargarEmpresa()
        {
            oEmpresa = new Empresa();
            query.Append("SELECT  nombre_empresa, nit_empresa, razon_social, email, usuario, contra FROM empresa");
            connect = new Conexionbd();      
            command = new MySqlCommand(query.ToString(), connect.Conectarbd);
            connect.openCon();
            reader = command.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    oEmpresa.NEmpresa = reader.GetString(0);
                    oEmpresa.NitEmpresa = reader.GetString(1);
                    oEmpresa.RazonSocial = reader.GetString(2);
                    oEmpresa.Email = reader.GetString(3);
                    oEmpresa.Usuario = reader.GetString(4);
                    oEmpresa.Contra = reader.GetString(5);
                }
                reader.Close();
                connect.closeCon();
            }
            else
            {
                MessageBox.Show("Ocurrió un error interno del sistema, contacta al administrador para recibir soporte.");
            }

            return oEmpresa;
        }


        public string AgregarEmpresa(string empresa, string nit, string razonSocial, string email, string usuario, string contra)
        {
            string salida = "Empresa Agregada Con Exito";

            connect = new Conexionbd();

            query.Append("INSERT INTO empresa (nombre_empresa, nit_empresa, razon_social, email, usuario, contra) VALUES ('")
                .Append(empresa).Append("','").Append(nit).Append("','").Append(razonSocial).Append("','").Append(email).Append("','")
                .Append(usuario).Append("','").Append(contra).Append("')");
            if (connect.executeQuery(query.ToString()))
            {
                query.Clear();
            }
            else
            {
                query.Clear();
                salida = "Ocurrió un error registrando tu empresa\nDetalles del error: " + connect.getMessage();
            }

            return salida;
        }

        //Metodo para Modificar Empresa
        public string ModificarEmpresa(string empresa, string nit, string razonSocial, string email, string usuario, string contra)
        {
            string salida;
            connect = new Conexionbd();
            query.Append("UPDATE empresa SET nombre_empresa='").Append(empresa).Append("', nit_empresa='").Append(nit).Append("', razon_social = '").Append(razonSocial)
                .Append("', email = '").Append(email).Append("', usuario ='").Append(usuario).Append("', contra='").Append(contra).Append("'");
            if (connect.executeQuery(query.ToString()))
            {
                salida = "Empresa Modificada Con Exito";
            }
            else
            {
                salida = "Ocurrió un problema al intentar modificar la empresa: \n" + connect.getMessage() ;
            }
            return salida;
        }

        public int Login(string usuario, string contra)
        {
            int validar = 0;
            try
            {
                connect = new Conexionbd();
                connect.openCon();
                query.Append("SELECT usuario, contra FROM empresa");
                command = new MySqlCommand(query.ToString(), connect.Conectarbd);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == usuario && reader.GetString(1) == contra)
                    {
                        validar = 1;
                    }
                }
                reader.Close();
                connect.closeCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al establecer una conexión a la base de datos:\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return validar;
        }

        //Metodo para Eliminar La empresa y todos sus registros
        public string EliminarEmpresa()
        {
            string salida = "Empresa y Todos sus registros fueron eliminados con exito";
            try
            {
                connect = new Conexionbd();
                query.Append("DELETE FROM empresa");
                connect.executeQuery(query.ToString());
                query.Clear();
                query.Append("DELETE FROM libro_diario");
                connect.executeQuery(query.ToString());
            }
            catch (Exception ex)
            {
                salida = "Ocurrió un problema al eliminar los registros: \n" + ex.ToString();
            }
            return salida;
        }

    }
}
