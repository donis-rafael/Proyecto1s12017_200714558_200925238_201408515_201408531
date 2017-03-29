using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ArbolB_PRUEBA
{
    public class Btree
    {
        public Bnodo p = new Bnodo();
        Bnodo Xder = new Bnodo();
        Bnodo Xizq = new Bnodo();
        NodoPr X;
        Bnodo Xr;

        bool EmpA = false, Esta = false;

        //para crear ek fichero
        int val = 0;
        StringBuilder buffer;

        public Btree()
        {
            Xder = Xizq = null;
            X = null;
            Xr = null;
        }

        public void Inserta(NodoPr clave)
        {
            Insertaa(clave, p);
        }

        void Insertaa(NodoPr clave, Bnodo raiz)
        {
            Empujar(clave, raiz);
            if (EmpA)
            {
                p = new Bnodo();
                p.Cuentas = 1;
                p.Clave0 = X;
                p.Rama0 = raiz;
                p.Rama1 = Xr;
            }
        }

        void Empujar(NodoPr clave, Bnodo raiz)
        {
            int k = 0;
            Esta = false;
            if (Vacio(raiz))
            {
                EmpA = true;
                X = clave;
                Xr = null;
            }
            else
            {
                k = BuscarNodo(clave, raiz);
                if (Esta)
                {
                    EmpA = false;
                }
                else
                {
                    switch (k)
                    {
                        case 4:
                            Empujar(clave, raiz.Rama4);
                            break;
                        case 3:
                            Empujar(clave, raiz.Rama3);
                            break;
                        case 2:
                            Empujar(clave, raiz.Rama2);
                            break;
                        case 1:
                            Empujar(clave, raiz.Rama1);
                            break;
                        case 0:
                            Empujar(clave, raiz.Rama0);
                            break;
                        default:
                            break;
                    }
                    if (EmpA)
                    {
                        if (raiz.Cuentas < 4)
                        {
                            EmpA = false;
                            MeterHoja(X, raiz, k);
                        }
                        else
                        {
                            EmpA = true;
                            DividirN(X, raiz, k);
                        }
                    }
                }
            }
        }

        //

        void DividirN(NodoPr Clave, Bnodo Raiz, int k)
        {
            int pos = 0;
            int Posmda = 0;
            if (k <= 2)
            {
                Posmda = 2;
            }
            else
            {
                Posmda = 3;
            }
            Bnodo Mder = new Bnodo();
            pos = Posmda + 1;

            int vale;
            while (pos != 5)
            {
                vale = (pos - Posmda) - 1;
                if (Posmda == 2)
                {
                    switch (vale)
                    {
                        case 1:
                            Mder.Clave1 = Raiz.Clave3;
                            Mder.Rama2 = Raiz.Rama4;
                            break;
                        case 0:
                            Mder.Clave0 = Raiz.Clave2;
                            Mder.Rama1 = Raiz.Rama3;
                            break;
                        default:
                            break;
                    }
                }
                else if (Posmda == 3)
                {
                    switch (vale)
                    {
                        case 0:
                            Mder.Clave0 = Raiz.Clave3;
                            Mder.Rama1 = Raiz.Rama4;
                            break;
                        default:
                            break;
                    }
                }
                ++pos;
            }
            //
            Mder.Cuentas = 4 - Posmda;
            Raiz.Cuentas = Posmda;
            if (k <= 2)
            {
                MeterHoja(Clave, Raiz, k);
            }
            else
            {
                MeterHoja(Clave, Mder, (k - Posmda));
            }
            //
            vale = Raiz.Cuentas - 1;
            switch (vale)
            {
                case 3:
                    X = Raiz.Clave3;
                    break;
                case 2:
                    X = Raiz.Clave2;
                    break;
                case 1:
                    X = Raiz.Clave1;
                    break;
                case 0:
                    X = Raiz.Clave0;
                    break;
                default:
                    break;
            }
            vale = Raiz.Cuentas;
            switch (vale)
            {
                case 4:
                    Mder.Rama0 = Raiz.Rama4;
                    break;
                case 3:
                    Mder.Rama0 = Raiz.Rama3;
                    break;
                case 2:
                    Mder.Rama0 = Raiz.Rama2;
                    break;
                case 1:
                    Mder.Rama0 = Raiz.Rama1;
                    break;
                case 0:
                    Mder.Rama0 = Raiz.Rama0;
                    break;
                default:
                    break;
            }
            Raiz.Cuentas = --Raiz.Cuentas;
            Xr = Mder;
        }

        //

        void MeterHoja(NodoPr clave, Bnodo raiz, int k)
        {
            int i = raiz.Cuentas;

            while (i != k)
            {
                switch (i)
                {
                    case 3:
                        raiz.Clave3 = raiz.Clave2;
                        raiz.Rama4 = raiz.Rama3;
                        break;
                    case 2:
                        raiz.Clave2 = raiz.Clave1;
                        raiz.Rama3 = raiz.Rama2;
                        break;
                    case 1:
                        raiz.Clave1 = raiz.Clave0;
                        raiz.Rama2 = raiz.Rama1;
                        break;
                    default:
                        break;
                }
                --i;
            }
            switch (k)
            {
                case 3:
                    raiz.Clave3 = clave;
                    raiz.Rama4 = Xr;
                    break;
                case 2:
                    raiz.Clave2 = clave;
                    raiz.Rama3 = Xr;
                    break;
                case 1:
                    raiz.Clave1 = clave;
                    raiz.Rama2 = Xr;
                    break;
                case 0:
                    raiz.Clave0 = clave;
                    raiz.Rama1 = Xr;
                    break;
            }
            raiz.Cuentas = ++raiz.Cuentas;
        }

        //

        int BuscarNodo(NodoPr clave, Bnodo raiz)
        {
            int j = 0;
            if (clave.id_transac.CompareTo(raiz.Clave0.id_transac) < 0)//if (clave.duda < raiz.Clave0.duda)
            {
                Esta = false;
                j = 0;
            }
            else
            {
                j = raiz.Cuentas;
                for (int ii = j; ii > 0; ii--)
                {
                    if (raiz.Clave3 != null && ii == 4)
                    {
                        if (clave.id_transac.CompareTo(raiz.Clave3.id_transac) < 0 && j > 1)//if (clave.duda < raiz.Clave3.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave2 != null && ii == 3)
                    {
                        if (clave.id_transac.CompareTo(raiz.Clave2.id_transac) < 0 && j > 1) //if (clave.duda < raiz.Clave2.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave1 != null && ii == 2)
                    {
                        if (clave.id_transac.CompareTo(raiz.Clave1.id_transac) < 0 && j > 1)//if (clave.duda < raiz.Clave1.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave0 != null && ii == 1)
                    {
                        if (clave.id_transac.CompareTo(raiz.Clave0.id_transac) < 0 && j > 1)//if (clave.duda < raiz.Clave0.duda && j > 1)
                        {
                            j--;
                        }
                    }
                }
                //
                switch (j)
                {
                    case 4:
                        if (raiz.Clave3 != null)
                            Esta = (clave.id_transac.Equals(raiz.Clave3.id_transac));//(clave.duda == raiz.Clave3.duda);
                        break;
                    case 3:
                        if (raiz.Clave2 != null)
                            Esta = (clave.id_transac.Equals(raiz.Clave2.id_transac));//(clave.duda == raiz.Clave2.duda);
                        break;
                    case 2:
                        if (raiz.Clave1 != null)
                            Esta = (clave.id_transac.Equals(raiz.Clave1.id_transac));//(clave.duda == raiz.Clave1.duda);
                        break;
                    case 1:
                        if (raiz.Clave0 != null)
                            Esta = (clave.id_transac.Equals(raiz.Clave0.id_transac));//(clave.duda == raiz.Clave0.duda);
                        break;
                }
            }
            return j;
        }

        //

        bool Miembro(NodoPr clave, Bnodo raiz)
        {
            bool si = false;
            int j;
            if (!Vacio(p))
            {
                if (clave.id_transac.CompareTo(raiz.Clave0.id_transac) < 0)//if (clave.duda < raiz.Clave0.duda)
                {
                    si = false;
                    j = 0;
                }
                else
                {
                    j = raiz.Cuentas;
                    for (int ii = j; ii > 1; ii++)
                    {
                        if (raiz.Clave3 != null && ii == 4)
                        {
                            if (clave.id_transac.CompareTo(raiz.Clave3.id_transac) < 0 && j > 1) //if (clave.duda < raiz.Clave3.duda && j > 1)
                            {
                                j--;
                            }
                        }
                        if (raiz.Clave2 != null && ii == 3)
                        {
                            if (clave.id_transac.CompareTo(raiz.Clave2.id_transac) < 0 && j > 1) //if (clave.duda < raiz.Clave2.duda && j > 1)
                            {
                                j--;
                            }
                        }
                        if (raiz.Clave1 != null && ii == 2)
                        {
                            if (clave.id_transac.CompareTo(raiz.Clave1.id_transac) < 0 && j > 1) //if (clave.duda < raiz.Clave1.duda && j > 1)
                            {
                                j--;
                            }
                        }
                    }
                    switch (j)
                    {
                        case 4:
                            si = (clave.id_transac.CompareTo(raiz.Clave3.id_transac) < 0); //si = (clave.duda == raiz.Clave3.duda);
                            break;
                        case 3:
                            si = (clave.id_transac.CompareTo(raiz.Clave2.id_transac) < 0); //si = (clave.duda == raiz.Clave2.duda);
                            break;
                        case 2:
                            si = (clave.id_transac.CompareTo(raiz.Clave1.id_transac) < 0);//si = (clave.duda == raiz.Clave1.duda);
                            break;
                        case 1:
                            si = (clave.id_transac.CompareTo(raiz.Clave0.id_transac) < 0);//si = (clave.duda == raiz.Clave0.duda);
                            break;
                    }
                }
            }
            return si;
        }

        bool Vacio(Bnodo raiz)
        {
            return (raiz == null || raiz.Cuentas == 0);
        }

        //

        void BusquedaNum(String identi)
        {
            Bnodo nodo = p;
            int k = 0;
            int c = 0;

            //buffer.append("Nodo").append(val).append("[label=\"<P0>");

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
                        if (nodo.Clave0.id_transac.Equals(identi))//if (nodo.Clave0.duda == num)
                        {
                            //condicion si lo encuentra
                        }
                        break;
                    case 1:
                        if (nodo.Clave1.id_transac.Equals(identi))//if (nodo.Clave1.duda == num)
                        {
                            //condicion si lo encuentra
                        }
                        break;
                    case 2:
                        if (nodo.Clave2.id_transac.Equals(identi))//if (nodo.Clave2.duda == num)
                        {
                            //condicion si lo encuentra
                        }
                        break;
                    case 3:
                        if (nodo.Clave3.id_transac.Equals(identi))//if (nodo.Clave3.duda == num)
                        {
                            //condicion si lo encuentra
                        }
                        break;
                }
                c++;
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
                        RecursivoBusquedaNum(nodo.Rama0, identi);
                        break;
                    case 1:
                        RecursivoBusquedaNum(nodo.Rama1, identi);
                        break;
                    case 2:
                        RecursivoBusquedaNum(nodo.Rama2, identi);
                        break;
                    case 3:
                        RecursivoBusquedaNum(nodo.Rama3, identi);
                        break;
                    case 4:
                        RecursivoBusquedaNum(nodo.Rama4, identi);
                        break;
                }
                k++;
            }
        }

        void RecursivoBusquedaNum(Bnodo nodo, String identi)
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
                            if (nodo.Clave0.id_transac.Equals(identi))//if (nodo.Clave0.duda == id)
                            {
                                //condicion si lo encuentra
                            }
                            break;
                        case 1:
                            if (nodo.Clave1.id_transac.Equals(identi))//if (nodo.Clave1.duda == num)
                            {
                                //condicion si lo encuentra
                            }
                            break;
                        case 2:
                            if (nodo.Clave2.id_transac.Equals(identi))//if (nodo.Clave2.duda == num)
                            {
                                //condicion si lo encuentra
                            }
                            break;
                        case 3:
                            if (nodo.Clave3.id_transac.Equals(identi))//if (nodo.Clave3.duda == num)
                            {
                                //condicion si lo encuentra
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
            //buffer.append("\"];\n");
            //String pasa = "Nodo" + val;

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
                //buffer.append(pasa).append(":P").append(k).append(" -> " + "Nodo").append(val).append(";\n");
                switch (k)
                {
                    case 0:
                        RecursivoBusquedaNum(nodo.Rama0, identi);
                        break;
                    case 1:
                        RecursivoBusquedaNum(nodo.Rama1, identi);
                        break;
                    case 2:
                        RecursivoBusquedaNum(nodo.Rama2, identi);
                        break;
                    case 3:
                        RecursivoBusquedaNum(nodo.Rama3, identi);
                        break;
                    case 4:
                        RecursivoBusquedaNum(nodo.Rama4, identi);
                        break;
                }
                k++;
            }

        }

        public NodoPr nodo_ = null;
        public NodoPr Buscar_Posicion(String clave, Bnodo raiz)
        {
            if (nodo_ != null)
                return nodo_;

            int k = 0;
            Esta = false;
            if (!Vacio(raiz))
            {
                k = BuscarNodo_Val(clave, raiz);
                if (Esta)
                {
                    switch (k)
                    {
                        case 4:
                            if (raiz.Clave3 != null)
                                return raiz.Clave3;
                            break;
                        case 3:
                            if (raiz.Clave2 != null)
                                return raiz.Clave2;
                            break;
                        case 2:
                            if (raiz.Clave1 != null)
                                return raiz.Clave1;
                            break;
                        case 1:
                            if (raiz.Clave0 != null)
                                return raiz.Clave0;
                            break;
                    }
                    return nodo_;
                }
                else
                {
                    switch (k)
                    {
                        case 4:
                            nodo_ = Buscar_Posicion(clave, raiz.Rama4);
                            break;
                        case 3:
                            nodo_ = Buscar_Posicion(clave, raiz.Rama3);
                            break;
                        case 2:
                            nodo_ = Buscar_Posicion(clave, raiz.Rama2);
                            break;
                        case 1:
                            nodo_ = Buscar_Posicion(clave, raiz.Rama1);
                            break;
                        case 0:
                            nodo_ = Buscar_Posicion(clave, raiz.Rama0);
                            break;
                        default:
                            break;
                    }
                }
            }
            return nodo_;
        }

        int BuscarNodo_Val(String clave, Bnodo raiz)
        {
            int j = 0;
            if (clave.CompareTo(raiz.Clave0.id_transac) < 0)
            {
                Esta = false;
                j = 0;
            }
            else
            {
                j = raiz.Cuentas;
                for (int ii = j; ii > 0; ii--)
                {
                    if (raiz.Clave3 != null && ii == 4)
                    {
                        if (clave.CompareTo(raiz.Clave3.id_transac) < 0 && j > 1)//if (clave < raiz.Clave3.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave2 != null && ii == 3)
                    {
                        if (clave.CompareTo(raiz.Clave2.id_transac) < 0 && j > 1)//if (clave < raiz.Clave2.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave1 != null && ii == 2)
                    {
                        if (clave.CompareTo(raiz.Clave1.id_transac) < 0 && j > 1)//if (clave < raiz.Clave1.duda && j > 1)
                        {
                            j--;
                        }
                    }
                    if (raiz.Clave0 != null && ii == 1)
                    {
                        if (clave.CompareTo(raiz.Clave0.id_transac) < 0 && j > 1)//if (clave < raiz.Clave0.duda && j > 1)
                        {
                            j--;
                        }
                    }
                }
                switch (j)
                {
                    case 4:
                        if (raiz.Clave3 != null)
                            Esta = (clave.Equals(raiz.Clave3.id_transac));
                        break;
                    case 3:
                        if (raiz.Clave2 != null)
                            Esta = (clave.Equals(raiz.Clave2.id_transac));//(clave == raiz.Clave2.duda);
                        break;
                    case 2:
                        if (raiz.Clave1 != null)
                            Esta = (clave.Equals(raiz.Clave1.id_transac));//(clave == raiz.Clave1.duda);
                        break;
                    case 1:
                        if (raiz.Clave0 != null)
                            Esta = (clave.Equals(raiz.Clave0.id_transac));//(clave == raiz.Clave0.duda);
                        break;
                }
            }
            return j;
        }















        public void Eliminar(NodoPr clave)
        {
            if (Vacio(p))
            {
                //JOptionPane.showMessageDialog(null, "No hay elementos");
            }
            else
            {
                Eliminara(p, clave);
            }
        }

        public void Eliminara(Bnodo Raiz, NodoPr clave)
        {
            try
            {
                EliminarRegistro(Raiz, clave);
            }
            catch (Exception e)
            {
                Esta = false;
            }
            if (!Esta)
            {
                //JOptionPane.showMessageDialog(null, "No se encontro el elemento");
            }
            else
            {
                if (Raiz.Cuentas == 0)
                {
                    Raiz = Raiz.Rama0;//.Ramas[0];
                }
                p = Raiz;
                //JOptionPane.showMessageDialog(null, "Eliminacion completa");
            }
        }

        public void EliminarRegistro(Bnodo raiz, NodoPr c)
        {
            int pos = 0;
            NodoPr sucesor;
            if (Vacio(raiz))
            {
                Esta = false;
            }
            else
            {
                pos = BuscarNodo(c, raiz);
                if (Esta)
                {
                    switch (pos - 1)
                    {
                        case 4:
                            if (Vacio(raiz.Rama4)) { Quitar(raiz, pos); }
                            else
                            {
                                Sucesor(raiz, pos);
                                switch (pos)
                                {
                                    case 4: EliminarRegistro(raiz.Rama4, raiz.Clave3); break;
                                    case 3: EliminarRegistro(raiz.Rama3, raiz.Clave2); break;
                                    case 2: EliminarRegistro(raiz.Rama2, raiz.Clave1); break;
                                    case 1: EliminarRegistro(raiz.Rama1, raiz.Clave0); break;
                                }
                            }
                            break;
                        case 3:
                            if (Vacio(raiz.Rama3)) { Quitar(raiz, pos); }
                            else
                            {
                                Sucesor(raiz, pos);
                                switch (pos)
                                {
                                    case 4: EliminarRegistro(raiz.Rama4, raiz.Clave3); break;
                                    case 3: EliminarRegistro(raiz.Rama3, raiz.Clave2); break;
                                    case 2: EliminarRegistro(raiz.Rama2, raiz.Clave1); break;
                                    case 1: EliminarRegistro(raiz.Rama1, raiz.Clave0); break;
                                }
                            }
                            break;
                        case 2:
                            if (Vacio(raiz.Rama2)) { Quitar(raiz, pos); }
                            else
                            {
                                Sucesor(raiz, pos);
                                switch (pos)
                                {
                                    case 4: EliminarRegistro(raiz.Rama4, raiz.Clave3); break;
                                    case 3: EliminarRegistro(raiz.Rama3, raiz.Clave2); break;
                                    case 2: EliminarRegistro(raiz.Rama2, raiz.Clave1); break;
                                    case 1: EliminarRegistro(raiz.Rama1, raiz.Clave0); break;
                                }
                            }
                            break;
                        case 1:
                            if (Vacio(raiz.Rama1)) { Quitar(raiz, pos); }
                            else
                            {
                                Sucesor(raiz, pos);
                                switch (pos)
                                {
                                    case 4: EliminarRegistro(raiz.Rama4, raiz.Clave3); break;
                                    case 3: EliminarRegistro(raiz.Rama3, raiz.Clave2); break;
                                    case 2: EliminarRegistro(raiz.Rama2, raiz.Clave1); break;
                                    case 1: EliminarRegistro(raiz.Rama1, raiz.Clave0); break;
                                }
                            }
                            break;
                        case 0:
                            if (Vacio(raiz.Rama0)) { Quitar(raiz, pos); }
                            else
                            {
                                Sucesor(raiz, pos);
                                switch (pos)
                                {
                                    case 4: EliminarRegistro(raiz.Rama4, raiz.Clave3); break;
                                    case 3: EliminarRegistro(raiz.Rama3, raiz.Clave2); break;
                                    case 2: EliminarRegistro(raiz.Rama2, raiz.Clave1); break;
                                    case 1: EliminarRegistro(raiz.Rama1, raiz.Clave0); break;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (pos)
                    {
                        case 4: EliminarRegistro(raiz.Rama4, c); break;
                        case 3: EliminarRegistro(raiz.Rama3, c); break;
                        case 2: EliminarRegistro(raiz.Rama2, c); break;
                        case 1: EliminarRegistro(raiz.Rama1, c); break;
                        case 0: EliminarRegistro(raiz.Rama0, c); break;
                    }
                    switch (pos)
                    {
                        case 4: if (raiz.Rama4 != null && raiz.Rama4.Cuentas < 2) { Restablecer(raiz, pos); } break;
                        case 3: if (raiz.Rama3 != null && raiz.Rama3.Cuentas < 2) { Restablecer(raiz, pos); } break;
                        case 2: if (raiz.Rama2 != null && raiz.Rama2.Cuentas < 2) { Restablecer(raiz, pos); } break;
                        case 1: if (raiz.Rama1 != null && raiz.Rama1.Cuentas < 2) { Restablecer(raiz, pos); } break;
                        case 0: if (raiz.Rama0 != null && raiz.Rama0.Cuentas < 2) { Restablecer(raiz, pos); } break;
                    }
                }
            }
        }

        public void Sucesor(Bnodo raiz, int k)
        {
            Bnodo q = null;
            switch (k)
            {
                case 4: q = raiz.Rama4; break;
                case 3: q = raiz.Rama3; break;
                case 2: q = raiz.Rama2; break;
                case 1: q = raiz.Rama1; break;
                case 0: q = raiz.Rama0; break;
            }
            while (!Vacio(q.Rama0))
            {
                q = q.Rama0;
            }
            switch (k)
            {
                case 4: raiz.Clave3 = q.Clave0; break;
                case 3: raiz.Clave2 = q.Clave0; break;
                case 2: raiz.Clave1 = q.Clave0; break;
                case 1: raiz.Clave0 = q.Clave0; break;
                //case 0: raiz.Clave3 = q.Clave0; break;
            }
        }

        public void Combina(Bnodo raiz, int pos)
        {
            int j;
            switch (pos)
            {
                case 4: Xder = raiz.Rama4; break;
                case 3: Xder = raiz.Rama3; break;
                case 2: Xder = raiz.Rama2; break;
                case 1: Xder = raiz.Rama1; break;
                case 0: Xder = raiz.Rama0; break;
            }

            switch (pos - 1)
            {
                case 4: Xizq = raiz.Rama4; break;
                case 3: Xizq = raiz.Rama3; break;
                case 2: Xizq = raiz.Rama2; break;
                case 1: Xizq = raiz.Rama1; break;
                case 0: Xizq = raiz.Rama0; break;
            }
            Xizq.Cuentas++;

            switch (Xizq.Cuentas - 1)
            {
                case 3:
                    switch (pos - 1)
                    {
                        case 3: Xizq.Clave3 = raiz.Clave3; break;
                        case 2: Xizq.Clave3 = raiz.Clave2; break;
                        case 1: Xizq.Clave3 = raiz.Clave1; break;
                        case 0: Xizq.Clave3 = raiz.Clave0; break;
                    }
                    break;
                case 2:
                    switch (pos - 1)
                    {
                        case 3: Xizq.Clave2 = raiz.Clave3; break;
                        case 2: Xizq.Clave2 = raiz.Clave2; break;
                        case 1: Xizq.Clave2 = raiz.Clave1; break;
                        case 0: Xizq.Clave2 = raiz.Clave0; break;
                    }
                    break;
                case 1:
                    switch (pos - 1)
                    {
                        case 3: Xizq.Clave1 = raiz.Clave3; break;
                        case 2: Xizq.Clave1 = raiz.Clave2; break;
                        case 1: Xizq.Clave1 = raiz.Clave1; break;
                        case 0: Xizq.Clave1 = raiz.Clave0; break;
                    }
                    break;
                case 0:
                    switch (pos - 1)
                    {
                        case 3: Xizq.Clave0 = raiz.Clave3; break;
                        case 2: Xizq.Clave0 = raiz.Clave2; break;
                        case 1: Xizq.Clave0 = raiz.Clave1; break;
                        case 0: Xizq.Clave0 = raiz.Clave0; break;
                    }
                    break;
            }
            switch (Xizq.Cuentas)
            {
                case 4: Xizq.Rama4 = Xder.Rama0; break;
                case 3: Xizq.Rama3 = Xder.Rama0; break;
                case 2: Xizq.Rama2 = Xder.Rama0; break;
                case 1: Xizq.Rama1 = Xder.Rama0; break;
                case 0: Xizq.Rama0 = Xder.Rama0; break;
            }
            j = 1;
            while (j != Xder.Cuentas + 1)
            {
                Xizq.Cuentas++;

                switch (Xizq.Cuentas - 1)
                {

                    case 3:
                        switch (j - 1)
                        {
                            case 3: Xizq.Clave3 = Xder.Clave3; Xizq.Rama4 = Xder.Rama4; break;
                            case 2: Xizq.Clave3 = Xder.Clave2; Xizq.Rama4 = Xder.Rama3; break;
                            case 1: Xizq.Clave3 = Xder.Clave1; Xizq.Rama4 = Xder.Rama2; break;
                            case 0: Xizq.Clave3 = Xder.Clave0; Xizq.Rama4 = Xder.Rama1; break;
                        }
                        break;
                    case 2:
                        switch (j - 1)
                        {
                            case 3: Xizq.Clave2 = Xder.Clave3; Xizq.Rama3 = Xder.Rama4; break;
                            case 2: Xizq.Clave2 = Xder.Clave2; Xizq.Rama3 = Xder.Rama3; break;
                            case 1: Xizq.Clave2 = Xder.Clave1; Xizq.Rama3 = Xder.Rama2; break;
                            case 0: Xizq.Clave2 = Xder.Clave0; Xizq.Rama3 = Xder.Rama1; break;
                        }
                        break;
                    case 1:
                        switch (pos - 1)
                        {
                            case 3: Xizq.Clave1 = Xder.Clave3; Xizq.Rama2 = Xder.Rama4; break;
                            case 2: Xizq.Clave1 = Xder.Clave2; Xizq.Rama2 = Xder.Rama3; break;
                            case 1: Xizq.Clave1 = Xder.Clave1; Xizq.Rama2 = Xder.Rama2; break;
                            case 0: Xizq.Clave1 = Xder.Clave0; Xizq.Rama2 = Xder.Rama1; break;
                        }
                        break;
                    case 0:
                        switch (pos - 1)
                        {
                            case 3: Xizq.Clave0 = Xder.Clave3; Xizq.Rama1 = Xder.Rama4; break;
                            case 2: Xizq.Clave0 = Xder.Clave2; Xizq.Rama1 = Xder.Rama3; break;
                            case 1: Xizq.Clave0 = Xder.Clave1; Xizq.Rama1 = Xder.Rama2; break;
                            case 0: Xizq.Clave0 = Xder.Clave0; Xizq.Rama1 = Xder.Rama1; break;
                        }
                        break;
                }
                j++;
            }
            Quitar(raiz, pos);
        }

        public void MoverDer(Bnodo raiz, int pos)
        {
            int i = 0;
            switch (pos)
            {
                case 4: i = raiz.Rama4.Cuentas; break;
                case 3: i = raiz.Rama3.Cuentas; break;
                case 2: i = raiz.Rama2.Cuentas; break;
                case 1: i = raiz.Rama1.Cuentas; break;
                case 0: i = raiz.Rama0.Cuentas; break;
            }
            while (i != 0)
            {
                switch (i)
                {
                    case 3:
                        switch (pos)
                        {
                            case 4: raiz.Rama4.Clave3 = raiz.Rama4.Clave2; raiz.Rama4.Rama4 = raiz.Rama4.Rama3; break;
                            case 3: raiz.Rama3.Clave3 = raiz.Rama3.Clave2; raiz.Rama3.Rama4 = raiz.Rama3.Rama3; break;
                            case 2: raiz.Rama2.Clave3 = raiz.Rama2.Clave2; raiz.Rama2.Rama4 = raiz.Rama2.Rama3; break;
                            case 1: raiz.Rama1.Clave3 = raiz.Rama1.Clave2; raiz.Rama1.Rama4 = raiz.Rama1.Rama3; break;
                            case 0: raiz.Rama0.Clave3 = raiz.Rama0.Clave2; raiz.Rama0.Rama4 = raiz.Rama0.Rama3; break;
                        }
                        break;
                    case 2:
                        switch (pos)
                        {
                            case 4: raiz.Rama4.Clave2 = raiz.Rama4.Clave1; raiz.Rama4.Rama3 = raiz.Rama4.Rama2; break;
                            case 3: raiz.Rama3.Clave2 = raiz.Rama3.Clave1; raiz.Rama3.Rama3 = raiz.Rama3.Rama2; break;
                            case 2: raiz.Rama2.Clave2 = raiz.Rama2.Clave1; raiz.Rama2.Rama3 = raiz.Rama2.Rama2; break;
                            case 1: raiz.Rama1.Clave2 = raiz.Rama1.Clave1; raiz.Rama1.Rama3 = raiz.Rama1.Rama2; break;
                            case 0: raiz.Rama0.Clave2 = raiz.Rama0.Clave1; raiz.Rama0.Rama3 = raiz.Rama0.Rama2; break;
                        }
                        break;
                    case 1:
                        switch (pos)
                        {
                            case 4: raiz.Rama4.Clave1 = raiz.Rama4.Clave0; raiz.Rama4.Rama2 = raiz.Rama4.Rama1; break;
                            case 3: raiz.Rama3.Clave1 = raiz.Rama3.Clave0; raiz.Rama3.Rama2 = raiz.Rama3.Rama1; break;
                            case 2: raiz.Rama2.Clave1 = raiz.Rama2.Clave0; raiz.Rama2.Rama2 = raiz.Rama2.Rama1; break;
                            case 1: raiz.Rama1.Clave1 = raiz.Rama1.Clave0; raiz.Rama1.Rama2 = raiz.Rama1.Rama1; break;
                            case 0: raiz.Rama0.Clave1 = raiz.Rama0.Clave0; raiz.Rama0.Rama2 = raiz.Rama0.Rama1; break;
                        }
                        break;
                }
                //raiz.Ramas[pos].Claves[i] = raiz.Ramas[pos].Claves[i - 1];
                //raiz.Ramas[pos].Ramas[i + 1] = raiz.Ramas[pos].Ramas[i];
                --i;
            }
            switch (pos)
            {
                case 4:
                    raiz.Rama4.Cuentas++;
                    raiz.Rama4.Rama1 = raiz.Rama4.Rama0;
                    raiz.Rama4.Clave0 = raiz.Clave3;
                    switch (raiz.Rama3.Cuentas - 1)
                    {
                        case 3: raiz.Clave3 = raiz.Rama3.Clave3; break;
                        case 2: raiz.Clave3 = raiz.Rama3.Clave2; break;
                        case 1: raiz.Clave3 = raiz.Rama3.Clave1; break;
                        case 0: raiz.Clave3 = raiz.Rama3.Clave0; break;
                    }
                    switch (raiz.Rama3.Cuentas - 1)
                    {
                        case 3: raiz.Clave3 = raiz.Rama3.Clave3; break;
                        case 2: raiz.Clave3 = raiz.Rama3.Clave2; break;
                        case 1: raiz.Clave3 = raiz.Rama3.Clave1; break;
                        case 0: raiz.Clave3 = raiz.Rama3.Clave0; break;
                    }
                    switch (raiz.Rama3.Cuentas)
                    {
                        case 4: raiz.Rama4.Rama0 = raiz.Rama3.Rama4; break;
                        case 3: raiz.Rama4.Rama0 = raiz.Rama3.Rama3; break;
                        case 2: raiz.Rama4.Rama0 = raiz.Rama3.Rama2; break;
                        case 1: raiz.Rama4.Rama0 = raiz.Rama3.Rama1; break;
                        case 0: raiz.Rama4.Rama0 = raiz.Rama3.Rama0; break;

                    }
                    raiz.Rama3.Cuentas--;
                    break;
                case 3:
                    raiz.Rama3.Cuentas++;
                    raiz.Rama3.Rama1 = raiz.Rama3.Rama0;
                    raiz.Rama3.Clave0 = raiz.Clave2;
                    switch (raiz.Rama2.Cuentas - 1)
                    {
                        case 3: raiz.Clave2 = raiz.Rama2.Clave3; break;
                        case 2: raiz.Clave2 = raiz.Rama2.Clave2; break;
                        case 1: raiz.Clave2 = raiz.Rama2.Clave1; break;
                        case 0: raiz.Clave2 = raiz.Rama2.Clave0; break;
                    }
                    switch (raiz.Rama2.Cuentas)
                    {
                        case 4: raiz.Rama3.Rama0 = raiz.Rama2.Rama4; break;
                        case 3: raiz.Rama3.Rama0 = raiz.Rama2.Rama3; break;
                        case 2: raiz.Rama3.Rama0 = raiz.Rama2.Rama2; break;
                        case 1: raiz.Rama3.Rama0 = raiz.Rama2.Rama1; break;
                        case 0: raiz.Rama3.Rama0 = raiz.Rama2.Rama0; break;

                    }
                    raiz.Rama2.Cuentas--;
                    break;
                case 2:
                    raiz.Rama2.Cuentas++;
                    raiz.Rama2.Rama1 = raiz.Rama2.Rama0;
                    raiz.Rama2.Clave0 = raiz.Clave1;
                    switch (raiz.Rama1.Cuentas - 1)
                    {
                        case 3: raiz.Clave1 = raiz.Rama1.Clave3; break;
                        case 2: raiz.Clave1 = raiz.Rama1.Clave2; break;
                        case 1: raiz.Clave1 = raiz.Rama1.Clave1; break;
                        case 0: raiz.Clave1 = raiz.Rama1.Clave0; break;
                    }
                    switch (raiz.Rama1.Cuentas)
                    {
                        case 4: raiz.Rama2.Rama0 = raiz.Rama1.Rama4; break;
                        case 3: raiz.Rama2.Rama0 = raiz.Rama1.Rama3; break;
                        case 2: raiz.Rama2.Rama0 = raiz.Rama1.Rama2; break;
                        case 1: raiz.Rama2.Rama0 = raiz.Rama1.Rama1; break;
                        case 0: raiz.Rama2.Rama0 = raiz.Rama1.Rama0; break;

                    }
                    raiz.Rama1.Cuentas--;
                    break;
                case 1:
                    raiz.Rama1.Cuentas++;
                    raiz.Rama1.Rama1 = raiz.Rama1.Rama0;
                    raiz.Rama1.Clave0 = raiz.Clave0;
                    switch (raiz.Rama0.Cuentas - 1)
                    {
                        case 3: raiz.Clave0 = raiz.Rama0.Clave3; break;
                        case 2: raiz.Clave0 = raiz.Rama0.Clave2; break;
                        case 1: raiz.Clave0 = raiz.Rama0.Clave1; break;
                        case 0: raiz.Clave0 = raiz.Rama0.Clave0; break;
                    }
                    switch (raiz.Rama0.Cuentas)
                    {
                        case 4: raiz.Rama1.Rama0 = raiz.Rama0.Rama4; break;
                        case 3: raiz.Rama1.Rama0 = raiz.Rama0.Rama3; break;
                        case 2: raiz.Rama1.Rama0 = raiz.Rama0.Rama2; break;
                        case 1: raiz.Rama1.Rama0 = raiz.Rama0.Rama1; break;
                        case 0: raiz.Rama1.Rama0 = raiz.Rama0.Rama0; break;

                    }
                    raiz.Rama0.Cuentas--;
                    break;
            }
        }

        public void MoverIzq(Bnodo raiz, int pos)
        {
            int i = 0;
            int posv = 0;
            switch (pos)
            {
                case 4: posv = raiz.Rama4.Cuentas + 1;
                    raiz.Rama4.Cuentas++;
                    switch (raiz.Rama3.Cuentas - 1)
                    {
                        case 3: raiz.Rama3.Clave3 = raiz.Clave3; raiz.Rama3.Rama4 = raiz.Rama4.Rama0; break;
                        case 2: raiz.Rama3.Clave2 = raiz.Clave3; raiz.Rama3.Rama2 = raiz.Rama4.Rama0; break;
                        case 1: raiz.Rama3.Clave1 = raiz.Clave3; raiz.Rama3.Rama1 = raiz.Rama4.Rama0; break;
                        case 0: raiz.Rama3.Clave0 = raiz.Clave3; raiz.Rama3.Rama0 = raiz.Rama4.Rama0; break;
                    }
                    raiz.Clave3 = raiz.Rama4.Clave0;
                    raiz.Rama4.Rama0 = raiz.Rama4.Rama1;
                    raiz.Rama4.Cuentas--;
                    i = 1;
                    break;
                case 3: posv = raiz.Rama3.Cuentas + 1;
                    raiz.Rama3.Cuentas++;
                    switch (raiz.Rama2.Cuentas - 1)
                    {
                        case 3: raiz.Rama2.Clave3 = raiz.Clave2; raiz.Rama2.Rama4 = raiz.Rama3.Rama0; break;
                        case 2: raiz.Rama2.Clave2 = raiz.Clave2; raiz.Rama2.Rama2 = raiz.Rama3.Rama0; break;
                        case 1: raiz.Rama2.Clave1 = raiz.Clave2; raiz.Rama2.Rama1 = raiz.Rama3.Rama0; break;
                        case 0: raiz.Rama2.Clave0 = raiz.Clave2; raiz.Rama2.Rama0 = raiz.Rama3.Rama0; break;
                    }
                    raiz.Clave2 = raiz.Rama3.Clave0;
                    raiz.Rama3.Rama0 = raiz.Rama3.Rama1;
                    raiz.Rama3.Cuentas--;
                    i = 1;
                    break;
                case 2: posv = raiz.Rama2.Cuentas + 1;
                    raiz.Rama2.Cuentas++;
                    switch (raiz.Rama1.Cuentas - 1)
                    {
                        case 3: raiz.Rama1.Clave3 = raiz.Clave1; raiz.Rama1.Rama4 = raiz.Rama2.Rama0; break;
                        case 2: raiz.Rama1.Clave2 = raiz.Clave1; raiz.Rama1.Rama2 = raiz.Rama2.Rama0; break;
                        case 1: raiz.Rama1.Clave1 = raiz.Clave1; raiz.Rama1.Rama1 = raiz.Rama2.Rama0; break;
                        case 0: raiz.Rama1.Clave0 = raiz.Clave1; raiz.Rama1.Rama0 = raiz.Rama2.Rama0; break;
                    }
                    raiz.Clave1 = raiz.Rama2.Clave0;
                    raiz.Rama2.Rama0 = raiz.Rama2.Rama1;
                    raiz.Rama2.Cuentas--;
                    i = 1;
                    break;
                case 1: posv = raiz.Rama1.Cuentas + 1;
                    raiz.Rama1.Cuentas++;
                    switch (raiz.Rama0.Cuentas - 1)
                    {
                        case 3: raiz.Rama0.Clave3 = raiz.Clave1; raiz.Rama0.Rama4 = raiz.Rama1.Rama0; break;
                        case 2: raiz.Rama0.Clave2 = raiz.Clave1; raiz.Rama0.Rama2 = raiz.Rama1.Rama0; break;
                        case 1: raiz.Rama0.Clave1 = raiz.Clave1; raiz.Rama0.Rama1 = raiz.Rama1.Rama0; break;
                        case 0: raiz.Rama0.Clave0 = raiz.Clave1; raiz.Rama0.Rama0 = raiz.Rama1.Rama0; break;
                    }
                    raiz.Clave0 = raiz.Rama1.Clave0;
                    raiz.Rama1.Rama0 = raiz.Rama1.Rama1;
                    raiz.Rama1.Cuentas--;
                    i = 1;
                    break;
                case 0: break;
            }


            while (i != posv)
            {
                switch (pos)
                {
                    case 0:
                        switch (i)
                        {
                            case 1: raiz.Rama0.Clave0 = raiz.Rama0.Clave1; raiz.Rama0.Rama1 = raiz.Rama0.Rama2; break;
                            case 2: raiz.Rama0.Clave1 = raiz.Rama0.Clave2; raiz.Rama0.Rama2 = raiz.Rama0.Rama3; break;
                            case 3: raiz.Rama0.Clave2 = raiz.Rama0.Clave3; raiz.Rama0.Rama3 = raiz.Rama0.Rama4; break;
                            //case 4: raiz.Rama0.Clave3 = raiz.Rama0.Clave4; break;
                        }
                        break;
                    case 1:
                        switch (i)
                        {
                            case 1: raiz.Rama1.Clave0 = raiz.Rama1.Clave1; raiz.Rama1.Rama1 = raiz.Rama1.Rama2; break;
                            case 2: raiz.Rama1.Clave1 = raiz.Rama1.Clave2; raiz.Rama1.Rama2 = raiz.Rama1.Rama3; break;
                            case 3: raiz.Rama1.Clave2 = raiz.Rama1.Clave3; raiz.Rama1.Rama3 = raiz.Rama1.Rama4; break;
                        }
                        break;
                    case 2:
                        switch (i)
                        {
                            case 1: raiz.Rama2.Clave0 = raiz.Rama2.Clave1; raiz.Rama2.Rama1 = raiz.Rama2.Rama2; break;
                            case 2: raiz.Rama2.Clave1 = raiz.Rama2.Clave2; raiz.Rama2.Rama2 = raiz.Rama2.Rama3; break;
                            case 3: raiz.Rama2.Clave2 = raiz.Rama2.Clave3; raiz.Rama2.Rama3 = raiz.Rama2.Rama4; break;
                        }
                        break;
                    case 3:
                        switch (i)
                        {
                            case 1: raiz.Rama3.Clave0 = raiz.Rama3.Clave1; raiz.Rama3.Rama1 = raiz.Rama3.Rama2; break;
                            case 2: raiz.Rama3.Clave1 = raiz.Rama3.Clave2; raiz.Rama3.Rama2 = raiz.Rama3.Rama3; break;
                            case 3: raiz.Rama3.Clave2 = raiz.Rama3.Clave3; raiz.Rama3.Rama3 = raiz.Rama3.Rama4; break;
                        }
                        break;
                    case 4:
                        switch (i)
                        {
                            case 1: raiz.Rama4.Clave0 = raiz.Rama4.Clave1; raiz.Rama4.Rama1 = raiz.Rama4.Rama2; break;
                            case 2: raiz.Rama4.Clave1 = raiz.Rama4.Clave2; raiz.Rama4.Rama2 = raiz.Rama4.Rama3; break;
                            case 3: raiz.Rama4.Clave2 = raiz.Rama4.Clave3; raiz.Rama4.Rama3 = raiz.Rama4.Rama4; break;
                        }
                        break;
                }
                i++;
            }
        }

        public void Quitar(Bnodo raiz, int pos)
        {
            int j = pos + 1;
            while (j != raiz.Cuentas + 1)
            {
                switch (j)
                {
                    case 4: raiz.Clave2 = raiz.Clave3; raiz.Rama3 = raiz.Rama4; break;
                    case 3: raiz.Clave1 = raiz.Clave2; raiz.Rama2 = raiz.Rama3; break;
                    case 2: raiz.Clave0 = raiz.Clave1; raiz.Rama1 = raiz.Rama2; break;
                    case 1: break;
                    case 0: break;
                }
                j++;
            }
            raiz.Cuentas--;
        }

        public void Restablecer(Bnodo raiz, int pos)
        {
            if (pos > 0)
            {
                switch (pos)
                {
                    case 4: if (raiz.Rama3.Cuentas > 2) { MoverDer(raiz, pos); } else { Combina(raiz, pos); } break;
                    case 3: if (raiz.Rama2.Cuentas > 2) { MoverDer(raiz, pos); } else { Combina(raiz, pos); } break;
                    case 2: if (raiz.Rama1.Cuentas > 2) { MoverDer(raiz, pos); } else { Combina(raiz, pos); } break;
                    case 1: if (raiz.Rama0.Cuentas > 2) { MoverDer(raiz, pos); } else { Combina(raiz, pos); } break;
                }
            }
            else if (raiz.Rama1.Cuentas > 2)
            {
                MoverIzq(raiz, 1);
            }
            else
            {
                Combina(raiz, 1);
            }
        }
    }
}