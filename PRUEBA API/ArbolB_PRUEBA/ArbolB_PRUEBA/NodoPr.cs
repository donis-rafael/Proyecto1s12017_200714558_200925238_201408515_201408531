using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ArbolB_PRUEBA
{
    public class NodoPr
    {
        public String id_transac;
        public String activo_rent;
        public String us;
        public String empresa;
        public String depto;
        public String fecha;
        public String t_rento;

        public int duda;

        public NodoPr(String id_transac, String activo_rent, String us, String empresa, String depto, String fecha, String t_rento)
        {
            this.id_transac = id_transac;
            this.activo_rent = activo_rent;
            this.us = us;
            this.empresa = empresa;
            this.depto = depto;
            this.fecha = fecha;
            this.t_rento = t_rento;
        }
    }
}