using UnityEngine;
using System;

namespace com.kumakore.plugin.util {

	/// <summary>
	/// Be aware this will not prevent a non singleton constructor
	///   such as `T myT = new T();`
	/// To prevent that, add `protected T () {}` to your singleton class.
	/// 
	/// As a note, this is made as MonoBehaviour because we need Coroutines.
	/// </summary>
	public class SingletonBehav<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static readonly String TAG = typeof(SingletonBehav<T>).Name;	

		private static T _instance;
		protected static bool _applicationIsQuitting = false;
		
		protected static volatile object _lock = new object();

		public static T instance
		{
			get
			{
				if (_applicationIsQuitting) {
					Kumakore.LOGW(TAG,"[Singleton] Instance '"+ typeof(T) +
					              "' already destroyed on application quit." +
					              " Won't create again - returning null.");
					return null;
				}
				
				lock(_lock)
				{
					if (_instance == null)
					{
						_instance = (T) FindObjectOfType(typeof(T));
						
						if ( FindObjectsOfType(typeof(T)).Length > 1 )
						{
							Kumakore.LOGW(TAG,"[Singleton] Something went really wrong " +
							              " - there should never be more than 1 singleton!" +
							              " Reopenning the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = typeof(T).ToString();
							
							DontDestroyOnLoad(singleton);

							Kumakore.LOGI(TAG,"[Singleton] An instance of " + typeof(T) + 
							              " is needed in the scene, so '" + singleton +
							              "' was created with DontDestroyOnLoad.");
						} 
					}
					
					return _instance;
				}
			}
		}

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		private void OnDestroy () {
			_applicationIsQuitting = true;
		}

		private void OnApplicationQuit() {
			_applicationIsQuitting = true;
		}
	}
}