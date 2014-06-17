#if UNITY_ANDROID
using System;
using System.Collections;
using UnityEngine;
using com.kumakore.plugin.util;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using com.kumakore;
using System.Collections.Generic;

namespace com.kumakore.plugin.android.http {

	/// <summary>
	/// Http cert validator.
	/// </summary>
	public class HttpCertValidator : ICertValidator {
//		private static readonly String TAG = typeof(HttpCertValidator).Name;

		public bool Validate(X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			
			//TODO:chbfiv
			//Not working ATM due to an issue with JNI connecting to the 
			//runtime inside this "Validate" function.
			return true;

			/*
			if (Application.platform == RuntimePlatform.Android) {
				try {
					List<string> certs = new List<string>();
					foreach(X509ChainElement element in chain.ChainElements)
					{
						string cert = Convert.ToBase64String(element.Certificate.GetRawCertData());
						certs.Add(cert);
					}
					
					string data = fastJSON.JSON.Instance.ToJSON(certs);
//					AndroidJNIHelper.debug = true;

					AndroidJNI.AttachCurrentThread();

					AndroidJavaClass securityClass = new AndroidJavaClass("com.kumakore.Security");

					return securityClass.CallStatic<bool>("checkCert", data);

				} catch (Exception ex) {
					String error = "Failed to checkCert; " + ex.Message;
					Kumakore.LOGE (TAG, error);		
					throw new InvalidOperationException (error);
				}
			} else if (Application.platform == RuntimePlatform.WindowsEditor) {
				return true;
			} else {
				Kumakore.LOGI (TAG, "Android Running in editor mode");
			}
			

			return false;
			*/
		}
	}
}

#endif
