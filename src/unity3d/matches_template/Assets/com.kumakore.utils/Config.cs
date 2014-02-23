using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace com.kumakore.utils {
	
public abstract class Config {
	
	private Injector _injector;
	protected Injector injector { get { return _injector; } }
//	public Config(Injector injector) {
//	}

	public void Load (Injector injector) {
		//			Debug.Log ("Config(" + GetType().Name + ") loaded.");
			_injector = injector;
		OnLoad ();
	}

	public void Unload () {
//		Debug.Log ("Config(" + GetType().Name + ") unloaded.");
				OnUnload ();
	}

		public abstract void OnLoad ();
	
		public abstract void OnUnload ();
}

}