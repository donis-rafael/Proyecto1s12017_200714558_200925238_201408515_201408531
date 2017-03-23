<%-- 
    Document   : index
    Created on : 20/03/2017, 10:35:11 PM
    Author     : KMMG
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@page session="true"%>
<!DOCTYPE html>
<html lang="es">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<title>Inicio | Log in - Proyecto 1 - EDD - 1S-2017</title>
	<meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
	<link rel="stylesheet" type="text/css" href="recursos/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="recursos/style.css">
</head>
<body>
	<header>
		<div class="container">
			<h1>Bienvenido</h1>
			Administrador de Servicio de Rentas
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
					<form method="POST" action="login">
						<br>
						<br>
						<label>Usuario:</label>
						<br>
						<input type="text" name="user" placeholder="Usuario">
						<br>
						<br>
						<label>Contraseña:&nbsp;</label>
						<br>
						<input type="password" name="pass" placeholder="Contraseña">
						<br>
						<br>
						<label>Empresa:</label>
						<br>
						<input type="text" name="empresa" placeholder="Empresa de Trabaja">
						<br>
						<br>
						<label>Departamento:</label>
						<br>
						<input type="text" name="depto" placeholder="Departamento de Trabajo">
						<br>
						<br>
						<input type="submit" name="ingresar" values="Ingresar">
						<input type="reset" name="Limpiar Campos">
						<br>
						<br>
						<a href="sign-up.jsp"><b>Registrar Usuario</b></a>
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

