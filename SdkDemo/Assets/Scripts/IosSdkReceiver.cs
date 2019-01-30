using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IOSSdkReceiver : MonoBehaviour {


	public event Action<string> HandleLoginSucceeded;
	public event Action HandleLogoutSucceeded;

	public void OnLoginSuccess(string deviceId)
	{
		if(HandleLoginSucceeded!=null){
			HandleLoginSucceeded (deviceId);
		}

	}

	public void OnLogoutSuccess()
	{
		if(HandleLogoutSucceeded!=null){
			HandleLogoutSucceeded ();
		}

	}





}
