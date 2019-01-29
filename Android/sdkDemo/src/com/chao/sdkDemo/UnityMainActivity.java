package com.chao.sdkDemo;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.util.Log;

public class UnityMainActivity extends UnityPlayerActivity
{

	static final String UnityReceiver = "AndroidSdkReceiver";
	static final String TAG = "UnityMainActivity";
	static final String OnLoginSuccess = "OnLoginSuccess";
	static final String OnLoginFaild = "OnLoginFaild";
	static final String OnLogoutSuccess = "OnLogoutSuccess";
	
	private boolean isDebug=false;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		initialize();
	}

	private void initialize() {
		Log.i(TAG, "initialize");
		
	}
	
	private void LogMessage (String message)
	{
		if(isDebug){
			Log.i(TAG, message);
		}
		
	}
	
	private void LogWarning (String message)
	{
		if(isDebug){
			Log.w(TAG, message);
		}
	}
	
	private void LogError (String message)
	{
		if(isDebug){
			Log.e(TAG, message);
		}
	}
	
	/*
	 * 设置是否是开发模式
	 */
	public void SetDebugState(boolean isDebug)
	{
		this.isDebug = isDebug;
	}
	
	/*
	 * 获取包名
	 */
	public String GetPackage()
	{
		return this.getPackageName();
	}
	
	/*
	 * 获取版本号
	 */
	public String GetVersion()
	{
		String versionName = "未获取到信息";
		try {
			versionName = this.getPackageManager().getPackageInfo(this.getPackageName(), 0).versionName;
		} catch (NameNotFoundException e) {
			e.printStackTrace();
		}
		return versionName;
	}
	
	/*
	 * 获取versionCode
	 */
	public int GetVersionCode()
	{
		int versionCode = 0;
		try {
			versionCode = this.getPackageManager()
						.getPackageInfo(this.getPackageName(), 0).versionCode;
		} catch (NameNotFoundException e) {
			e.printStackTrace();
		}
		return versionCode;
	}
	
	public void Login(String userName,String password)
	{
		this.LogMessage("do login ");
		String deviceId;
	    try {
	        TelephonyManager telephonyManager = (TelephonyManager) this.getSystemService(Context.TELEPHONY_SERVICE);
	        deviceId = telephonyManager.getDeviceId();
	    } catch (Exception e) {
	        deviceId = "";
	    }
		UnityPlayer.UnitySendMessage(this.UnityReceiver,this.OnLoginSuccess,deviceId);
	}
	
	public void Logout()
	{
		UnityPlayer.UnitySendMessage(this.UnityReceiver,this.OnLogoutSuccess,"");
	}
	
	public void Purchage(int purchaseId)
	{
		this.LogMessage("pruchaseId is "+purchaseId);
		
	}
	 
	
}
