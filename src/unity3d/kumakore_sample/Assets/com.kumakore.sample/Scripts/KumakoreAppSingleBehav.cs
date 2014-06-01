using UnityEngine;
using System.Collections;
using System;
using com.kumakore.plugin.util;

namespace com.kumakore.sample {

	public sealed class KumakoreAppSingleBehav : SingletonBehav<KumakoreAppSingleBehav> {
		
		private static readonly string TAG = typeof(KumakoreAppSingleBehav).Name;	

		///NOTE: these are static readonly, not public instance fields.
		private static readonly string API_KEY = "XXXXXXXXXXXXXXXXXXXXXXXXXX"; 
		private static readonly string APP_VERSION = "0.0";
		//NOTE:: you can change the DASHBOARD_VERSION at runtime, but this is still required for
		// the static setup of KumakoreApp
		private static readonly int DASHBOARD_VERSION = 999999999;
		
		private static readonly bool CLEAR_CACHE = false;

		static KumakoreAppSingleBehav() {
			//type of auto-load
			if (CLEAR_CACHE) 
				app.delete();
			else
				app.load();
		}

		private static KumakoreApp _app;

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

		private void Update () {
			app.getDispatcher().dispatch();	
		}

		private void OnApplicationFocus(bool focus) {
			if (app != null)
				app.save ();
		}

		private void OnApplicationPause() {
			if (app != null)
				app.save ();
		}
	}
}