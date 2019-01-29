using UnityEngine;
using System;

public class AndroidSdkReceiver:MonoBehaviour
{
	public event Action<string> HandleLoginSucceeded;
	public event Action HandleLogoutSucceeded;

	public  void Initialize()
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


