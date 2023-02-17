using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos; //usando CapaDatos

namespace CapaLogica
{
    public class ListaProducto
    {
        //Atributos
        public int codigo;
        public string nombre;
        public double precio;

        Conexion M = new Conexion(); // llamando a la clase Conexion de la CapaDatos
        public void conectarDB()
        {
            M.abrir_conexion();
        }
        public void desconectarDB()
        {
            M.cerrar_conexion();
        }
        public void insertarRegistro(int codigo, string nombre, double precio) 
        {
            conectarDB();
        
            if(M.productoRegistrado(codigo) == 0)
            {
                M.insertar("Insert into dbProductList(codigo, nombre, precio) values (" + codigo + ",'" + nombre + "'," + precio + ")");
            }
            else{
                Console.WriteLine("No se pudo insertar registro");
            }
            desconectarDB();
            
        }

        public void actualizarRegistro(int codigo, string nombre, double precio)
        {
            conectarDB();
            M.actualizar("Update dbProductList set codigo=" + codigo + ",nombre='" + nombre + "',precio=" + precio + " where codigo=" + codigo + "");
            desconectarDB();
        }
        
        public DataTable cargarRegistros()
        {
            return M.cargar();
            //return M.cargar();
        }








        //Registrar Producto
        public string Registrar_Productos()
        {
            string msj = "";
            List<Parametros> lst = new List<Parametros>();
            try
            {
                //parametros de entrada
                lst.Add(new Parametros("codigo", codigo));
                lst.Add(new Parametros("nombre", nombre));
                lst.Add(new Parametros("precio", precio));
            }
            //parametros de salida
            
            catch (Exception ex)
            {
                throw ex;
            }

            return msj;
        }

    }
}
