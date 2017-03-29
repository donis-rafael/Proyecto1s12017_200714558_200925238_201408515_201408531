using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbolB_PRUEBA
{
    public class articulos
    {
        public string id_transaccion { get; set; }
        public string id_articulo { get; set; }
        public string usuario { get; set; }
        public string empresa { get; set; }
        public string departamento { get; set; }
        public string fecha { get; set; }
        public string tiempo { get; set; }

        public articulos(string idT, string idA, string usuario, string empresa, string departamento, string fecha, string tiempo)
        {
            this.id_transaccion = idT;
            this.id_articulo = idA;
            this.usuario = usuario;
            this.empresa = empresa;
            this.departamento = departamento;
            this.fecha = fecha;
            this.tiempo = tiempo;
        }

    }
}