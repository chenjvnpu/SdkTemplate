using UnityEngine;
using System;

public class AndroidSdkReceiver:MonoBehaviour
{
	private static AndroidSdkReceiver instance;
	public event Action<string> HandleLoginSucceeded;
	public event Action HandleLogoutSucceeded;
	public static AndroidSdkReceiver Instance {
		get {
			if (instance == null) {
				GameObject gob = new GameObject (typeof(AndroidSdkReceiver).Name);
				GameObject.DontDestroyOnLoad (gob);
				instance = gob.AddComponent<AndroidSdkReceiver> ();

			}
			return instance;

		}


	}

	public  void Init()
	{
		//		Debug.Log ("-----Init--------");
	}

	public void OnLoginSuccess(string deviceId)
	{
//		Debug.Log ("-----OnLoginSuccess--------");
		if (HandleLoginSucceeded != null) {
			HandleLoginSucceeded (deviceId);
		}
	}

	public void OnLogoutSuccess()
	{
//		Debug.Log ("----OnLogoutSuccess---------");
		if (HandleLogoutSucceeded != null) {
			HandleLogoutSucceeded ();
		}
	}

}


