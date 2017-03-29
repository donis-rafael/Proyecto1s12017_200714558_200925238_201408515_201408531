/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package servlets;

import com.squareup.okhttp.FormEncodingBuilder;
import com.squareup.okhttp.RequestBody;
import conexion.Conexion;
import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Random;
import javax.servlet.http.HttpSession;

/**
 *
 * @author KMMG
 */
@WebServlet(name = "add", urlPatterns = {"/add"})
public class add extends HttpServlet {

    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            /* TODO output your page here. You may use following sample code. */
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>");
            out.println("<title>Servlet add</title>");            
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet add at " + request.getContextPath() + "</h1>");
            out.println("</body>");
            out.println("</html>");
        }
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            /* TODO output your page here. You may use following sample code. */
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>");
            out.println("<title>Servlet login</title>");            
            out.println("</head>");
            out.println("<body>");
            
            String nombre = request.getParameter("nameProd");
            String desc = request.getParameter("desc");
            String id = getCadenaAlfanumAleatoria(15);
            
            HttpSession s = request.getSession();
            String user = (String)s.getAttribute("user");
            String empresa = (String)s.getAttribute("empresa");
            String depto = (String)s.getAttribute("depto");
            System.out.println("---------------------------------------------------->"+user);
            
            boolean exito = agregarProd(user, empresa, depto, id,nombre,desc);  
            if(exito==true){
                out.println("<script type=\"text/javascript\">alert(\"El Activo se ha sido registrado\");"
                        + "window.location.href='add-article.jsp'; </script> ");
            }else if(exito==false){
                out.println("<script type=\"text/javascript\">alert(\"No se ha podido registrar el activo\");"
                        + "window.location.href='add-article.jsp'; </script> ");
            }
            
            
            out.println("</body>");
            out.println("</html>");
        }
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>
    
    String getCadenaAlfanumAleatoria (int longitud){
        String cadenaAleatoria = "";
        long milis = new java.util.GregorianCalendar().getTimeInMillis();
        Random rnd = new Random(milis);
        int i = 0;
        while ( i < longitud){
        char c = (char)rnd.nextInt(255);
        if ( (c >= '0' && c <='9') || (c >='A' && c <='Z') ){
        cadenaAleatoria += c;
        i ++;
        }
        }
        return cadenaAleatoria;
    }
    
    private static boolean agregarProd(String user, String empresa, String depto, String id, String nombre, String desc ) {
        RequestBody rb = new FormEncodingBuilder().add("nombreA",nombre).add("usuario",user).add("departamento",depto).add("empresa",empresa).add("descripcion",desc).add("id",id).build();
        String res = Conexion.consultar("insertarActivo",rb);
        System.out.println(res);
        if (res.equals("insertado")){
            return true;
        }else{
            return false;
        }
    }
    

}
