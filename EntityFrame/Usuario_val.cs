using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;

namespace EntityFrame
{
    public class Usuario_val
    {
        Conexion oConexion = new Conexion();
        public static string NomUsuario { get; set; }

        public void NuevoUsuario(string nomUsuario,string Contraseña,DateTime Fecha)
        {
            try
            {
                using(var scope=new TransactionScope())
                {
                    oConexion.Abrir();
                    SqlCommand cm = new SqlCommand("SP_NuevoUsuario", oConexion.conexion);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@Usuario", nomUsuario);
                    cm.Parameters.AddWithValue("@Contraseña", Contraseña);
                    cm.Parameters.AddWithValue("@FecRegistro", Fecha);
                    cm.Parameters.AddWithValue("@UsuarioCreate", cache_Usuario.NomUsuario);
                    cm.ExecuteNonQuery();
                    oConexion.Cerrar();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*
        public DataSet ListarLog()
        {
            DataSet dts = new DataSet();
            oConexion.Abrir();
            SqlCommand cm = new SqlCommand("SP_LogUsuario", oConexion.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Usuario", cache_Usuario.NomUsuario);
            cm.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dts);
            oConexion.Cerrar();
            return dts;
        }
        */
        public int ValidarUsuario(string usuario, string Contraseña)
        {
            int Stado = 0;
            try
            {
                DataSet dts = new DataSet();
                oConexion.Abrir();
                SqlCommand cm = new SqlCommand("SP_ValidarUsuario", oConexion.conexion);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@Usuario", usuario);
                cm.Parameters.AddWithValue("@Contraseña", Contraseña);
                cm.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dts);
                Stado = Convert.ToInt32(dts.Tables[0].Rows[0][0].ToString());
                cache_Usuario.NomUsuario = dts.Tables[1].Rows[0][1].ToString();
                oConexion.Cerrar();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Stado;
        }
    }
}
