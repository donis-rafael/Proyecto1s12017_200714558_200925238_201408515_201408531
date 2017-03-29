import os
from flask import Flask, request, send_file
path = "C:\Users\Ottoniel\Documents\GitHub\servidorPy\\"
app = Flask("Servidor de Usuarios y Activos")
escritorio = os.path.expanduser("~/Desktop")
class nodoLista:
    def __init__(self, activo):
        self.siguiente = None
        self.activo = activo
class listaParaReporte:
    def __init__(self):
        self.cabeza = None
    def insertar(self, nuevo):
        if self.cabeza==None:
            self.cabeza = nuevo
        else:
            nuevo.siguiente = self.cabeza
            self.cabeza = nuevo
        return self.cabeza

    def obtenerHASH(self, objeto):
        id = hash(objeto)
        if int(id) < 0:
            return str((-1 * id))
        return str(id)
    def graficar(self):
        if self.cabeza == None:
            print "esta vacia"
            return

        temporal = self.cabeza
        file = open(escritorio+"\\reporte.dot", "w")
        file.write("digraph G{\n")
        while temporal!=None:
            file.write("nodo" + self.obtenerHASH(temporal) + "[label=\"" + temporal.activo.id+"\n"+temporal.activo.nombre + "\", style = filled, shape= \"box\", fillcolor=\"blue:cyan\", gradientangle=\"270\"]\n")
            if temporal.siguiente!=None:
                file.write("nodo"+self.obtenerHASH(temporal)+" -> "+"nodo"+self.obtenerHASH(temporal.siguiente)+"\n")
            temporal = temporal.siguiente
        file.write("}")
        file.close()
        print "crea el reporte"
        os.system("dot -Tpng "+escritorio+"\\reporte.dot > "+escritorio+"\\reporte.png")
reporte = listaParaReporte()
class nodoUsuario:
    siguiente = None
    anterior = None
    usuario = ""
    nombreCompleto = ""
    password = ""
    activos = None
    def __init__(self, nombre, user, password):
        self.siguiente
        self.anterior
        self.usuario = user
        self.nombreCompleto = nombre
        self.password = password
        self.activos = AVL()
    def insertarActivo(self, nombre, descripcion, id):
        nuevo = nodoActivos(nombre, descripcion, id)
        self.activos.raizG = self.activos.insertarActivo(nuevo, self.activos.raizG)
        self.activos.raizG, val = self.activos.factorEquilibrio(self.activos.raizG)
        self.activos.raizG = self.activos.recorrerArbol(self.activos.raizG)
    def eliminarActivo(self, id):
        self.activos.raizG = self.activos.eliminarActivo(self.activos.raizG, id)
        self.activos.raizG, val = self.activos.factorEquilibrio(self.activos.raizG)
        self.activos.raizG = self.activos.recorrerArbol(self.activos.raizG)
    def graficarAVL(self):
        self.activos.graficarAVL(self.activos.raizG)
    def modificarActivo(self, id, nuevaD):
        self.activos.raizG = self.activos.modificarActivo(id, nuevaD, self.activos.raizG)
class nodoMatriz:
    arriba = None
    abajo = None
    izquierda = None
    derecha = None
    lista = None
    empresa = ""
    departamento = ""
    def __init__(self, empresa, departamento):
        self.arriba = None
        self.abajo = None
        self.derecha = None
        self.izquierda = None
        self.empresa = empresa
        self.departamento = departamento
        self.lista = listaAsociadaNodoMatriz()
    def insertarActivo(self, usuario, nombreA, descripcion, id):
        temporal = self.lista.primero
        while temporal!=None:
            if temporal.usuario == usuario:
                temporal.insertarActivo(nombreA, descripcion, id)
                return
            temporal = temporal.siguiente
    def eliminarActivo(self, usuario, id):
        temporal = self.lista.primero
        while temporal != None:
            if temporal.usuario == usuario:
                temporal.eliminarActivo(id)
                return
            temporal = temporal.siguiente
    def graficarAVL(self, usuario):
        temporal = self.lista.primero
        while temporal!=None:
            if temporal.usuario == usuario:
                temporal.graficarAVL()
                return
            temporal = temporal.siguiente
class nodoCabezaHorizontal:
    siguiente = None
    anterior = None
    empresa = ""
    lista = None

    def __init__(self, valor):
        self.siguiente = None
        self.anterior = None
        self.empresa = valor
        self.lista = listaAsociadaCabeceraHorizontal()
class nodoCabezaVertical:
    siguiente = None
    anterior = None
    departamento = ""
    lista = None

    def __init__(self, valor):
        self.siguiente = None
        self.anterior = None
        self.departamento = valor
        self.lista = listaAsociadaCabeceraVertical()
class nodoActivos:
    def __init__(self, nombre, descripcion, id):
        self.nombre = nombre
        self.descripcion = descripcion
        self.id = id
        self.derecho = None
        self.izquierdo = None
        self.fe = 0
class listaAsociadaNodoMatriz:
    def __init__(self):
        self.primero = None
        self.ultimo = None
    # metodo para insertar recibe como parametro nuevo que es un nodoUsuario
    def insertar(self, nuevo):
        print "inserto"
        if self.primero == None:
            self.primero = self.ultimo = nuevo
        else:
            nuevo.anterior = self.ultimo
            nuevo.siguiente = None
            self.ultimo.siguiente = nuevo
            self.ultimo = nuevo
    def insertarActivo(self, usuario, nombreA, descripcion, id):
        if self.primero==None:
            return
        temporal = self.primero
        while temporal!=None:
            if temporal.usuario == usuario:
                temporal.insertarActivo(nombreA, descripcion, id)
                return
            temporal = temporal.siguiente
    def login(self, usuario, password):
        temporal = self.primero
        while temporal!=None:
            if temporal.usuario == usuario:
                if temporal.password== password:
                    print "Lo encontro"
                    return "existe"
            temporal = temporal.siguiente
        return "no existe"
    def existeUsuario(self, usuario):
        temporal = self.primero
        while temporal!=None:
            if temporal.usuario == usuario:
                return True
            temporal = temporal.siguiente
        return False
class listaAsociadaCabeceraHorizontal:
    def __init__(self):
        self.primero = None
        self.ultimo = None

    def agregar(self, nuevo):
        if (self.primero == None):
            self.primero = self.ultimo = nuevo
            return

        if nuevo.departamento < self.primero.departamento:
            nuevo.abajo = self.primero
            nuevo.arriba = None
            self.primero.arriba = nuevo
            self.primero = nuevo
            return
        elif nuevo.departamento > self.ultimo.departamento:
            nuevo.abajo = None
            nuevo.arriba = self.ultimo
            self.ultimo.abajo = nuevo
            self.ultimo = nuevo
            return
        else:
            temporal = self.primero
            while temporal != None:
                if temporal.departamento > nuevo.departamento:
                    nuevo.arriba = temporal.arriba
                    nuevo.abajo = temporal
                    temporal.arriba.abajo = nuevo
                    temporal.arriba = nuevo
                temporal = temporal.abajo

    # recibe como parametro y que es la letra inicial de la direccion de correo
    def eliminar(self, y):
        if self.primero == None:
            return

        temporal = self.primero
        while temporal != None:
            if temporal.inicialDireccion == y:
                if self.primero == temporal and temporal.abajo != None:
                    self.primero.abajo.arriba = None
                    self.primero = self.primero.abajo
                    return
                elif self.primero == temporal and temporal.abajo == None:
                    self.primero = self.ultimo = None
                    return
                elif self.ultimo == temporal:
                    self.ultimo = self.ultimo.arriba
                    self.ultimo.abajo = None
                    return
                else:
                    temporal.abajo.arriba = temporal.arriba
                    temporal.arriba.abajo = temporal.abajo
                    return
            else:
                temporal = temporal.abajo
class listaAsociadaCabeceraVertical:
    def __init__(self):
        self.primero = None
        self.ultimo = None

    # nuevo es un nodo matriz
    def agregar(self, nuevo):
        if self.primero == None:
            self.primero = self.ultimo = nuevo
            return

        if nuevo.empresa < self.primero.empresa:
            nuevo.derecha = self.primero
            nuevo.izquierda = None
            self.primero.izquierda = nuevo
            self.primero = nuevo
            return
        elif nuevo.empresa > self.ultimo.empresa:
            nuevo.derecha = None
            nuevo.izquierda = self.ultimo
            self.ultimo.derecha = nuevo
            self.ultimo = nuevo
            return
        else:
            temporal = self.primero
            while temporal != None:
                if temporal.empresa > nuevo.empresa:
                    nuevo.izquierda = temporal.izquierda
                    nuevo.derecha = temporal
                    temporal.izquierda.derecha = nuevo
                    temporal.izquierda = nuevo
                    return
                temporal = temporal.derecha

    # recibe como parametro x que es el empresa del correo
    def eliminar(self, x):
        if self.primero == None:
            return

        temporal = self.primero
        while temporal != None:
            if temporal.empresa == x:
                if self.primero == temporal and temporal.derecha != None:
                    self.primero.derecha.izquierda = None
                    self.primero = self.primero.derecha
                    return
                elif self.primero == temporal and temporal.derecha == None:
                    self.primero = self.ultimo = None
                    return
                elif self.ultimo == temporal:
                    self.ultimo = self.ultimo.izquierda
                    self.ultimo.derecha = None
                    return
                else:
                    temporal.derecha.izquierda = temporal.izquierda
                    temporal.izquierda.derecha = temporal.derecha
                    return
            else:
                temporal = temporal.derecha
class listaCabecerasHorizontales:
    listaVertical = None
    def __init__(self):
        self.primero = None
        self.ultimo = None
        self.listaVertical = listaAsociadaCabeceraHorizontal()
    def insertar(self, empresa):
        nuevo = nodoCabezaHorizontal(empresa)
        if self.primero == None:
            self.primero = self.ultimo = nuevo
            return

        if nuevo.empresa < self.primero.empresa:
            nuevo.siguiente = self.primero
            nuevo.anterior = None
            self.primero.anterior = nuevo
            self.primero = nuevo
            return
        elif nuevo.empresa > self.ultimo.empresa:
            nuevo.siguiente = None
            nuevo.anterior = self.ultimo
            self.ultimo.siguiente = nuevo
            self.ultimo = nuevo
            return
        else:
            temporal = self.primero
            while temporal != None:
                if temporal.empresa > nuevo.empresa:
                    nuevo.anterior = temporal.anterior
                    nuevo.siguiente = temporal
                    temporal.anterior.siguiente = nuevo
                    temporal.anterior = nuevo
                    return
                temporal = temporal.siguiente
    def eliminar(self, empresa):
        if self.primero == None:
            return
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == empresa:
                if self.primero == temporal and temporal.siguiente != None:
                    self.primero.siguiente.anterior = None
                    self.primero = self.primero.siguiente
                    return
                elif self.primero == temporal and temporal.siguiente == None:
                    self.primero = self.ultimo = None
                    return
                elif self.ultimo == temporal:
                    self.ultimo = self.ultimo.anterior
                    self.ultimo.siguiente = None
                    return
                else:
                    temporal.siguiente.anterior = temporal.anterior
                    temporal.anterior.siguiente = temporal.siguiente
                    return
            else:
                temporal = temporal.siguiente
    def existeH(self, dom):
        if self.primero == None:
            return False
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == dom:
                return True
            temporal = temporal.siguiente
        return False
    def existeNodoMatriz(self, departamento, empresa):
        if self.primero == None:
            return False
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        return True
                    aux = aux.abajo
            temporal = temporal.siguiente
        return False
    def insertarDirectoANodo(self, departamento, empresa, nombre, usuario, password):
        if self.primero == None:
            return
        tm = self.primero
        while tm != None:
            if tm.empresa == empresa:
                aux = tm.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        nu = nodoUsuario(nombre, usuario, password)
                        aux.lista.insertar(nu)
                    aux = aux.abajo
            tm = tm.siguiente
    def insertarDirectoAArbolPorUsuario(self, usuario, nombreA, descripcion, id, empresa, departamento):
        if self.primero == None:
            return
        tm = self.primero
        while tm != None:
            if tm.empresa == empresa:
                aux = tm.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        aux.insertarActivo(usuario, nombreA, descripcion, id)
                        return
                    aux = aux.abajo
            tm = tm.siguiente
    def eliminarEnNodo(self, di, do):
        bandera = False
        if self.primero == None:
            return
        tm = self.primero
        while tm != None:
            if tm.empresa == do:
                aux = tm.lista.primero
                while aux != None:
                    if aux.inicialDireccion == di[0]:
                        aux.lista.eliminar(di + do)
                        if aux.lista.primero == None:
                            bandera = True
                    aux = aux.abajo
            tm = tm.siguiente
        return bandera
    def noTieneNada(self, domin):
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == domin:
                if temporal.lista.primero == None:
                    return True
            temporal = temporal.siguiente
        return False
    def graficarAVLPorUsuario(self, usuario, empresa, departamento):
        temporal = self.primero
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    if aux.departamento == departamento:
                            aux.graficarAVL(usuario)
                            return
                    aux = aux.abajo
            temporal = temporal.siguiente
    def eliminarActivoDeUsuario(self, usuario, empresa, departamento, id):
        if self.primero == None:
            return
        tm = self.primero
        while tm != None:
            if tm.empresa == empresa:
                aux = tm.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        aux.eliminarActivo(usuario,id)
                        return
                    aux = aux.abajo
            tm = tm.siguiente
    def login(self, departamento, empresa, usuario, password):
        temporal = self.primero
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    if aux.departamento == departamento:
                        return aux.lista.login(usuario,password)
                    aux = aux.abajo
            temporal = temporal.siguiente
        return "noExiste"
    def existeUsuario(self, departamento, empresa, usuario):
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        return aux.lista.existeUsuario(usuario)
                    aux = aux.abajo
            temporal = temporal.siguiente
        return False
    def modificarActivo(self, departamento, empresa, usuario, id, nuevaD):
        temporal = self.primero
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    if aux.departamento == departamento:
                        aux2 = aux.lista.primero
                        while aux2!=None:
                            if aux2.usuario == usuario:
                                aux2.activos.modificarActivo(id, nuevaD, aux2.activos.raizG )
                                return
                            aux2 = aux2.siguiente
                    aux = aux.abajo
            temporal = temporal.siguiente
    def devolverID(self, departamento, empresa, usuario):
        temporal = self.primero
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    if aux.departamento == departamento:
                        aux2 = aux.lista.primero
                        while aux2!=None:
                            if aux2.usuario == usuario:
                                return aux2.activos.retornarID(aux2.activos.raizG)
                            aux2 = aux2.siguiente
                    aux=aux.abajo
            temporal = temporal.siguiente
        return "No tiene,1,2"
    def devolverDescripcionPorID(self, departamento, usuario, empresa, id):
        temporal = self.primero
        while temporal != None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        aux2 = aux.lista.primero
                        while aux2 != None:
                            if aux2.usuario == usuario:
                                return aux2.activos.retornarDescripcion(aux2.activos.raizG, id)
                            aux2 = aux2.siguiente
                    aux = aux.abajo
            temporal = temporal.siguiente
        return "No tiene"
    def reportePorEmpresa(self, empresa):
        temporal = self.primero
        reporte.cabeza=None
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    aux2 = aux.lista.primero
                    while aux2!=None:
                        aux2.activos.listarAReporte(aux2.activos.raizG)
                        print "lo encuentra"
                        aux2=aux2.siguiente
                    aux = aux.abajo
            temporal = temporal.siguiente
        reporte.graficar()
    def devolverEmpresas(self):
        cadena = ""
        temporal = self.primero
        while temporal!=None:

            if temporal.siguiente==None:
                cadena+=temporal.empresa
                return cadena
            else:
                cadena += temporal.empresa + ","

            temporal = temporal.siguiente
        return cadena
    def todosLosActivos(self):
        temporal = self.primero
        conca = ""
        while temporal!=None:
            aux = temporal.lista.primero
            while aux!=None:
                aux2 = aux.lista.primero
                while aux2!=None:
                    conca +=aux2.activos.retornarID(aux2.activos.raizG)+"\n"
                    aux2 = aux2.siguiente
                aux = aux.abajo
            temporal = temporal.siguiente
        return conca


class listaCabecerasVerticales:
    concatenador = ""
    bandera = False
    def __init__(self):
        self.primero = None
        self.ultimo = None
    def insertar(self, departamento):
        nuevo = nodoCabezaVertical(departamento)
        if self.primero == None:
            self.primero = self.ultimo = nuevo
            return

        if nuevo.departamento < self.primero.departamento:
            nuevo.siguiente = self.primero
            nuevo.anterior = None
            self.primero.anterior = nuevo
            self.primero = nuevo
            return
        elif nuevo.departamento > self.ultimo.departamento:
            nuevo.siguiente = None
            nuevo.anterior = self.ultimo
            self.ultimo.siguiente = nuevo
            self.ultimo = nuevo
            return
        else:
            temporal = self.primero
            while temporal != None:
                if temporal.departamento > nuevo.departamento:
                    nuevo.anterior = temporal.anterior
                    nuevo.siguiente = temporal
                    temporal.anterior.siguiente = nuevo
                    temporal.anterior = nuevo
                    return
                temporal = temporal.siguiente
    def eliminar(self, departamento):
        if self.primero == None:
            return

        temporal = self.primero
        while temporal != None:
            if temporal.departamento == departamento:
                if self.primero == temporal and temporal.siguiente != None:
                    self.primero.siguiente.anterior = None
                    self.primero = self.primero.siguiente
                    return
                elif self.primero == temporal and temporal.siguiente == None:
                    self.primero = self.ultimo = None
                    return
                elif self.ultimo == temporal:
                    self.ultimo = self.ultimo.anterior
                    self.ultimo.siguiente = None
                    return
                else:
                    temporal.siguiente.anterior = temporal.anterior
                    temporal.anterior.siguiente = temporal.siguiente
                    return
            else:
                temporal = temporal.siguiente
    def concatenar(self, pedaso):
        if self.bandera == False:
            self.concatenador = self.concatenador + pedaso
            self.bandera = True
        else:
            self.concatenador = self.concatenador + "," + pedaso
    def buscarLetra(self, letra):
        if self.primero == None:
            return "no existe"
        temporal = self.primero
        while temporal != None:
            if temporal.departamento == letra:
                aux = temporal.lista.primero
                while aux != None:
                    aux2 = aux.lista.primero
                    while aux2 != None:
                        self.concatenar(aux2.direccion)
                        aux2 = aux2.siguiente
                    aux = aux.siguiente
                resultado = self.concatenador
                self.concatenador = ""
                self.bandera = False
                return resultado
            temporal = temporal.siguiente
    def existeV(self, departamento):
        if self.primero == None:
            return False
        temporal = self.primero
        while temporal != None:
            if temporal.departamento == departamento:
                return True
            temporal = temporal.siguiente
        return False
    def noTieneNada(self, indice):
        temporal = self.primero
        while temporal != None:
            if temporal.departamento == indice:
                if temporal.lista.primero == None:
                    return True
            temporal = temporal.siguiente
        return False
    def reportePorDepartamento(self, departamento):
        temporal = self.primero
        reporte.cabeza=None
        while temporal!=None:
            if temporal.departamento == departamento:
                aux = temporal.lista.primero
                while aux!=None:
                    aux2 = aux.lista.primero
                    while aux2!=None:
                        aux2.activos.listarAReporte(aux2.activos.raizG)
                        aux2=aux2.siguiente
                    aux = aux.derecha
            temporal = temporal.siguiente
        reporte.graficar()
    def listarDepartamentos(self):
        cadena = ""
        temporal = self.primero
        while temporal != None:

            if temporal.siguiente == None:
                cadena += temporal.departamento
                return cadena
            else:
                cadena += temporal.departamento + ","
            temporal = temporal.siguiente
        return cadena
class matriz:
    def __init__(self):
        self.verticales = listaCabecerasVerticales()
        self.horizontales = listaCabecerasHorizontales()
    def retornarUsuarios(self):
        cadena = ""
        temporal = self.horizontales.primero
        while temporal!=None:
            cadena+= temporal.empresa
            aux = temporal.lista.primero
            while aux!=None:
                aux2 = aux.lista.primero
                while aux2!=None:
                    cadena += "#" + aux.departamento + "#" + aux2.usuario + ","
                    aux2 = aux2.siguiente
                aux = aux.abajo
            temporal = temporal.siguiente
        return cadena+"0"
    def retornarUsu(self):
        cadena = ""
        temporal = self.horizontales.primero
        while temporal != None:
            aux = temporal.lista.primero
            while aux != None:
                aux2 = aux.lista.primero
                while aux2 != None:
                    cadena += aux2.usuario + ","
                    aux2 = aux2.siguiente
                aux = aux.abajo
            temporal = temporal.siguiente
        return cadena
    def insertar(self, empresa, departamento, usuario, nombreCompleto, password):
       # print(usuario+"   "+password)
        if self.verticales.existeV(departamento) == False:
            self.verticales.insertar(departamento)

        if self.horizontales.existeH(empresa) == False:
            self.horizontales.insertar(empresa)

        if self.horizontales.existeNodoMatriz(departamento, empresa) == False:
            nodom = nodoMatriz(empresa, departamento)
            tm = self.horizontales.primero
            while tm != None:
                if tm.empresa == empresa:
                    tm.lista.agregar(nodom)
                    break
                tm = tm.siguiente
            tm2 = self.verticales.primero
            while tm2 != None:
                if tm2.departamento == departamento:
                    tm2.lista.agregar(nodom)
                    break
                tm2 = tm2.siguiente

        self.horizontales.insertarDirectoANodo(departamento, empresa, nombreCompleto, usuario, password)
    def insertarActivo(self, empresa, departamento, usuario, nombreActivo, descripcio, id):
        self.horizontales.insertarDirectoAArbolPorUsuario(usuario,nombreActivo,descripcio,id,empresa,departamento)
    def graficarAVLPorUsuario(self, usuario, empresa,departamento):
        self.horizontales.graficarAVLPorUsuario(usuario, empresa, departamento)
    def obtenerHASH(self, objeto):
        id = hash(objeto)
        if int(id) < 0:
            return str((-1 * id))
        return str(id)
    def graficarMatriz(self):
        if self.horizontales.primero == None or self.verticales.primero == None:
            return
        cabeza = self.horizontales.primero
        lateral = self.verticales.primero
        file = open(escritorio+"\\matriz.dot", "w")
        file.write("digraph G{\n")
        file.write("\"nodoR\"[label=\"Inicio\", style = filled, fillcolor = \"red:yellow\", shape = \"box\", gradientangle=\"90\", group = rr]\n")
        file.write("{rank = same; \"nodoR\" \"nodoc" + self.obtenerHASH(cabeza) + "\"}\n")
        file.write("\"nodoR\" -> \"nodoc" + self.obtenerHASH(cabeza) + "\"\n")
        # graficando cabezas
        while cabeza != None:

            file.write("\"nodoc" + self.obtenerHASH(cabeza) + "\"[label = \"" + cabeza.empresa + "\", style = filled, fillcolor = \"red:yellow\",shape = \"box\", gradientangle=\"90\", group = r" + self.obtenerHASH(cabeza) + "]\n")
            if cabeza.siguiente != None:
                file.write("{rank = same; \"nodoc" + self.obtenerHASH(cabeza) + "\"  \"nodoc" + self.obtenerHASH(
                    cabeza.siguiente) + "\"}\n")
                file.write("\"nodoc" + self.obtenerHASH(cabeza) + "\" -> \"nodoc" + self.obtenerHASH(cabeza.siguiente) + "\"\n")
            if cabeza.anterior != None:
                file.write("{rank = same; \"nodoc" + self.obtenerHASH(cabeza) + "\"  \"nodoc" + self.obtenerHASH(
                    cabeza.anterior) + "\"}\n")
                file.write("\"nodoc" + self.obtenerHASH(cabeza) + "\" -> \"nodoc" + self.obtenerHASH(cabeza.anterior) + "\"\n")
            cabeza = cabeza.siguiente
        file.write("\"nodoR\" -> \"nodol" + self.obtenerHASH(lateral) + "\"\n")
        while lateral != None:
            file.write("\"nodol" + self.obtenerHASH(
                lateral) + "\"[label = \"" + lateral.departamento + "\", style = filled, fillcolor = \"red:yellow\",shape = \"box\", gradientangle=\"90\", group = rr]\n")
            if lateral.siguiente != None:
                file.write(
                    "\"nodol" + self.obtenerHASH(lateral) + "\" -> \"nodol" + self.obtenerHASH(lateral.siguiente) + "\"\n")
            if lateral.anterior != None:
                file.write(
                    "\"nodol" + self.obtenerHASH(lateral) + "\" -> \"nodol" + self.obtenerHASH(lateral.anterior) + "\"\n")
            lateral = lateral.siguiente
        cabeza = self.horizontales.primero
        while cabeza != None:
            enMatriz = cabeza.lista.primero
            file.write("subgraph s" + self.obtenerHASH(cabeza) + "{\n")
            while enMatriz != None:
                file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(enMatriz) + "\"[label = \"" + enMatriz.lista.primero.nombreCompleto + "\", style = filled, fillcolor = \"red:yellow\",shape = \"box\", gradientangle=\"90\", group = r" + self.obtenerHASH(cabeza) + "]\n")
                if enMatriz.lista.primero.siguiente != None:
                    tm = enMatriz.lista.primero.siguiente
                    file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\" -> \"nodoS" + self.obtenerHASH(tm) + "\"\n")
                    file.write("\"nodoS" + self.obtenerHASH(tm) + "\" -> " + "\"nodoc" + self.obtenerHASH(
                        enMatriz) + "l" + self.obtenerHASH(enMatriz) + "\"\n")
                    while tm != None:
                        file.write("\"nodoS" + self.obtenerHASH(
                            tm) + "\"[label = \"" + tm.nombreCompleto + "\", style = filled, fillcolor = \"red:yellow\", gradientangle=\"90\",shape = \"box\"]\n")
                        if tm.siguiente != None:
                            file.write(
                                "\"nodoS" + self.obtenerHASH(tm) + "\" -> \"nodoS" + self.obtenerHASH(tm.siguiente) + "\"\n")
                            file.write(
                                "\"nodoS" + self.obtenerHASH(tm.siguiente) + "\" -> \"nodoS" + self.obtenerHASH(tm) + "\"\n")
                        tm = tm.siguiente
                enMatriz = enMatriz.abajo
            file.write("}\n")
            cabeza = cabeza.siguiente
        cabeza = self.horizontales.primero
        while cabeza != None:
            enMatriz = cabeza.lista.primero
            while enMatriz != None:
                if enMatriz.derecha != None:
                    file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\" -> \"" + "nodoc" + self.obtenerHASH(enMatriz.derecha) + "l" + self.obtenerHASH(
                        enMatriz.derecha) + "\"\n")
                    file.write("{rank = same; " + " \"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\"  " + "\"nodoc" + self.obtenerHASH(enMatriz.derecha) + "l" + self.obtenerHASH(
                        enMatriz.derecha) + "\"}\n")
                if enMatriz.izquierda != None:
                    file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\" -> \"" + "nodoc" + self.obtenerHASH(enMatriz.izquierda) + "l" + self.obtenerHASH(
                        enMatriz.izquierda) + "\"\n")
                    file.write("{rank = same; " + " \"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\"  " + "\"nodoc" + self.obtenerHASH(enMatriz.izquierda) + "l" + self.obtenerHASH(
                        enMatriz.izquierda) + "\"}\n")
                if enMatriz.abajo != None:
                    file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\" -> " + "\"nodoc" + self.obtenerHASH(enMatriz.abajo) + "l" + self.obtenerHASH(
                        enMatriz.abajo) + "\"\n")
                    # file.write("{rank = same; " + " nodoc" + enMatriz.empresa + "l" + enMatriz.inicialDireccion + " " + "nodoc" + enMatriz.arriba.empresa + "l" + enMatriz.arriba.inicialDireccion)
                if enMatriz.arriba != None:
                    file.write("\"nodoc" + self.obtenerHASH(enMatriz) + "l" + self.obtenerHASH(
                        enMatriz) + "\" -> \"" + "nodoc" + self.obtenerHASH(enMatriz.arriba) + "l" + self.obtenerHASH(
                        enMatriz.arriba) + "\"\n")
                    # file.write("{rank = same; " + " nodoc" + enMatriz.empresa + "l" + enMatriz.inicialDireccion + " " + "nodoc" + enMatriz.arriba.empresa + "l" + enMatriz.arriba.inicialDireccion)
                enMatriz = enMatriz.abajo
            cabeza = cabeza.siguiente

        lateral = self.verticales.primero
        while lateral != None:
            enMatriz = lateral.lista.primero
            if lateral.lista.primero != None:
                file.write("\"nodol" + self.obtenerHASH(lateral) + "\" -> \"" + "nodoc" + self.obtenerHASH(
                    enMatriz) + "l" + self.obtenerHASH(enMatriz) + "\"\n")
                file.write("{rank = same; " + "\"nodol" + self.obtenerHASH(lateral) + "\"  " + "\"nodoc" + self.obtenerHASH(
                    enMatriz) + "l" + self.obtenerHASH(enMatriz) + "\"}\n")
            lateral = lateral.siguiente
        # file.write("}\n")
        cabeza = self.horizontales.primero
        while cabeza != None:
            enMatriz = cabeza.lista.primero
            if cabeza.lista.primero != None:
                file.write("\"nodoc" + self.obtenerHASH(cabeza) + "\" -> \"nodoc" + self.obtenerHASH(
                    enMatriz) + "l" + self.obtenerHASH(enMatriz) + "\"\n")
            cabeza = cabeza.siguiente

        file.write("}\n")
        file.close()
        os.system("dot -Tpng "+escritorio+"\\matriz.dot > "+escritorio+"\\matriz.png")
    def eliminar(self, cad):
        pedasos = cad.split("@")
        dir = pedasos[0]
        do = "@" + pedasos[1]
        caracterUno = dir[0]
        if self.verticales.existeV(caracterUno) == False:
            return " "

        if self.horizontales.existeH(do) == False:
            return " "

        if self.horizontales.existeNodoMatriz(do, caracterUno) == False:
            return " "
        if self.horizontales.eliminarEnNodo(dir, do) == True:
            # print "pasa eliminacion desde nodoMatriz"
            temporal = self.horizontales.primero
            while temporal != None:
                if temporal.empresa == do:
                    #       print "entra eliminacion de la lista asociada a nodo horizontal"
                    temporal.lista.eliminar(caracterUno)
                    #      print"pasa eliminacion de la lista asociada a nodo horizontal"
                    break
                temporal = temporal.siguiente
            if self.horizontales.noTieneNada(do) == True:
                # print "entra a la eliminacion de nodo horizontal"
                self.horizontales.eliminar(do)
                # print"pasa eliminacion de nodo horizontal"
            temporal2 = self.verticales.primero
            while temporal2 != None:
                if temporal2.departamento == caracterUno:
                    #   print "entra eliminacion de la lista asociada a nodo vertical"
                    temporal2.lista.eliminar(do)
                    #  print "pasa eliminacion de la lista asociada a nodo vertical"
                    break
                temporal2 = temporal2.siguiente
            if self.verticales.noTieneNada(caracterUno) == True:
                # print "entra eliminacion de nodo vertical"
                self.verticales.eliminar(caracterUno)
                # print "pasa eliminacion de nodo vertical"
    def modificarActivo(self, empresa, departamento, usuario, id, nuevaD):
        self.horizontales.modificarActivo(departamento,empresa,usuario,id,nuevaD)
    def existeUsuario(self, departamento, empresa, usuario):
        temporal = self.horizontales.primero
        while temporal!=None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux!=None:
                    if aux.departamento == departamento:
                        tm = aux.lista.primero
                        while tm!=None:
                            if tm.usuario == usuario:
                                return True
                            tm = tm.siguiente
                    aux = aux.abajo
            temporal = temporal.siguiente
        return False
    def tieneActivos(self, departamento, empresa, usuario):
        temporal = self.horizontales.primero
        while temporal != None:
            if temporal.empresa == empresa:
                aux = temporal.lista.primero
                while aux != None:
                    if aux.departamento == departamento:
                        tm = aux.lista.primero
                        while tm != None:
                            if tm.usuario == usuario and tm.activos.raizG!=None:
                                return True
                            tm = tm.siguiente
                    aux = aux.abajo
            temporal = temporal.siguiente
        return False


class AVL:
    def __init__(self):
        self.raizG=None
    def listarAReporte(self, raiz):
        if raiz==None:
            return
        nodo = nodoLista(raiz)
        reporte.cabeza =  reporte.insertar(nodo)
        if raiz.izquierdo:
            self.listarAReporte(raiz.izquierdo)
        if raiz.derecho:
            self.listarAReporte(raiz.derecho)
    def esHoja(self, nodo):
        if nodo==None:
            return False
        if nodo.derecho!=None or nodo.izquierdo!=None:
            return False
        return True
    def factorEquilibrio(self, raiz):
        if raiz==None:
            return raiz, 0
        if self.esHoja(raiz):
            raiz.fe = 0
            return raiz, 1
        elif raiz.izquierdo and raiz.derecho:
            raiz.izquierdo, valorIz = self.factorEquilibrio(raiz.izquierdo)
            raiz.derecho, valorDer = self.factorEquilibrio(raiz.derecho)
            raiz.fe = valorDer - valorIz
            if valorDer> valorIz:
                return raiz, (1+valorDer)
            else:
                return raiz, (1+valorIz)
        elif raiz.izquierdo:
            raiz.izquierdo, valorIz = self.factorEquilibrio(raiz.izquierdo)
            raiz.fe = valorIz*(-1)
            return raiz, (1+ valorIz)
        elif raiz.derecho:
            raiz.derecho, valorDer = self.factorEquilibrio(raiz.derecho)
            raiz.fe = valorDer
            return raiz, (1+ valorDer)
        return raiz
    def rotacionII(self, rai):
        n0 = rai
        n1 = rai.izquierdo
        n0.izquierdo = n1.derecho
        n1.derecho = n0
        return n1
    def rotacionID(self, rai):
        n0 = rai
        n1 = rai.izquierdo
        n2 = rai.izquierdo.derecho
        n0.izquierdo = n2.derecho
        n1.derecho = n2.izquierdo
        n2.derecho = n0
        n2.izquierdo = n1
        return n2
    def rotacionDD(self, rai):
        n0 = rai
        n1 = rai.derecho
        n0.derecho = n1.izquierdo
        n1.izquierdo = n0
        return n1
    def rotacionDI(self, rai):
        n0 = rai
        n1 = rai.derecho
        n2 = rai.derecho.izquierdo
        n0.derecho = n2.izquierdo
        n1.izquierdo = n2.derecho
        n2.derecho = n1
        n2.izquierdo = n0
        return n2
    def evaluarCasosAVL(self, ra):
        if ra.fe==-2:
            if ra.izquierdo.fe==-1:
                ra = self.rotacionII(ra)
            else:
                ra = self.rotacionID(ra)
        if ra.fe==2:
            if ra.derecho.fe==1:
                ra = self.rotacionDD(ra)
            else:
                ra = self.rotacionDI(ra)
        return ra
    def recorrerArbol(self, ra):
        if ra==None:
            return
        if ra.izquierdo:
           ra.izquierdo = self.recorrerArbol(ra.izquierdo)
        if ra.derecho:
            ra.derecho = self.recorrerArbol(ra.derecho)
        if ra.fe == 2 or ra.fe==-2:
            ra = self.evaluarCasosAVL(ra)
            ra, val = self.factorEquilibrio(ra)
        return ra
    def insertarActivo(self, nodoN, raiz):
        if raiz==None:
            raiz = nodoN

        else:
            if raiz.id > nodoN.id:
                raiz.izquierdo = self.insertarActivo(nodoN, raiz.izquierdo)
            else:
                raiz.derecho = self.insertarActivo(nodoN,raiz.derecho)
        return raiz
    def eliminarActivo(self, raiz, id):
        if raiz == None:
            return
        elif raiz.id > id:
            raiz.izquierdo = self.eliminarActivo(raiz.izquierdo, id)
        elif raiz.id < id:
            raiz.derecho = self.eliminarActivo(raiz.derecho, id)
        else:
            q = raiz
            if q.izquierdo == None:
                raiz = q.derecho
            elif q.derecho== None:
                raiz = q.izquierdo
            else:
                self.reemplazar(q)
            q = None
        return raiz
    def reemplazar(self, aux):
        p = aux
        a = aux.izquierdo
        while a.derecho:
            p = a
            a = a.derecho
        aux.nombre = a.nombre
        aux.descripcion = a.descripcion
        aux.id = a.id
        if p == aux:
            p.izquierdo = a.izquierdo
        else:
            p.derecho = a.izquierdo
            aux = a
    def existeActivo(self, b, id):
        if b==None:
            return False
        if b.id == id:
            return True
        elif b.id > id:
            if b.izquierdo:
                return self.existeActivo(b.izquierdo, id)
            else:
                return False
        else:
            if b.derecho:
                return self.existeActivo(b.derecho, id)
            else:
                return False
    def graficarAVL(self, raiz):
        if raiz==None:
            print"esta vacio :("
            return
        file = open(escritorio+"\\avl.dot", "w")
        file.write("digraph G{\n")
        file.write(self.graficarNodoAVL(raiz))
        file.write("}\n")
        file.close()
        os.system("dot -Tpng "+escritorio+"\\avl.dot > "+escritorio+"\\avl.png")
    def graficarNodoAVL(self, nodo):
        cadena = "nodo"+self.obtenerHASH(nodo)+"[label=\"<f0>|<f1>"+nodo.nombre+" \\n"+nodo.descripcion+"|<f2>\", shape=record,style=filled,fillcolor=\"blue:cyan\", gradientangle=\"270\"]\n"
        if nodo.izquierdo:
            cadena+=self.graficarNodoAVL(nodo.izquierdo)
            cadena+= "nodo"+self.obtenerHASH(nodo)+":f0 -> "+"nodo"+self.obtenerHASH(nodo.izquierdo)+"\n"
        if nodo.derecho:
            cadena += self.graficarNodoAVL(nodo.derecho)
            cadena += "nodo" + self.obtenerHASH(nodo) + ":f2 -> " + "nodo" + self.obtenerHASH(nodo.derecho)+"\n"
        return cadena
    def obtenerHASH(self, objeto):
        id = hash(objeto)
        if int(id) < 0:
            return str((-1 * id))
        return str(id)
    def modificarActivo(self, id, nuevaD, raiz):
        if raiz==None:
            return raiz
        if raiz.id == id:
            raiz.descripcion = nuevaD
            return raiz
        if raiz.id<id:
            raiz.derecho = self.modificarActivo(id, nuevaD, raiz.derecho)
            return raiz
        if raiz.id>id:
            raiz.izquierdo = self.modificarActivo(id, nuevaD, raiz.izquierdo)
            return raiz
        return raiz
    def retornarID(self, raiz):
        cadena = ""
        if raiz==None:
            return cadena
        cadena+= raiz.id+","+raiz.nombre+","+raiz.descripcion
        if raiz.derecho:
            cadena+=","+self.retornarID(raiz.derecho)
        if raiz.izquierdo:
            cadena+=","+self.retornarID(raiz.izquierdo)
        print cadena
        return cadena
    def retornarDesripcion(self, raiz, id):
        if raiz==None:
            return "No existe, nada"
        if raiz.id == id:
            return raiz.descripcion+","+raiz.nombre
        if raiz.id<id:
            return self.retornarDesripcion(raiz.derecho, id)
        if raiz.id>id:
            return self.retornarDesripcion(raiz.izquierdo, id)

ma = matriz()
@app.route('/insertarUsuario', methods= ['POST'])
def insertarUsuario():
    nombre = str(request.form['nombre'])
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    contra = str(request.form['password'])
    if ma.horizontales.existeUsuario(departamento, empresa, usuario) == False:
        ma.insertar(empresa, departamento, usuario, nombre, contra)
    return "inserto"
@app.route('/graficarMatriz', methods= ['POST'])
def graficarMatriz():
    nombre = str(request.form['p'])
    ma.graficarMatriz()
    return send_file(escritorio+"\\matriz.png", attachment_filename="matriz.png")
@app.route('/insertarActivo', methods= ['POST'])
def insertarActivo():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    nombreActivo = str(request.form['nombreA'])
    descripcion = str(request.form['descripcion'])
    id = str(request.form['id'])
    ma.insertarActivo(empresa, departamento, usuario, nombreActivo, descripcion, id)
    return "insertado"
@app.route('/eliminarActivo', methods= ['POST'])
def eliminarActivo():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    id = str(request.form['id'])
    ma.horizontales.eliminarActivoDeUsuario(usuario, empresa, departamento, id)
    return "eliminado"
@app.route('/graficarAVL', methods= ['POST'])
def graficarAVL():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    if ma.existeUsuario(departamento, empresa, usuario):
        if ma.tieneActivos(departamento, empresa, usuario):
            ma.graficarAVLPorUsuario(usuario, empresa, departamento)
            print "lo encontro\n"
            return send_file(escritorio + "\\avl.png", attachment_filename="avl.png")
        else:
            return send_file(escritorio + "\\noActivos.png", attachment_filename="noActivos.png")

    else:
        return send_file(escritorio + "\\error.png", attachment_filename="error.png")

@app.route('/verificarLogin', methods= ['POST'])
def verificarLogin():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    password = str(request.form['password'])
    print usuario+" "+departamento+" "+empresa+" "+password
    return ma.horizontales.login(departamento, empresa, usuario, password)
@app.route('/modificarActivo', methods= ['POST'])
def modificarActivo():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
    descripcion = str(request.form['descripcion'])
    id = str(request.form['id'])
    ma.modificarActivo(empresa, departamento, usuario, id, descripcion)
    return "modificado"
@app.route('/imagen', methods=['GET'])
def devolverPath():
    return path+"matriz.png"
@app.route('/usuarios', methods= ['POST'])
def usuarios():
    usuario = str(request.form['p'])
    print ma.retornarUsuarios()
    return ma.retornarUsuarios()
@app.route('/retornarActivos', methods = ['POST'])
def retornarActivos():
    usuario = str(request.form['usuario'])
    departamento = str(request.form['departamento'])
    empresa = str(request.form['empresa'])
   # password = str(request.form['password'])
    return str(ma.horizontales.devolverID(departamento, empresa, usuario))
@app.route('/descripcionActivo', methods= ['POST'])
def descripcionActivo():
    usuario = str(request['usuario'])
    empresa = str(request['empresa'])
    departamento = str(request['departamento'])
    id = str(request['id'])
    return ma.horizontales.devolverDescripcionPorID(departamento, usuario, empresa, id)
@app.route('/reportePorDepartamento', methods = ['POST'])
def reporteDepartamento():
    departamento = str(request.form['departamento'])
    ma.verticales.reportePorDepartamento(departamento)
    return send_file(escritorio+"\\reporte.png", attachment_filename="reporte.png")
@app.route('/reportePorEmpresa', methods = ['POST'])
def reporteEmpresa():
    empresa = str(request.form['empresa'])
    ma.horizontales.reportePorEmpresa(empresa)
    return send_file(escritorio+"\\reporte.png", attachment_filename="reporte.png")
@app.route('/departamentos', methods = ['POST'])
def departamentos():
    p = str(request.form['p'])
    print ma.verticales.listarDepartamentos()
    return ma.verticales.listarDepartamentos()
@app.route('/empresas', methods = ['POST'])
def empresas():
    p = str(request.form['p'])
    print ma.horizontales.devolverEmpresas()
    return ma.horizontales.devolverEmpresas()
@app.route('/listarUsuarios', methods = ['POST'])
def usus():
    parametro = str(request.form['p'])
    print ma.retornarUsu()
    return ma.retornarUsu()
@app.route('/activos', methods = ['POST'])
def activos():
    parametro = str(request.form['p'])
    print ma.horizontales.todosLosActivos()
    return ma.horizontales.todosLosActivos()
ma.insertar("udo","dos","prueba","jose","123")
ma.insertar("unqo","dgos","pueba","jose","123")
ma.insertar("un2o","dos","prueba","jose","123")
ma.insertar("uno3","d7os","preba","jose","123")
ma.insertar("unoe","dos","prueb","jose","123")
ma.insertar("unoe","dos","pb","jose","123")
ma.insertarActivo("uno", "dos", "prueba", "Impresora", "Esta es la descripcion", "1")
ma.insertarActivo("uno", "dos", "prueba", "Impresora", "Descripcion2", "2")
ma.horizontales.devolverID("dos", "uno", "prueba")
ma.graficarMatriz()
ma.horizontales.reportePorEmpresa("uno")
ma.graficarAVLPorUsuario("prueba", "uno", "dos")
print "los activos son"+ma.horizontales.todosLosActivos()
ma.graficarMatriz()
if __name__ == '__main__':
    app.run(debug = True, host = '192.168.43.180')

