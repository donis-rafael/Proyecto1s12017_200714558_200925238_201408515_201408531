/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package reportesedd;

import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;
import java.io.FileOutputStream;
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
    
    public static InputStream consultar(String nom, RequestBody rq){
        InputStream respues=null;
        try {
            URL uu = new URL("http://192.168.43.180:5000/"+nom);
            Request t = new Request.Builder().url(uu).post(rq).build();
            Response rr = cliente.newCall(t).execute();
            respues = rr.body().byteStream();
        } catch (MalformedURLException ex) {
            Logger.getLogger(Conexion.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IOException ex) {
            Logger.getLogger(Conexion.class.getName()).log(Level.SEVERE, null, ex);
        }
        
            return respues;
    }
    
    public static String consultarConString(String nom, RequestBody rq){
        String respues="No Hay";
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
    
    public static void Conection_API(String parametro){

        
        try {
            String path="http://192.168.43.122:58402/api/btree/?"+parametro;
            System.out.println(path);
            URL url = new URL(path);
            //URL url = new URL("http://localhost:65359/api/btree");
            HttpURLConnection coneccion = (HttpURLConnection) url.openConnection();
            coneccion.setRequestMethod("GET");
            coneccion.connect();
            InputStream is = coneccion.getInputStream();
            
            System.out.println("resuelto");
            coneccion.disconnect();
            //System.out.println("***************************************************** "+resultado);

        }catch (Exception e){

        }
        System.out.println("termino conexion");
    }
    
    public static void Conection_Llenar(String parametro){

        
        try {
            String path="http://192.168.43.122:58402/api/btree/?"+parametro;
            System.out.println(path);
            URL url = new URL(path);
            //URL url = new URL("http://localhost:65359/api/btree");
            HttpURLConnection coneccion = (HttpURLConnection) url.openConnection();
            System.out.println("resuelto  1 ");
            coneccion.setRequestMethod("GET");
            System.out.println("resuelto  2 ");
            coneccion.connect();
            System.out.println("resuelto  3");
            InputStream is = coneccion.getInputStream();
            System.out.println("resuelto  ");
            
            coneccion.disconnect();
            System.out.println("***************************************************** ");

        }catch (Exception e){

        }
        System.out.println("termino conexion");
    }
    
    
    
}
