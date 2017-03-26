/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package reportesedd;

import com.squareup.okhttp.FormEncodingBuilder;
import com.squareup.okhttp.RequestBody;

/**
 *
 * @author KMMG
 */
public class ReportesEDD {

    /**
     * @param args the command line arguments
     */
    
    static RequestBody rb;
    
    
    public static void main(String[] args) {
        // TODO code application logic here
        
        rb = new FormEncodingBuilder().add("p","p").build();//agregar parametros
        String usuarios = Conexion.consultarConString("listarUsuarios",rb);
        
        rb = new FormEncodingBuilder().add("p","p").build();//agregar parametros
        String empresas = Conexion.consultarConString("empresas",rb);
        
        rb = new FormEncodingBuilder().add("p","p").build();//agregar parametros
        String deptos = Conexion.consultarConString("departamentos",rb);
        
        Reportes r = new Reportes(usuarios, empresas, deptos);
        r.setVisible(true);
        

    }
    
}
