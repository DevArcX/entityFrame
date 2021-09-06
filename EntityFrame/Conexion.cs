using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EntityFrame
{
    public class Conexion
    {
        public SqlConnection conexion;

        public void CadenaConexion()
        {
            conexion = new SqlConnection(@"
                Data Source=mssql-46601-0.cloudclusters.net,19839;
                Initial Catalog=Empresa;
                User ID=Cris;
                Password=DBSistem2020");
        }
        public void Abrir()
        {
            try
            {
                CadenaConexion();
                conexion.Open();
            }
            catch (Exception ex)
            {

            }
        }
        public void Cerrar()
        {
            conexion.Close();
        }
    }
}
