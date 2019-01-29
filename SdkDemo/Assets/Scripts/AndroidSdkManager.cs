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


	public static AndroidSdkManager Instance {
		get {
			if (instance == null) {
				instance = new AndroidSdkManager ();
				instance.Init ();
			}
			return instance;
		}


	}

	void Init()
	{
		currentAndroidObj = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject> ("currentActivity"); 
		androidJavaObjProxy = new AndroidJavaObjectProxy (currentAndroidObj);
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
		AndroidSdkReceiver.Instance.HandleLoginSucceeded += OnLoginSuccess;
		androidJavaObjProxy.Call ("Login",account,password);

	}

	/// <summary> 登出 </summary>
	public void Logout(Action OnLogoutSuccess)
	{
		AndroidSdkReceiver.Instance.HandleLogoutSucceeded += OnLogoutSuccess;
		androidJavaObjProxy.Call ("Logout");
	}

}
