package rafaelmorales.practica.com.appproyecto1;

import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.NumberPicker;
import com.squareup.okhttp.*;
import java.net.MalformedURLException;
import java.net.URL;

public class Rentar extends AppCompatActivity {
    String numeroDeDias="";
    public static OkHttpClient clienteWeb = new OkHttpClient();
    ProgressDialog cuadroDeDialogo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getWindow().requestFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_rentar);

        NumberPicker np = (NumberPicker) findViewById(R.id.np);
        Button rent = (Button) findViewById(R.id.btnRentar);

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

        rent.setOnClickListener(new View.OnClickListener(){

            @Override
            public void onClick(View v) {
                RequestBody formBody = new FormEncodingBuilder()
                        .add("valor", " ")
                        .build();

                AsyncCallWSRes conectarMetodo2 = new AsyncCallWSRes();
                conectarMetodo2.execute();
            }
        });
    }

    private class AsyncCallWSRes extends AsyncTask<RequestBody, Void, Void> {
        /*@Override
        protected Void doInBackground(Void... params){
            Log.i("Vik", "doInBackground");

            //Coneccion_Flask();

            return null;
        }*/

        @Override
        protected Void doInBackground(RequestBody... params) {
            Log.i("Vik", "doInBackground");

            Coneccion_Flask("", params[0]);

            return null;
        }

        @Override
        protected void onPostExecute(Void result) {
            Log.i("Vik", "onPostExecute");

            //LLENAR COMBOBOX
            //addItemsOnSpinnerDynamic();
            //spinner.setAdapter(adapter);

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
            URL url = new URL("http://0.0.0.0:5000/" + metodo);
            Request req = new Request.Builder().url(url).post(formBody).build();
            Response resp = clienteWeb.newCall(req).execute();
            String respuesta = resp.body().string();
            //System.out.println(respuesta);
            //return respuesta;
        } catch(MalformedURLException ex){
            //
        }catch(Exception ex){
            //
        }
    }
}
