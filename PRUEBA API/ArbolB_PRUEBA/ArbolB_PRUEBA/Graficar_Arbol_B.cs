using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ArbolB_PRUEBA
{
    public class Graficar_Arbol_B
    {
        public static List<string> lista;
        public string desktop;
        private System.Text.StringBuilder graphivz;
        private int contador;
        String ruta_file = "";

        int val = 0;

        public Graficar_Arbol_B()
        {
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            this.GenerarRuta();
        }

        public void GenerarRuta()
        {
            String desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            List<String> rutas = new List<String>();
            rutas.Add(desktop + "\\PRUEBA API\\ArbolB_PRUEBA\\ArbolB_PRUEBA");
            foreach (String item in rutas)
            {
                if (!System.IO.Directory.Exists(item))
                {
                    System.IO.DirectoryInfo dir = System.IO.Directory.CreateDirectory(item);
                }
            }
        }

        public void Graficar_File(Bnodo nodo)
        {
            graphivz = new System.Text.StringBuilder();
            //contador = 0;
            String rdot = desktop + "\\PRUEBA API\\ArbolB_PRUEBA\\ArbolB_PRUEBA\\Arbol_B.dot";
            String rpng = desktop + "\\PRUEBA API\\ArbolB_PRUEBA\\ArbolB_PRUEBA\\Arbol_B.png";
            ruta_file = rpng;
            graphivz.Append("\ndigraph G {\r\n node [shape=record] ;\n");
            //\ngraph [rankdir = \"LR\"]
            //TablaHash_Grafo(tabla, val_n);
            Graficar_B(nodo);
            graphivz.Append("}");
            this.GeneradorPng(rdot, rpng);
        }

        public void Graficar_B(Bnodo nodo)
        {
            int k = 0;
            int c = 0;

            graphivz.Append("Nodo").Append(val).Append("[label=\"<P0>");
            while (c < 4)
            {
                if (c == 0)
                {
                    if (nodo.Clave0 == null)
                        break;
                }
                else if (c == 1)
                {
                    if (nodo.Clave1 == null)
                        break;
                }
                else if (c == 2)
                {
                    if (nodo.Clave2 == null)
                        break;
                }
                else if (c == 3)
                {
                    if (nodo.Clave3 == null)
                        break;
                }
                switch (c)
                {
                    case 0:
                        graphivz.Append("|").Append(nodo.Clave0.id_transac).AppendLine("\\n" + nodo.Clave0.activo_rent);
                        graphivz.AppendLine("\\n" + nodo.Clave0.us).AppendLine("\\n" + nodo.Clave0.empresa).AppendLine("\\n" + nodo.Clave0.depto);
                        graphivz.AppendLine("\\n" + nodo.Clave0.fecha).AppendLine("\\n" + nodo.Clave0.t_rento);
                        graphivz.Append("|<P").Append(c + 1).Append(">");
                        break;
                    case 1:
                        graphivz.Append("|").Append(nodo.Clave1.id_transac).AppendLine("\\n" + nodo.Clave1.activo_rent);
                        graphivz.AppendLine("\\n" + nodo.Clave1.us).AppendLine("\\n" + nodo.Clave1.empresa).AppendLine("\\n" + nodo.Clave1.depto);
                        graphivz.AppendLine("\\n" + nodo.Clave1.fecha).AppendLine("\\n" + nodo.Clave1.t_rento);
                        graphivz.Append("|<P").Append(c + 1).Append(">");
                        break;
                    case 2:
                        graphivz.Append("|").Append(nodo.Clave2.id_transac).AppendLine("\\n" + nodo.Clave2.activo_rent);
                        graphivz.AppendLine("\\n" + nodo.Clave2.us).AppendLine("\\n" + nodo.Clave2.empresa).AppendLine("\\n" + nodo.Clave2.depto);
                        graphivz.AppendLine("\\n" + nodo.Clave2.fecha).AppendLine("\\n" + nodo.Clave2.t_rento);
                        graphivz.Append("|<P").Append(c + 1).Append(">");
                        break;
                    case 3:
                        graphivz.Append("|").Append(nodo.Clave3.id_transac).AppendLine("\\n" + nodo.Clave3.activo_rent);
                        graphivz.AppendLine("\\n" + nodo.Clave3.us).AppendLine("\\n" + nodo.Clave3.empresa).AppendLine("\\n" + nodo.Clave3.depto);
                        graphivz.AppendLine("\\n" + nodo.Clave3.fecha).AppendLine("\\n" + nodo.Clave3.t_rento);
                        graphivz.Append("|<P").Append(c + 1).Append(">");
                        break;
                }
                c++;
            }
            //
            graphivz.Append("\"];\n");
            String pasa = "Nodo" + val;

            while (k < 5 && nodo.Cuentas >= k)
            {
                if (k == 0)
                {
                    if (nodo.Rama0 == null)
                        return;
                    if (nodo.Rama0.Cuentas == 0)
                        return;
                }
                else if (k == 1)
                {
                    if (nodo.Rama1 == null)
                        return;
                    if (nodo.Rama1.Cuentas == 0)
                        return;
                }
                else if (k == 2)
                {
                    if (nodo.Rama2 == null)
                        return;
                    if (nodo.Rama2.Cuentas == 0)
                        return;
                }
                else if (k == 3)
                {
                    if (nodo.Rama3 == null)
                        return;
                    if (nodo.Rama3.Cuentas == 0)
                        return;
                }
                else if (k == 4)
                {
                    if (nodo.Rama4 == null)
                        return;
                    if (nodo.Rama4.Cuentas == 0)
                        return;
                }

                val++;
                graphivz.Append(pasa).Append(":P").Append(k).Append(" -> " + "Nodo").Append(val).Append(";\n");
                switch (k)
                {
                    case 0:
                        RecursivoGrafica(nodo.Rama0);
                        break;
                    case 1:
                        RecursivoGrafica(nodo.Rama1);
                        break;
                    case 2:
                        RecursivoGrafica(nodo.Rama2);
                        break;
                    case 3:
                        RecursivoGrafica(nodo.Rama3);
                        break;
                    case 4:
                        RecursivoGrafica(nodo.Rama4);
                        break;
                }
                k++;
            }
        }

        void RecursivoGrafica(Bnodo nodo)
        {
            int k = 0;
            int c = 0;
            graphivz.Append("Nodo").Append(val).Append("[label=\"<P0>");

            while (c < 4)
            {
                if (c != nodo.Cuentas && nodo.Cuentas != 0)
                {
                    if (c == 0)
                    {
                        if (nodo.Clave0 == null)
                            break;
                    }
                    else if (c == 1)
                    {
                        if (nodo.Clave1 == null)
                            break;
                    }
                    else if (c == 2)
                    {
                        if (nodo.Clave2 == null)
                            break;
                    }
                    else if (c == 3)
                    {
                        if (nodo.Clave3 == null)
                            break;
                    }
                    switch (c)
                    {
                        case 0:
                            graphivz.Append("|").Append(nodo.Clave0.id_transac).AppendLine("\\n" + nodo.Clave0.activo_rent);
                            graphivz.AppendLine("\\n" + nodo.Clave0.us).AppendLine("\\n" + nodo.Clave0.empresa).AppendLine("\\n" + nodo.Clave0.depto);
                            graphivz.AppendLine("\\n" + nodo.Clave0.fecha).AppendLine("\\n" + nodo.Clave0.t_rento);
                            graphivz.Append("|<P").Append(c + 1).Append(">");
                            break;
                        case 1:
                            graphivz.Append("|").Append(nodo.Clave1.id_transac).AppendLine("\\n" + nodo.Clave1.activo_rent);
                            graphivz.AppendLine("\\n" + nodo.Clave1.us).AppendLine("\\n" + nodo.Clave1.empresa).AppendLine("\\n" + nodo.Clave1.depto);
                            graphivz.AppendLine("\\n" + nodo.Clave1.fecha).AppendLine("\\n" + nodo.Clave1.t_rento);
                            graphivz.Append("|<P").Append(c + 1).Append(">");
                            break;
                        case 2:
                            graphivz.Append("|").Append(nodo.Clave2.id_transac).AppendLine("\\n" + nodo.Clave2.activo_rent);
                            graphivz.AppendLine("\\n" + nodo.Clave2.us).AppendLine("\\n" + nodo.Clave2.empresa).AppendLine("\\n" + nodo.Clave2.depto);
                            graphivz.AppendLine("\\n" + nodo.Clave2.fecha).AppendLine("\\n" + nodo.Clave2.t_rento);
                            graphivz.Append("|<P").Append(c + 1).Append(">");
                            break;
                        case 3:
                            graphivz.Append("|").Append(nodo.Clave3.id_transac).AppendLine("\\n" + nodo.Clave3.activo_rent);
                            graphivz.AppendLine("\\n" + nodo.Clave3.us).AppendLine("\\n" + nodo.Clave3.empresa).AppendLine("\\n" + nodo.Clave3.depto);
                            graphivz.AppendLine("\\n" + nodo.Clave3.fecha).AppendLine("\\n" + nodo.Clave3.t_rento);
                            graphivz.Append("|<P").Append(c + 1).Append(">");
                            break;
                    }
                    c++;
                }
                else
                {
                    break;
                }
            }
            //
            graphivz.Append("\"];\n");
            String pasa = "Nodo" + val;

            while (k < 5 && nodo.Cuentas >= k)
            {
                if (k == 0)
                {
                    if (nodo.Rama0 == null)
                        return;
                    if (nodo.Rama0.Cuentas == 0)
                        return;
                }
                else if (k == 1)
                {
                    if (nodo.Rama1 == null)
                        return;
                    if (nodo.Rama1.Cuentas == 0)
                        return;
                }
                else if (k == 2)
                {
                    if (nodo.Rama2 == null)
                        return;
                    if (nodo.Rama2.Cuentas == 0)
                        return;
                }
                else if (k == 3)
                {
                    if (nodo.Rama3 == null)
                        return;
                    if (nodo.Rama3.Cuentas == 0)
                        return;
                }
                else if (k == 4)
                {
                    if (nodo.Rama4 == null)
                        return;
                    if (nodo.Rama4.Cuentas == 0)
                        return;
                }

                val++;
                graphivz.Append(pasa).Append(":P").Append(k).Append(" -> " + "Nodo").Append(val).Append(";\n");
                switch (k)
                {
                    case 0:
                        RecursivoGrafica(nodo.Rama0);
                        break;
                    case 1:
                        RecursivoGrafica(nodo.Rama1);
                        break;
                    case 2:
                        RecursivoGrafica(nodo.Rama2);
                        break;
                    case 3:
                        RecursivoGrafica(nodo.Rama3);
                        break;
                    case 4:
                        RecursivoGrafica(nodo.Rama4);
                        break;
                }
                k++;
            }

        }



        int nodeB = 0;
        public List<string> Graficar_FileII(String usuario, String empresa, String dpto, Bnodo nodo)
        {
            lista = new List<string>();
            nodeB = 0;
            graphivz = new System.Text.StringBuilder();
            //contador = 0;
            String rdot = desktop + "\\PRUEBA API\\ArbolB_PRUEBA\\ArbolB_PRUEBA\\Arbol_B.dot";
            String rpng = desktop + "\\PRUEBA API\\ArbolB_PRUEBA\\ArbolB_PRUEBA\\Arbol_B.png";
            ruta_file = rpng;
            graphivz.Append("\ndigraph G {\r\n node [shape=record] ;\n");
            GraficarListadoB(nodo, usuario, empresa, dpto);
            graphivz.Append("}");
            this.GeneradorPng(rdot, rpng);

            return lista;
        }

        public void GraficarListadoB(Bnodo nodo, String usuario, String empresa, String dpto)
        {
            int k = 0;
            int c = 0;

            while (c < 4)
            {
                if (c == 0)
                {
                    if (nodo.Clave0 == null)
                        break;
                }
                else if (c == 1)
                {
                    if (nodo.Clave1 == null)
                        break;
                }
                else if (c == 2)
                {
                    if (nodo.Clave2 == null)
                        break;
                }
                else if (c == 3)
                {
                    if (nodo.Clave3 == null)
                        break;
                }
                switch (c)
                {
                    case 0:
                        if (usuario.Equals(nodo.Clave0.us) && empresa.Equals(nodo.Clave0.empresa) && dpto.Equals(nodo.Clave0.depto))
                        {
                            graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave0.id_transac).AppendLine(nodo.Clave0.activo_rent);
                            graphivz.AppendLine(nodo.Clave0.us).AppendLine(nodo.Clave0.empresa).AppendLine(nodo.Clave0.depto);
                            graphivz.AppendLine(nodo.Clave0.fecha).AppendLine(nodo.Clave0.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                            lista.Add(nodo.Clave0.id_transac + "," + nodo.Clave0.activo_rent + "," + nodo.Clave0.t_rento);
                            nodeB++;
                        }
                        break;
                    case 1:
                        if (usuario.Equals(nodo.Clave1.us) && empresa.Equals(nodo.Clave1.empresa) && dpto.Equals(nodo.Clave1.depto))
                        {
                            graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave1.id_transac).AppendLine(nodo.Clave1.activo_rent);
                            graphivz.AppendLine(nodo.Clave1.us).AppendLine(nodo.Clave1.empresa).AppendLine(nodo.Clave1.depto);
                            graphivz.AppendLine(nodo.Clave1.fecha).AppendLine(nodo.Clave1.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                            lista.Add(nodo.Clave1.id_transac + "," + nodo.Clave1.activo_rent + "," + nodo.Clave1.t_rento);
                            nodeB++;
                        }
                        break;
                    case 2:
                        if (usuario.Equals(nodo.Clave2.us) && empresa.Equals(nodo.Clave2.empresa) && dpto.Equals(nodo.Clave2.depto))
                        {
                            graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave2.id_transac).AppendLine(nodo.Clave2.activo_rent);
                            graphivz.AppendLine(nodo.Clave2.us).AppendLine(nodo.Clave2.empresa).AppendLine(nodo.Clave2.depto);
                            graphivz.AppendLine(nodo.Clave2.fecha).AppendLine(nodo.Clave2.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                            lista.Add(nodo.Clave2.id_transac + "," + nodo.Clave2.activo_rent + "," + nodo.Clave2.t_rento);
                            nodeB++;
                        }
                        break;
                    case 3:
                        if (usuario.Equals(nodo.Clave3.us) && empresa.Equals(nodo.Clave3.empresa) && dpto.Equals(nodo.Clave3.depto))
                        {
                            graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave3.id_transac).AppendLine(nodo.Clave3.activo_rent);
                            graphivz.AppendLine(nodo.Clave3.us).AppendLine(nodo.Clave3.empresa).AppendLine(nodo.Clave3.depto);
                            graphivz.AppendLine(nodo.Clave3.fecha).AppendLine(nodo.Clave3.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                            lista.Add(nodo.Clave3.id_transac + "," + nodo.Clave3.activo_rent + "," + nodo.Clave3.t_rento);
                            nodeB++;
                        }
                        break;
                }
                c++;
            }
            //
            String pasa = "Nodo" + val;

            while (k < 5 && nodo.Cuentas >= k)
            {
                if (k == 0)
                {
                    if (nodo.Rama0 == null)
                        return;
                    if (nodo.Rama0.Cuentas == 0)
                        return;
                }
                else if (k == 1)
                {
                    if (nodo.Rama1 == null)
                        return;
                    if (nodo.Rama1.Cuentas == 0)
                        return;
                }
                else if (k == 2)
                {
                    if (nodo.Rama2 == null)
                        return;
                    if (nodo.Rama2.Cuentas == 0)
                        return;
                }
                else if (k == 3)
                {
                    if (nodo.Rama3 == null)
                        return;
                    if (nodo.Rama3.Cuentas == 0)
                        return;
                }
                else if (k == 4)
                {
                    if (nodo.Rama4 == null)
                        return;
                    if (nodo.Rama4.Cuentas == 0)
                        return;
                }

                val++;
                switch (k)
                {
                    case 0:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama0);
                        break;
                    case 1:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama1);
                        break;
                    case 2:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama2);
                        break;
                    case 3:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama3);
                        break;
                    case 4:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama4);
                        break;
                }
                k++;
            }
        }

        void RecursivoListado(String usuario, String empresa, String dpto, Bnodo nodo)
        {
            int k = 0;
            int c = 0;

            while (c < 4)
            {
                if (c != nodo.Cuentas && nodo.Cuentas != 0)
                {
                    if (c == 0)
                    {
                        if (nodo.Clave0 == null)
                            break;
                    }
                    else if (c == 1)
                    {
                        if (nodo.Clave1 == null)
                            break;
                    }
                    else if (c == 2)
                    {
                        if (nodo.Clave2 == null)
                            break;
                    }
                    else if (c == 3)
                    {
                        if (nodo.Clave3 == null)
                            break;
                    }
                    switch (c)
                    {
                        case 0:
                            if (usuario.Equals(nodo.Clave0.us) && empresa.Equals(nodo.Clave0.empresa) && dpto.Equals(nodo.Clave0.depto))
                            {
                                graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave0.id_transac).AppendLine(nodo.Clave0.activo_rent);
                                graphivz.AppendLine(nodo.Clave0.us).AppendLine(nodo.Clave0.empresa).AppendLine(nodo.Clave0.depto);
                                graphivz.AppendLine(nodo.Clave0.fecha).AppendLine(nodo.Clave0.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                                lista.Add(nodo.Clave0.id_transac + "," + nodo.Clave0.activo_rent + "," + nodo.Clave0.t_rento);
                                nodeB++;
                            }
                            break;
                        case 1:
                            if (usuario.Equals(nodo.Clave1.us) && empresa.Equals(nodo.Clave1.empresa) && dpto.Equals(nodo.Clave1.depto))
                            {
                                graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave1.id_transac).AppendLine(nodo.Clave1.activo_rent);
                                graphivz.AppendLine(nodo.Clave1.us).AppendLine(nodo.Clave1.empresa).AppendLine(nodo.Clave1.depto);
                                graphivz.AppendLine(nodo.Clave1.fecha).AppendLine(nodo.Clave1.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                                lista.Add(nodo.Clave1.id_transac + "," + nodo.Clave1.activo_rent + "," + nodo.Clave1.t_rento);
                                nodeB++;
                            }
                            break;
                        case 2:
                            if (usuario.Equals(nodo.Clave2.us) && empresa.Equals(nodo.Clave2.empresa) && dpto.Equals(nodo.Clave2.depto))
                            {
                                graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave2.id_transac).AppendLine(nodo.Clave2.activo_rent);
                                graphivz.AppendLine(nodo.Clave2.us).AppendLine(nodo.Clave2.empresa).AppendLine(nodo.Clave2.depto);
                                graphivz.AppendLine(nodo.Clave2.fecha).AppendLine(nodo.Clave2.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                                lista.Add(nodo.Clave2.id_transac + "," + nodo.Clave2.activo_rent + "," + nodo.Clave2.t_rento);
                                nodeB++;
                            }
                            break;
                        case 3:
                            if (usuario.Equals(nodo.Clave3.us) && empresa.Equals(nodo.Clave3.empresa) && dpto.Equals(nodo.Clave3.depto))
                            {
                                graphivz.Append("Node").Append(nodeB.ToString()).Append("[label=\"").AppendLine(nodo.Clave3.id_transac).AppendLine(nodo.Clave3.activo_rent);
                                graphivz.AppendLine(nodo.Clave3.us).AppendLine(nodo.Clave3.empresa).AppendLine(nodo.Clave3.depto);
                                graphivz.AppendLine(nodo.Clave3.fecha).AppendLine(nodo.Clave3.t_rento).AppendLine("\", shape=\"box\", style=filled ];");
                                lista.Add(nodo.Clave3.id_transac + "," + nodo.Clave3.activo_rent + "," + nodo.Clave3.t_rento);
                                nodeB++;
                            }
                            break;
                    }
                    c++;
                }
                else
                {
                    break;
                }
            }
            //
            while (k < 5 && nodo.Cuentas >= k)
            {
                if (k == 0)
                {
                    if (nodo.Rama0 == null)
                        return;
                    if (nodo.Rama0.Cuentas == 0)
                        return;
                }
                else if (k == 1)
                {
                    if (nodo.Rama1 == null)
                        return;
                    if (nodo.Rama1.Cuentas == 0)
                        return;
                }
                else if (k == 2)
                {
                    if (nodo.Rama2 == null)
                        return;
                    if (nodo.Rama2.Cuentas == 0)
                        return;
                }
                else if (k == 3)
                {
                    if (nodo.Rama3 == null)
                        return;
                    if (nodo.Rama3.Cuentas == 0)
                        return;
                }
                else if (k == 4)
                {
                    if (nodo.Rama4 == null)
                        return;
                    if (nodo.Rama4.Cuentas == 0)
                        return;
                }

                val++;
                switch (k)
                {
                    case 0:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama0);
                        break;
                    case 1:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama1);
                        break;
                    case 2:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama2);
                        break;
                    case 3:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama3);
                        break;
                    case 4:
                        RecursivoListado(usuario, empresa, dpto, nodo.Rama4);
                        break;
                }
                k++;
            }

        }






        private void GeneradorPng(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, graphivz.ToString());

            String comandodot = "dot -Tpng " + "\"" + rdot + "\"" + " -o " + "\"" + rpng + "\"" ;


            var command = string.Format(comandodot);
            var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/C" + command);

            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
        }

        public void OpenB_Tree()
        {
            if (!File.Exists(ruta_file))
                return;

            try
            {
                System.Diagnostics.Process.Start(ruta_file);
            }
            catch (Exception ex)
            {
                //Error :
            }
        }

    }


}