using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; ///////SQL

namespace CapaDatos
{
    public class Parametros
    {
        public string Nombre { get; set; }
        public Object Valor { get; set; }
        public SqlDbType TipoDato { get; set; }
        public Int32 Tamano { get; set; }
        public ParameterDirection Direccion { get; set; }
        
        public Parametros(string objnombre, Object objvalor)
        {
            Nombre = objnombre;
            Valor = objvalor;
            Direccion = ParameterDirection.Input;

        }
        public Parametros(string objnombre, SqlDbType objTipoDato, Int32 objTamano)
        {
            Nombre = objnombre;
            TipoDato = objTipoDato;
            Direccion = ParameterDirection.Output;
        }
    }
}
