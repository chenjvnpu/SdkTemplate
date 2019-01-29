﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SdkTestUIManager : MonoBehaviour {
	
	[SerializeField]
	Text messageLabel;

	[SerializeField]
	Button initBtn;

	[SerializeField]
	Button loginBtn;

	[SerializeField]
	Button logoutBtn;

	[SerializeField]
	Button testBtn;



	void Start () {
		initBtn.onClick.AddListener (OnInitBtn);
		loginBtn.onClick.AddListener (OnLoginBtn);
		logoutBtn.onClick.AddListener (OnLogoutBtn);
		Application.logMessageReceived += LogHander ;

	}

	void LogHander(string condition, string stackTrace, LogType type)
	{
		string logMessage;
		switch (type) {
		case LogType.Error:
		case LogType.Exception:
			logMessage = condition + stackTrace;
			break;
		case LogType.Log:
		case LogType.Warning:
			logMessage = condition;
			break;
		default:
				logMessage = "";
				break;
		}
		logMessage += "\n";
		messageLabel.text = logMessage+messageLabel.text;
	}

	void OnInitBtn()
	{
		Debug.Log ("---OnInitBtn------");
		AndroidSdkReceiver.Instance.Init();
		AndroidSdkManager.Instance.SetDebugable (true);
		 string packageName = AndroidSdkManager.Instance.getPackageName();
		Debug.Log ("--------> packageName is "+packageName);
	}

	void OnLoginBtn()
	{
		Debug.Log ("-----OnLoginBtn----");
		AndroidSdkManager.Instance.Login ("zhang san","123456",(deviceId)=>{
			Debug.Log("--------> unity receive login success message,deviceId is "+deviceId);
		});
	}

	void OnLogoutBtn()
	{
		Debug.Log ("----OnLogoutBtn-----");
		AndroidSdkManager.Instance.Logout (()=>{
			Debug.Log("--------> unity receive logout success message !");
//			Check();
		});
	}


	void OnGUI()
	{
		if(GUILayout.Button(" click normal")){
			Debug.Log ("this is a message ");
		}

		if(GUILayout.Button(" click warning")){
			Debug.Log ("this is a warning ");
		}

		if(GUILayout.Button(" click error")){
			Debug.Log ("this is a error ");
		}

		if(GUILayout.Button(" click exception")){
			Check ();
		}
	}

	void Check()
	{
		List<int> list = new List<int> ();
		list.Add (12);
		list.Add (22);
		list.Add (1322);
		list.Add (112);

		for (int i = 0; i < 4; i++) {
			list.RemoveAt (i);
		}
	}
















}
