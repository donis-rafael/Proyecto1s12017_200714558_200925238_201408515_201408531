/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package conexion;

import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author KMMG
 */
public class Conexion {
    
    static OkHttpClient cliente = new OkHttpClient();
    
    public static String consultar(String nom, RequestBody rq){
        String respues="no hay";
        try {
            URL uu = new URL("http://192.168.43.180:5000/"+nom);
            Request t = new Request.Builder().url(uu).post(rq).build();
            Response rr = cliente.newCall(t).execute();
            respues = rr.body().string();
        } catch (MalformedURLException ex) {
            Logger.getLogger(Conexion.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IOException ex) {
            Logger.getLogger(Conexion.class.getName()).log(Level.SEVERE, null, ex);
        }
        
            return respues;
    }
    
    public static void Conection_API_EliminarB(String parametro){

        
        try {
            String path="http://192.168.43.122:58402/api/btree/?"+parametro;
            System.out.println(path);
            URL url = new URL(path);
            //URL url = new URL("http://localhost:65359/api/btree");
            HttpURLConnection coneccion = (HttpURLConnection) url.openConnection();
            coneccion.setRequestMethod("GET");
            coneccion.connect();
            InputStream is = coneccion.getInputStream();
            
            coneccion.disconnect();
            System.out.println("***************************************************** ");

        }catch (Exception e){

        }
        System.out.println("termino conexion");
    }
    
    
}
