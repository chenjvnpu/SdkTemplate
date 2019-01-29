package com.chao.sdkDemo;

import java.io.IOException;
import java.io.InputStream;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;


public class MainActivity extends Activity {
	private Handler mHandler = new Handler();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
		initialize();
		 mHandler.post(mRunnable);
    }

    void initialize(){
    	Log.i("Unity", "------------initialize---------");
    }
    
    public void LoadFile(){
    	Log.i("Unity", "------------LoadFile---------");
    }
    
    public byte[] getFromAssetss(String fileName){ //android��߶�ȡstreamingAssetĿ¼���ļ�
    	InputStream in;
    	byte [] buffer = null;
		try {
			in = getResources().getAssets().open(fileName);
			
			int length = in.available();
	    	buffer = new byte[length];
	    	in.read(buffer);
	    	in.close();
		} catch (IOException e) {
			e.printStackTrace();
			Log.e("Unity", "----------Android getFromAssetss error------");
		}
    	
    	return buffer;
    	}
    
    private Runnable mRunnable = new Runnable() {  
        public void run() {  
        	Log.e("Unity", "----------runnable----logFile.txt--");
        	byte[] buff = getFromAssetss("logFile.txt");
        	Log.i("Unity","length "+ buff.length);
        	mHandler.postDelayed(mRunnable, 3000); 
        }  
    };
    	


}
