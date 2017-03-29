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
import javax.servlet.http.HttpSession;

/**
 *
 * @author KMMG
 */
@WebServlet(name = "modify", urlPatterns = {"/modify"})
public class modify extends HttpServlet {

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
            out.println("<title>Servlet modify</title>");            
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>Servlet modify at " + request.getContextPath() + "</h1>");
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
            
            
            String id = request.getParameter("productos");
            String desc = request.getParameter("desc");
            
            HttpSession s = request.getSession();
            String user = (String)s.getAttribute("user");
            String empresa = (String)s.getAttribute("empresa");
            String depto = (String)s.getAttribute("depto");
            
            boolean exito = modificarActivo(user,empresa,depto,id,desc);            
            if(exito==true){
                out.println("<script type=\"text/javascript\">alert(\"Se modificó el activo\");"
                        + "window.location.href='modify-article.jsp'; </script> ");
            }else if(exito==false){
                out.println("<script type=\"text/javascript\">alert(\"No se ha podido modificar el activo\");"
                        + "window.location.href='modify-article.jsp'; </script> ");
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
    
    private static boolean modificarActivo(String user, String empresa, String depto, String id, String desc ) {
        RequestBody rb = new FormEncodingBuilder().add("usuario",user).add("departamento",depto).add("empresa",empresa).add("descripcion",desc).add("id",id).build();
        String res = Conexion.consultar("modificarActivo",rb);
        System.out.println(res);
        if (res.equals("modificado")){
            return true;
        }else{
            return false;
        }
    }

}
