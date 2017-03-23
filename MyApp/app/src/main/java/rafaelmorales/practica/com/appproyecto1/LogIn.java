package rafaelmorales.practica.com.appproyecto1;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.squareup.okhttp.FormEncodingBuilder;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import java.net.MalformedURLException;
import java.net.URL;

public class LogIn extends AppCompatActivity {
    String user="";
    String pass="";
    String enterprice="";
    String depto="";
    EditText txtUsuario, txtContrasena, txtEmpresa, txtDepartamento;
    public static OkHttpClient clienteWeb = new OkHttpClient();
    ProgressDialog cuadroDeDialogo;
    String respuesta="";
    //Toast tos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getWindow().requestFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_log_in);

        txtUsuario = (EditText) findViewById(R.id.txtUsuario);
        txtContrasena = (EditText) findViewById(R.id.txtPass);
        txtEmpresa = (EditText) findViewById(R.id.txtEmpresa);
        txtDepartamento = (EditText) findViewById(R.id.txtDepto);
        Button login = (Button) findViewById(R.id.btnLogin);


        login.setOnClickListener(new View.OnClickListener(){

            @Override
            public void onClick(View v) {


                user = txtUsuario.getText().toString();
                pass = txtContrasena.getText().toString();
                enterprice = txtEmpresa.getText().toString();
                depto = txtDepartamento.getText().toString();


                AsyncCallWSRes conectarMetodo2 = new AsyncCallWSRes();
                conectarMetodo2.execute();

            }
        });
    }

    private class AsyncCallWSRes extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            Log.i("Vik", "doInBackground");


            RequestBody formBody = new FormEncodingBuilder()
                    .add("usuario", user)
                    .add("password", pass)
                    .add("empresa", enterprice)
                    .add("departamento", depto)
                    .build();

            Coneccion_Flask("verificarLogin", formBody);

            return null;
        }

        @Override
        protected void onPostExecute(Void result) {
            Log.i("Vik", "onPostExecute");
            cuadroDeDialogo.dismiss();
        }


        @Override
        protected void onPreExecute() {
            Log.i("Vik", "onPreExecute");
            super.onPreExecute();
            cuadroDeDialogo = new ProgressDialog(LogIn.this);
            cuadroDeDialogo.setMessage("Cargando...");
            cuadroDeDialogo.setIndeterminate(false);
            cuadroDeDialogo.setProgressStyle(ProgressDialog.STYLE_SPINNER);
            cuadroDeDialogo.setCancelable(true);
            cuadroDeDialogo.show();
        }

        @Override
        protected void onProgressUpdate(Void... values) {
            Log.i("Vik", "onProgressUpdate");
        }
    }

    public void Coneccion_Flask(String metodo, RequestBody formBody){
        try{
            URL url = new URL("http://192.168.43.180:5000/" + metodo);
            Request req = new Request.Builder().url(url).post(formBody).build();
            Response resp = clienteWeb.newCall(req).execute();
            respuesta = resp.body().string();
            //System.out.println(respuesta);

            //return respuesta;

            if(respuesta.equals("existe")){
                Intent intent = new Intent(LogIn.this, MenuInicial.class);
                startActivity(intent);
            }else{
                AlertDialog.Builder build = new AlertDialog.Builder(LogIn.this);
                build.setMessage("Información Incorrecta")
                        .setTitle("¡ ATENCION !")
                        .setCancelable(false)
                        .setNeutralButton("Aceptar", new DialogInterface.OnClickListener(){

                            @Override
                            public void onClick(DialogInterface dialog, int which) {
                                dialog.cancel();
                            }
                        });
                AlertDialog alerta = build.create();
                alerta.show();
                //tos = Toast.makeText(LogIn.this, "Información Incorrecta", Toast.LENGTH_SHORT);
                //tos.show();
            }

        } catch(MalformedURLException ex){
            System.out.println("NOOOO "+ex.toString());
        }catch(Exception ex){
            System.out.println("NOOOO "+ex.toString());
            //
        }
    }
}
