package rafaelmorales.practica.com.appproyecto1;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.squareup.okhttp.FormEncodingBuilder;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

public class Devoluciones extends AppCompatActivity {
    ProgressDialog cuadroDeDialogo;
    public String respuesta="",user = MenuInicial.usuario, depto = MenuInicial.departamento, enterprice = MenuInicial.empresa;
    public String todosDatos[], ids[], tiempo[];
    Spinner spin;
    TextView txtNombre, txtDescrip, txtTiempo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_devoluciones);

        spin = (Spinner) findViewById(R.id.spinner);
        txtNombre = (TextView) findViewById(R.id.textView7);
        txtDescrip = (TextView) findViewById(R.id.textView10);
        txtTiempo = (TextView) findViewById(R.id.txtTiempoRenta);
        Button regresar = (Button) findViewById(R.id.btnRegresar);

        AsyncCallWSRes conectarMetodo2 = new AsyncCallWSRes();
        conectarMetodo2.execute();

        regresar.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Devoluciones.this, MenuInicial.class);
                startActivity(intent);
            }
        });
    }

    private class AsyncCallWSRes extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            Log.i("Vik", "doInBackground");

            Coneccion_API2();

            return null;
        }

        @Override
        protected void onPostExecute(Void result) {
            Log.i("Vik", "onPostExecute");
            addItemsOnSpinnerDynamic();
            cuadroDeDialogo.dismiss();
        }


        @Override
        protected void onPreExecute() {
            Log.i("Vik", "onPreExecute");
            super.onPreExecute();
            cuadroDeDialogo = new ProgressDialog(Devoluciones.this);
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

    public void addItemsOnSpinnerDynamic() {

        List<String> dynamicList = new ArrayList<String>();
        int tamano=0, pos=0;
        if((respuesta != null) && (!respuesta.equals(""))) {
            todosDatos = respuesta.split("\n");

            for (int i = 0; i < todosDatos.length; i++) {
                tamano++;
            }
            ids = new String[tamano];
            tiempo = new String[tamano];

            for (int i = 0; i < todosDatos.length; i++) {
                String a[] = todosDatos[i].split(",");
                ids[i] = a[1];
                tiempo[i] = a[2];
            }

            for (int p = 0; p < ids.length ; p++){
                dynamicList.add(ids[p] + "");
            }

        }else{
            dynamicList.add("** No Hay Usuarios **");
        }

        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, dynamicList);
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spin.setAdapter(dataAdapter);

        spin.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                //codPaisOrigen = Integer.parseInt(codigosDePaises[position]);
                for(int i=0; i< Rentar.ids.length; i++){
                    if(ids[position].equals(Rentar.ids[i])){
                        txtNombre.setText(Rentar.nombres[i]);
                        txtDescrip.setText(Rentar.descripciones[i]);
                        txtTiempo.setText(tiempo[position]);
                        i = Rentar.ids.length;
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    public void Coneccion_API2(){//SI CONECTA
        try {
            String cadena = user+","+enterprice+","+depto;
            URL url = new URL("http://192.168.43.122:58402/api/btree?activos="+cadena);
            HttpURLConnection coneccion = (HttpURLConnection) url.openConnection();
            coneccion.setRequestMethod("GET");
            coneccion.connect();

            InputStream input = coneccion.getInputStream();
            int byt;

            while((byt = input.read()) != -1){
                respuesta += (char) byt;
            }

            Log.d("json api", respuesta);

        }catch (Exception e){

        }
    }

}
