using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbolB_PRUEBA
{
    public class Bnodo
    {

        public Bnodo Rama0;
        public Bnodo Rama1;
        public Bnodo Rama2;
        public Bnodo Rama3;
        public Bnodo Rama4;

        public NodoPr Clave0;
        public NodoPr Clave1;
        public NodoPr Clave2;
        public NodoPr Clave3;

        //public NodoOrtogonal nodoOrto;

        public int Cuentas = 0;

        public Bnodo(NodoPr clave)
        {
            this.Clave0 = clave;
        }

        public Bnodo()
        {
            this.Rama0 = null;
            this.Rama1 = null;
            this.Rama2 = null;
            this.Rama3 = null;
            this.Rama4 = null;

            this.Clave0 = null;
            this.Clave1 = null;
            this.Clave2 = null;
            this.Clave3 = null;

            //this.nodoOrto = null;
        }

    }
}