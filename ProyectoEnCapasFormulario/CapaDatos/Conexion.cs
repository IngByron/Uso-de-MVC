using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; ///
using System.Data.SqlClient; ///

namespace CapaDatos
{
    public class Conexion
    {
        SqlConnection conexion = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MiWindowsFormTEMPORAL; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //Abrir Conexion
        public void abrir_conexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
        }
        public void cerrar_conexion()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }

        public string insertar(string SQL)
        {
            SqlCommand cmd;
            string salida = "Se inserto";
            string comandoSQLinsertar = SQL;
            try
            {
                //entre comillas dobles cuando es entero, cuando es varchar entre comillas simples y dentro comillas dobles.
                //Adicional, dentro de todo va + +
                cmd = new SqlCommand(comandoSQLinsertar, conexion);
                cmd.ExecuteNonQuery();
                //MessageBox.Show(salida);
            }
            catch (Exception ex)
            {
                salida = "No se inserto: " + ex.ToString();
            }

            return salida;
        }
        public string actualizar(string SQL)
        {
            string salida = "Se actualizo";
            SqlCommand cmd;
            string comandoSQLactualizar = SQL;
            try
            {
                //entre comillas dobles cuando es entero, cuando es varchar entre comillas simples y dentro comillas dobles.
                //Adicional, dentro de todo va + +
                cmd = new SqlCommand(comandoSQLactualizar, conexion);
                cmd.ExecuteNonQuery();
                //MessageBox.Show(salida);
            }
            catch (Exception ex)
            {
                salida = "No se actualizo: " + ex.ToString();
            }

            return salida;
        }
        public int productoRegistrado(int codigo)
        {
            SqlCommand cmd;
            SqlDataReader datareader;
            string salida = "Registrado satisfactoriamente";
            string comandoSQLconsultarRegistro = "Select * from dbProductList where codigo=" + codigo + "";
            int contador = 0;
            try
            {
                //entre comillas dobles cuando es entero, cuando es varchar entre comillas simples y dentro comillas dobles.
                //Adicional, dentro de todo va + +
                cmd = new SqlCommand(comandoSQLconsultarRegistro, conexion);
                datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    contador++;
                }
                datareader.Close();
            }
            catch (Exception ex)
            {
                salida = "No se pudo consultar:" + ex.ToString();
                Console.WriteLine(salida);
            }
            return contador;

        }
        public class DataGridView
        {
            public DataTable DataSource { get; internal set; }
        }

        public DataTable cargar()
        {
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            try
            {
                da = new SqlDataAdapter("Select * from dbProductList", conexion);
                da.Fill(dt);
                //dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR de Carga en DataGridView: "+ex);
                //MessageBox.Show("No se pudo llenar: " + ex.ToString());
            }
            return dt;
        }



        //Procedimientos para insert, update, delete
        public void Ejecutar_SP(String NombreSp, List<Parametros> lst)
        {
            SqlCommand cmd;
            try
            {
                abrir_conexion();
                cmd = new SqlCommand(NombreSp, conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    for(int i=0; i < lst.Count; i++)
                    {
                        if (lst[i].Direccion == ParameterDirection.Input)
                        {
                            cmd.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                        }
                        if (lst[i].Direccion == ParameterDirection.Output)
                        {
                            cmd.Parameters.Add(lst[i].Nombre, lst[i].TipoDato, lst[i].Tamano).Direction = ParameterDirection.Output;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    //Recuperar parametros de salida
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                        {
                            lst[i].Valor = cmd.Parameters[i].Value.ToString();
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            cerrar_conexion();
        }
        //Listados Consultas
        public DataTable Listado(string NombreSp, List<Parametros> lst)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            try
            {
                da = new SqlDataAdapter(NombreSp, conexion);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    for(int i=0; i < lst.Count; i++)
                    {
                        da.SelectCommand.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                    }
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
