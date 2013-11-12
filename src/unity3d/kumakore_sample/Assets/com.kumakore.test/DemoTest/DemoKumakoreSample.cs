using UnityEngine;
using System.Collections;
using com.kumakore;
using System;
using com.kumakore.test;
using System.Collections.Generic;

namespace com.kumakore.sample
{
	public class DemoKumakoreSample : MonoBehaviour
	{	
#pragma warning disable 0414
		private static readonly String TAG = typeof(DemoKumakoreSample).Name;
		private KumakoreApp _app;

		// Use this for initialization
		void Start ()
		{
			_app = new KumakoreApp (DemoHelpers.API_KEY,
				DemoHelpers.DASHBOARD_VERSION);
			
			_app.signin ("carlos.fernandez.musoles@gmail.com","password").sync ();
			
			
			//TestApp.All();
			//TestUser.All();
			//TestAchievements.All ();
			//TestProducts.All();
			//TestLeaderboards.All ();
			//TestDatastore.All();
			//TestInventory.All ();
			//TestMatches.All ();
			//TestDevice.All ();
		}
		
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}