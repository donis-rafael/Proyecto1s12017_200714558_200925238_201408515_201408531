<%-- 
    Document   : add-article
    Created on : 21/03/2017, 12:00:22 AM
    Author     : KMMG
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="es">
<head>
    <%
        HttpSession s = request.getSession();
        String user = (String)s.getAttribute("user");
        String empresa = (String)s.getAttribute("empresa");
        String depto = (String)s.getAttribute("depto");
            
    %>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<title>Agregar Activo | Panel de Control - Proyecto 1 - EDD - 1S-2017</title>
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
			Agregar Activos
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
					<form method="POST" action="add">
						<br>
						<br>
						<label>Nombre:&nbsp;</label>
						<br>
						<input type="text" name="nameProd" placeholder="Nombre del Producto">
						<br>
						<br>
						<label>Descripción:</label>
						<br>
						<textarea name="desc" rows="6" cols="60" placeholder="Descripción del prodcuto"></textarea>
						<br>
						<br>
						<input type="submit" name="createProd" value="Agregar Activo">
						<input type="reset" name="Limpiar Campos">
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
