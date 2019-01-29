using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 添加unity调用Android方法之前，判断方法是否存在，如果方法不存在，则在 c# 端输出错误。
/// 这里对参数类型进行过滤，支配置了bool，int，string，float类型数据
/// </summary>
public class AndroidJavaObjectProxy
{
	private AndroidJavaObject baseJavaObject;
	private Dictionary<string,string> ParamDict = new Dictionary<string, string> ();

	public AndroidJavaObjectProxy (AndroidJavaObject baseJavaObject)
	{
		this.baseJavaObject = baseJavaObject;
		InitDict ();
	}

	private void InitDict()
	{
		ParamDict["System.Boolean"] = "Z";
		ParamDict["System.Int32"] = "I";
		ParamDict["System.Single"] = "F";
		ParamDict["System.String"] = "Ljava/lang/String;";
	}

	public void Call(string methodName, params object[] args)
	{
		bool havaFunc = CheckHaveFunInAndroid (methodName,null,false,args);
		if(havaFunc){
			baseJavaObject.Call (methodName, args);
		}else{
			Debug.Log("The Android Method is Miss : " + methodName);
		}
	}

	public ReturnType Call<ReturnType>(string methodName, params object[] args)
	{
		bool havaFunc = CheckHaveFunInAndroid (methodName, typeof(ReturnType).ToString (), false, args);
		if (havaFunc) {
			return baseJavaObject.Call<ReturnType> (methodName,args);
		} else {
			Debug.Log("The Android Method is Miss : " + methodName);
		}
		return default(ReturnType);
	}

	public void CallStatic(string methodName, params object[] args)
	{
		bool havaFunc = CheckHaveFunInAndroid (methodName,null,true,args);
		if(havaFunc){
			baseJavaObject.CallStatic (methodName, args);
		}else{
			Debug.Log("The Android Method is Miss : " + methodName);
		}
	}

	public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
	{
		bool havaFunc = CheckHaveFunInAndroid (methodName, typeof(ReturnType).ToString (), true, args);
		if (havaFunc) {
			return baseJavaObject.CallStatic<ReturnType> (methodName,args);
		} else {
			Debug.Log("The Android Method is Miss : " + methodName);
		}
		return default(ReturnType);
	}

	/// <summary>
	/// 检查函数是否存在
	/// </summary>
	/// <param name="methodName">函数名</param>
	/// <param name="returnType">返回类型的Type</param>
	/// <param name="isStatic">是否是静态函数</param>
	/// <param name="args">所有参数</param>
	/// <returns></returns>
	private bool CheckHaveFunInAndroid(string methodName, string returnType, bool isStatic, params object[] args)
	{
		string sigStr = GetSigStr(returnType, args);
		if (string.IsNullOrEmpty(sigStr))
		{
			Debug.Log("the Android Method args is wrong");
			return false;
		}
		IntPtr fid;
		//通过方法名称和参数，获取方法的id，如果方法不存在，fid 为 new IntPtr(0)
		if (isStatic)
		{
			fid = AndroidJNI.GetStaticMethodID(baseJavaObject.GetRawClass(), methodName, sigStr);
		}
		else
		{
			fid = AndroidJNI.GetMethodID(baseJavaObject.GetRawClass(), methodName, sigStr);
		}

		if (fid != new IntPtr(0))
		{
			return true;
		}
		else
		{
			AndroidJNI.ExceptionClear();
			return false;
		}
	}

	/// <summary>
	/// 获得参数类型返回的Sign签名
	/// </summary>
	/// <param name="typeStr"></param>
	/// <returns></returns>
	private string GetReturnStr(string typeStr)
	{
		string str = "";
		if (ParamDict.Count == 0)
		{
			InitDict();
		}
		if (ParamDict.ContainsKey(typeStr))
		{
			str = ParamDict[typeStr];
		}
		return str;
	}

	private string GetParameterStr(object obj)
	{
		string str = "";
		str = GetReturnStr(obj.GetType().ToString());
		return str;
	}

	private string GetSigStr(string returnType, params object[] args)
	{
		string startStr = "(";
		string middle = ")";
		string parameterStr = "";
		string tempStr = "";
		for (int i = 0; i < args.Length; i++)
		{
			tempStr = GetParameterStr(args[i]);
			if (tempStr == "")
			{
				return "";
			}
			parameterStr = parameterStr + tempStr;
		}
		string endStr = "";
		if (returnType == null)
		{
			endStr = "V";
		}
		else
		{
			endStr = GetReturnStr(returnType);
			if (endStr == "")
			{
				return "";
			}
		}

		return startStr + parameterStr + middle + endStr;
	}

}


