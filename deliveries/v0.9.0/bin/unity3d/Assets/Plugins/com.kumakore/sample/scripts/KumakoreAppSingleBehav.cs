using UnityEngine;
using System.Collections;
using System;
using com.kumakore.plugin.util;
#if UNITY_ANDROID
using com.kumakore.plugin.android.http;
#endif

namespace com.kumakore.sample {

	public sealed class KumakoreAppSingleBehav : SingletonBehav<KumakoreAppSingleBehav> {
		
		private static readonly string TAG = typeof(KumakoreAppSingleBehav).Name;	

		///NOTE: these are static readonly, not public instance fields.
		private static readonly string API_KEY = "efc3993b2afd05a99f978342604046bc"; 
		private static readonly string APP_VERSION = "0.0";
		//NOTE:: you can change the DASHBOARD_VERSION at runtime, but this is still required for
		// the static setup of KumakoreApp
		private static readonly int DASHBOARD_VERSION = 1400132926;
		
		private static readonly bool CLEAR_CACHE = true;

		static KumakoreAppSingleBehav() {
			//type of auto-load
			if (CLEAR_CACHE) 
                app.delete(DATA_PATH);
			else
                app.load(DATA_PATH);
		}

		private static KumakoreApp _app;

        private static String filename = "kkuser";
        
		private static readonly String DATA_PATH = Application.persistentDataPath + "//" + filename;

		public static KumakoreApp app
		{
			get
			{
				//force creation of mono behaviour singleton
				#pragma warning disable 0219
				KumakoreAppSingleBehav instance = KumakoreAppSingleBehav.instance;
				#pragma warning restore 0219

				if (_applicationIsQuitting) {
					Kumakore.LOGW(TAG,"[KumakoreAppMgr] Instance 'KumakoreApp'" +
					              " already destroyed on application quit." +
					              " Won't create again - returning null.");
					return null;
				}
				
				lock(_lock)
				{
					if (_app == null)
						_app = new KumakoreApp(API_KEY, APP_VERSION, DASHBOARD_VERSION);
					
					return _app;
				}
			}
		}

		private void Awake() {
			#if UNITY_ANDROID
			KumakoreHttpClient.registerCertValidator(new HttpCertValidator());
			#endif
		}

		private void Update () {
			app.getDispatcher().dispatch();	
		}

		private void OnApplicationFocus(bool focus) {
			if (app != null)
                app.save (DATA_PATH);
		}

		private void OnApplicationPause() {
			if (app != null)
                app.save (DATA_PATH);
		}
	}
}