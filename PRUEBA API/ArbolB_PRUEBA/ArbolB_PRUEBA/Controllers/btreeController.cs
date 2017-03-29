using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ArbolB_PRUEBA.Controllers
{
    public class btreeController : ApiController
    {
        public static Btree arbolB = new Btree();
        public static List<articulos> artis = new List<articulos>();

        public string GET()
        {
            return "FUNCIONA";
        }

        public string GET(int a)
        {
            return "numero = " + a;
        }

        public string GETgraficar(int imagen)
        {
            Graficar_Arbol_B graf = new Graficar_Arbol_B();
            graf.Graficar_File(arbolB.p);
            return "se grafico";
        }

        public string GET(string cadena)
        {
            string[] a = cadena.Split(',');
            string id_transaccion = a[0];
            string id_articulo = a[1];
            string usuario = a[2];
            string empresa = a[3];
            string departamento = a[4];
            string fecha_renta = a[5];
            string timpo_renta = a[6];

            arbolB.Inserta(new NodoPr(id_transaccion, id_articulo, usuario, empresa, departamento, fecha_renta, timpo_renta));
            artis.Add(new articulos(id_transaccion, id_articulo, usuario, empresa, departamento, fecha_renta, timpo_renta));

            return "se guardon en B";
        }

        public string GETeliminar(string elimina)
        {
            string[] a = elimina.Split(',');
            string id_articulo = a[0];
            string usuario = a[1];
            string empresa = a[2];
            string departamento = a[3];
            string id_transaccion = "";
            string fecha_renta = "";
            string timpo_renta = "";
            for (int i=0; i<artis.Count; i++)
            {
                if((id_articulo.Equals(artis[i].id_articulo)) && (usuario.Equals(artis[i].usuario)) && (empresa.Equals(artis[i].empresa)) && (departamento.Equals(artis[i].departamento)))
                {
                    id_transaccion = artis[i].id_transaccion;
                    fecha_renta = artis[i].fecha;
                    timpo_renta = artis[i].tiempo;
                    i = artis.Count;

                    artis.Remove(artis[i]);
                }
            }

            arbolB.Eliminar(new NodoPr(id_transaccion, id_articulo, usuario, empresa, departamento, fecha_renta, timpo_renta));

            return "se elimino en B";
        }

        public string GETactivos(string activos)
        {
            Graficar_Arbol_B graf = new Graficar_Arbol_B();
            string[] a = activos.Split(',');
            string usuario = a[0];
            string empresa = a[1];
            string departamento = a[2];
            StringBuilder ahiVa = new StringBuilder();
            List<string> lista = graf.Graficar_FileII(usuario, empresa, departamento, arbolB.p);

            foreach(string item in lista)
            {
                ahiVa.Append(item + "\\n");
            }

            return ahiVa.ToString();
        }
    }
}
