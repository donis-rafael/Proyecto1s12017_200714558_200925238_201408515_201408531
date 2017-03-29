package rafaelmorales.practica.com.appproyecto1;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.Spinner;
import android.widget.TextView;

import com.squareup.okhttp.*;

import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;
import java.util.Random;

public class Rentar extends AppCompatActivity {
    String numeroDeDias="";
    public static OkHttpClient clienteWeb = new OkHttpClient();
    ProgressDialog cuadroDeDialogo;
    public String respuesta="", user = MenuInicial.usuario, depto = MenuInicial.departamento, enterprice = MenuInicial.empresa, idActivo="";
    public static String todosDatos[], ids[], descripciones[], nombres[];
    String cadena;
    Spinner spin;
    TextView txtNombre, txtDescrip;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getWindow().requestFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_rentar);

        NumberPicker np = (NumberPicker) findViewById(R.id.np);
        Button rent = (Button) findViewById(R.id.btnRentar);
        Button regresar = (Button) findViewById(R.id.btnRegresar);
        spin = (Spinner) findViewById(R.id.spinProducto);
        txtNombre = (TextView) findViewById(R.id.txtNombre);
        txtDescrip = (TextView) findViewById(R.id.txtDescripcion);

        AsyncCallWSRes conectarMetodo2 = new AsyncCallWSRes();
        conectarMetodo2.execute();

        //Populate NumberPicker values from minimum and maximum value range
        //Set the minimum value of NumberPicker
        np.setMinValue(0);
        //Specify the maximum value/number of NumberPicker
        np.setMaxValue(100);

        //Gets whether the selector wheel wraps when reaching the min/max value.
        np.setWrapSelectorWheel(true);

        //Set a value change listener for NumberPicker
        np.setOnValueChangedListener(new NumberPicker.OnValueChangeListener() {
            @Override
            public void onValueChange(NumberPicker picker, int oldVal, int newVal){
                //Display the newly selected number from picker
                numeroDeDias= "" + newVal;
            }
        });

        regresar.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                regresaAmenu();
            }
        });

        rent.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                Calendar cal = new GregorianCalendar();
                Date date = cal.getTime();
                String idConsulta = getCadenaAlfanumAleatoria(15);

                cadena = idConsulta+","+idActivo+","+user+","+enterprice+","+depto+","+date.toString()+","+numeroDeDias;

                AsyncCallWSRes2 as = new AsyncCallWSRes2();
                as.execute();
            }
        });

    }

    public void regresaAmenu(){
        Intent intent = new Intent(Rentar.this, MenuInicial.class);
        startActivity(intent);
    }

    private class AsyncCallWSRes extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            Log.i("Vik", "doInBackground");


            RequestBody formBody = new FormEncodingBuilder()
                    .add("p", "p")
                    .build();

            Coneccion_Flask("activos", formBody);

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
            cuadroDeDialogo = new ProgressDialog(Rentar.this);
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

        } catch(MalformedURLException ex){
            System.out.println("NOOOO "+ex.toString());
        }catch(Exception ex){
            System.out.println("NOOOO "+ex.toString());
            //
        }
    }

    public void addItemsOnSpinnerDynamic() {

        List<String> dynamicList = new ArrayList<String>();
        int tamano=0, pos=0;
        if((respuesta != null) && (!respuesta.equals(""))) {
            todosDatos = respuesta.split("\n");

            for (int i = 0; i < todosDatos.length; i++) {
                String a[] = todosDatos[i].split(",");
                for(int j = 0; j < a.length; j = j + 3){
                    tamano++;
                }
            }
            ids = new String[tamano];
            descripciones = new String[tamano];
            nombres = new String[tamano];

            for (int i = 0; i < todosDatos.length; i++) {
                String a[] = todosDatos[i].split(",");
                for(int j = 0; j < a.length; j = j + 3){
                    ids[pos] = a[j];
                    nombres[pos] = a[j+1];
                    descripciones[pos] = a[j+2];
                    pos++;
                }
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
                txtNombre.setText(nombres[position]);
                txtDescrip.setText(descripciones[position]);
                idActivo = ids[position];
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }


    private class AsyncCallWSRes2 extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            Log.i("Vik", "doInBackground");

            Coneccion_API2();

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
            cuadroDeDialogo = new ProgressDialog(Rentar.this);
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

    public void Coneccion_API2(){//SI CONECTA
        try {
            URL url = new URL("http://192.168.43.122:58402/api/btree?cadena="+cadena);
            HttpURLConnection coneccion = (HttpURLConnection) url.openConnection();
            coneccion.setRequestMethod("GET");
            coneccion.connect();

            InputStream input = coneccion.getInputStream();
            int byt;
            String resultado = "";

            while((byt = input.read()) != -1){
                resultado += (char) byt;
            }

            Log.d("json api", resultado);

            regresaAmenu();

        }catch (Exception e){

        }
    }
}
