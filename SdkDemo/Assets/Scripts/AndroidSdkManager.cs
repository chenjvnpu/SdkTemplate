using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AndroidSdkManager {

	private static AndroidSdkManager instance ;
	private AndroidSdkReceiver receiver;
	private delegate void RunPtr();

	AndroidJavaObject currentAndroidObj;
	AndroidJavaObjectProxy androidJavaObjProxy;

	void InitReceiver()
	{
		receiver = new GameObject ("AndroidSdkReceiver").AddComponent<AndroidSdkReceiver> ();
		GameObject.DontDestroyOnLoad (receiver.gameObject);
		receiver.Initialize ();

	}

	public static AndroidSdkManager Instance {
		get {
			if (instance == null) {
				instance = new AndroidSdkManager ();
			}
			return instance;
		}


	}

	public void Initialize()
	{
		currentAndroidObj = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject> ("currentActivity"); 
		androidJavaObjProxy = new AndroidJavaObjectProxy (currentAndroidObj);
		if (receiver == null) {
			InitReceiver ();
		}
	}




	// UI线程中运行  
	public void RunOnUIThread(Action action)
	{
		currentAndroidObj.Call("runOnUiThread", new AndroidJavaRunnable(new RunPtr(action)));
	}

	public void SetDebugable(bool isDebug)
	{
		androidJavaObjProxy.Call("SetDebugState",true);
	}

	/// <summary> 获取包名  </summary> 
	public string getPackageName()
	{
		return androidJavaObjProxy.Call<string>("GetPackage");
	}

	/// <summary> 登录 </summary>
	public void Login(string account,string password,Action<string> OnLoginSuccess)
	{
		receiver.HandleLoginSucceeded += OnLoginSuccess;
		androidJavaObjProxy.Call ("Login",account,password);

	}

	/// <summary> 登出 </summary>
	public void Logout(Action OnLogoutSuccess)
	{
		receiver.HandleLogoutSucceeded += OnLogoutSuccess;
		androidJavaObjProxy.Call ("Logout");
	}

}
