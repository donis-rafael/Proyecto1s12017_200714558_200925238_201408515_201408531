<%-- 
    Document   : modify-article
    Created on : 21/03/2017, 12:01:11 AM
    Author     : KMMG
--%>

<%@page import="com.squareup.okhttp.FormEncodingBuilder"%>
<%@page import="conexion.Conexion"%>
<%@page import="com.squareup.okhttp.RequestBody"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>

<%
    HttpSession s = request.getSession();
        String user = (String)s.getAttribute("user");
        String empresa = (String)s.getAttribute("empresa");
        String depto = (String)s.getAttribute("depto");
    String res="nada";
    try{
        //RequestBody rb  = new FormEncodingBuilder().add("usuario",user).add("password","123").add("empresa",empresa).add("departamento",depto).build();
        //res=Conexion.consultar("verificarLogin",rb);
        RequestBody rb = new FormEncodingBuilder().add("usuario",user).add("empresa",empresa).add("departamento",depto).build();
        res = Conexion.consultar("retornarActivos",rb);
        System.out.println(res);
    }catch(Exception e){
        System.out.println("HUBO ERROR");
    }
%>

<!DOCTYPE html>
<html lang="es">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<title>Modificar Activo | Panel de Control - Proyecto 1 - EDD - 1S-2017</title>
	<meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
	<link rel="stylesheet" type="text/css" href="recursos/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="recursos/style.css">
</head>
<body>
	<header>
		<div class="container">
			<% 
                        out.println("<h1>Bienvenido "+user+"</h1>");
                    %>
			Modificar Activos
			<br>
			<br>
		</div>
	</header>

	<div class="container">

		
		<section class="main row">

			<article class="col-xs-1 col-sm-1  col-md-2">
				
			</article>

			<article class="login col-xs-10 col-sm-10  col-md-8">
				<center>
					<form method="POST" action="modify">
						<br>
						<br>
						<label>Id del producto:</label>
						<br>
						<select name="productos" id="prods" onchange="selecOp()">
                                                    
                                                    <%
                                                        
                                                        
                                                        
                                                        String productos[] = res.split(",");
                                                        for(int i =0;i<productos.length;i=i+3){
                                                            out.println("<option>"+productos[i]+"</option>");
                                                        }
                                                        out.println("</select>");
                                                        out.println("<script type=\"text/javascript\">");
                                                        out.println("function selecOp()");
                                                        out.println("{");
                                                        out.println("var op=document.getElementById(\"prods\");");
                                                        out.println("var tt=document.getElementById(\"nameProd\");");
                                                        out.println("var dd=document.getElementById(\"desc\");");
                                                        int k=0;
                                                        for(int i =0;i<productos.length;i+=3){
                                                            out.println("if (op.selectedIndex=="+k+")tt.value=\""+productos[i+1]+"\";");
                                                            out.println("if (op.selectedIndex=="+k+")dd.value=\""+productos[i+2]+"\";");
                                                            k++;
                                                        }
                                                        out.println("}");
                                                        out.println("</script>");
                                                    
                                                    %>
						<br>
						<br>
						<label>Nombre:&nbsp;</label>
						<br>
						<input type="text" id="nameProd" name="nameProd" placeholder="Nombre del Producto" disabled="disabled">
						<br>
						<br>
						<label>Descripción:</label>
						<br>
						<textarea id="desc" class="col-xs-12 col-sm-12  col-md-12" name="desc" rows="6" cols="60" placeholder="Descripción del prodcuto"></textarea>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<input type="submit" name="modifyProd" value="Modificar Activo">
						<br>
						<br>
						<br>
						<br>
						<br>
						<a href="control-panel.jsp"><b>Regresar al Menú</b></a>
						<br>
						<br>
						<br>
					</form>
				</center>
			</article>

			<article class="col-xs-1 col-sm-1  col-md-2">
				
			</article>
		</section>

	</div>

	<footer>
		<div class="container">
			<article class="col-xs-12 col-sm-6  col-md-3">
				<h3>Estrucutras de Datos</h3>
				<h4>1er Semestre 2017</h4>
				Proyecto 1 <br><br> 	
			</article>

			<article class="col-xs-12 col-sm-6  col-md-3">
			</article>
			
			<article class="col-xs-12 col-sm-6  col-md-3">
				<h3>Estudiantes:</h3>
				<h4>Rafael Antonio Morales Donis</h4>
				<h4>Samuel Alberto Perez Jimenez</h4>
			</article>

			<article class="col-xs-12 col-sm-6  col-md-3">
				<h3>&nbsp;</h3>
				<h4>Jose Ottoniel Santos Barrios</h4>
				<h4>Kevin Manuel Mejía Grajeda</h4>
			</article>

		</div>
	</footer>

	<script type="text/javascript" src="recursos/js/jquery.js"></script>
	<script type="text/javascript" src="recursos/js/bootstrap.min.js"></script>
</body>
</html>
