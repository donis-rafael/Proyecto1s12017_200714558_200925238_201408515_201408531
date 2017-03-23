/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package reportes;

import com.squareup.okhttp.FormEncodingBuilder;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;
import java.awt.EventQueue;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;

/**
 *
 * @author Samuel
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

    public static boolean agregarProd() {
        RequestBody rb = new FormEncodingBuilder().add("p", "p" ).build();
        String res = Conexion.consultar("graficarMatriz",rb);
        System.out.println(res);
        if (res.equals("siii")){
            return true;
        }else{
            return false;
        }
    }
    
    
    public void Carga_Imagen() {
        EventQueue.invokeLater(new Runnable() {
            @Override
            public void run() {
                try {
                    UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
                } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException ex) {
                }

                try {
                    String path = "http://192.168.43.180:5000/matriz.png";
                    System.out.println("Obtengo imagen con path " + path);
                    URL url = new URL(path);
                    BufferedImage image = ImageIO.read(url);
                    System.out.println("Cargando imagen...");
                    JLabel label = new JLabel(new ImageIcon(image));
                    JFrame f = new JFrame();
                    f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
                    f.getContentPane().add(label);
                    f.pack();
                    f.setLocation(200, 200);
                    f.setVisible(true);
                } catch (Exception exp) {
                    exp.printStackTrace();
                }

            }
        });
    }
    
}
