using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class IOSSdkManager  {
	private static IOSSdkManager instance;
	private IOSSdkReceiver iosReceiver;

	[DllImport("__Internal")]
	private static extern int _Sdk_Login (string account, string password);
	[DllImport("__Internal")]
	private static extern int _Sdk_Logout ();


	void InitReceiver()
	{
		iosReceiver = new GameObject ("IOSSdkReceiver").AddComponent<IOSSdkReceiver> ();
		GameObject.DontDestroyOnLoad (iosReceiver.gameObject);
		Debug.Log ("-----InitReceiver----------");
	}


	public static IOSSdkManager Instance {
		get {
			if (instance == null) {
				instance = new IOSSdkManager ();
			}
			return instance;
		}


	}

	public void Initialize(){
		if(iosReceiver == null){
			InitReceiver ();
		}
	}

	public void Login(string account,string password,Action<string> onLoginSuccess)
	{
		iosReceiver.HandleLoginSucceeded += onLoginSuccess;
		_Sdk_Login (account,password);
	}

	public void Logout(Action onLogoutSuccess)
	{
		iosReceiver.HandleLogoutSucceeded += onLogoutSuccess;
		_Sdk_Logout ();
	}



}
