using UnityEngine;
using System.Collections;
using System;

namespace com.kumakore.utils {
	
	public class Binding<TDependency> : BindingBase {
		private TDependency _dependency;
		//private TImplementation _implementation;

		public Binding() {

		} 

		public Binding<TDependency> ToSingleton<TImplementation>() where TImplementation : TDependency {
			return null;
		}
		
//		public object Get() {
//			return null;
//		}
}

}