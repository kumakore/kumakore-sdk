using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using Mono.Cecil;

namespace com.kumakore.utils
{
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Assembly,
                AllowMultiple = false,
                Inherited = true)]
		public class InjectAttribute : Attribute
		{
				private string name;
				public double version;
	
				public InjectAttribute ()
				{
						version = 1.0;
				}
		}

//is used to retrieve object instances as defined by provider, 
// instantiate types, invoke methods, and load modules.
		public sealed class Injector : MonoBehaviour
		{		


				private Dictionary<Type, BindingBase> _bindings = new Dictionary<Type, BindingBase> ();
				private Dictionary<Type, PropertyInfo> _properties = new Dictionary<Type, PropertyInfo> ();
				private HashSet<object> _objects = new HashSet<object> ();
			
				public Injector ()
				{
				}
	
				private Config _config;

				void Awake ()
				{
						DontDestroyOnLoad (gameObject);
//			Debug.Log ("Injector(" + GetType ().Name + ") loaded. ");
				}

				// Use this for initialization
				void Start ()
				{
				
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

				private void Inject ()
				{
					
				}

				private void Init ()
				{

						foreach (Assembly asm in GetAssembliesWithInjectAttribute()) {

				
								foreach (Type type in GetTypesWithInjectAttribute(asm)) {
										foreach (PropertyInfo prop in GetPropertiesWithInjectAttribute(type)) {
												_properties [type] = prop;
										}
								}
						}
				}

				public void Load (Config config)
				{
						Init ();
						if (_config != null) {
								Unload ();
						}

						if (config != null) {
								_config = config;
								_config.Load (this);
						}
				}

				public void Unload ()
				{
						if (_config != null)
								_config.Unload ();
			
						_config = null;
				}

				public TDependency Get<TDependency>() {
					return Bind<TDependency> ().Get();
				}

//				public IEnumerable<TDependency> GetAll<TDependency>() {
//					return Bind<TDependency> ().GetAll();
//				}
				
				public bool HasBinding<TDependency> ()
				{
						return HasBinding (typeof(TDependency));
				}
		
				public bool HasBinding (Type typeDependency)
				{
						return _bindings.ContainsKey (typeDependency);
				}

				public Binding<TDependency> Bind<TDependency> ()
				{
						Type typeDependency = typeof(TDependency);

						if (!HasBinding (typeDependency)) {
								_bindings [typeDependency] = new Binding<TDependency> ();
						}
			
						return _bindings [typeDependency] as Binding<TDependency>;
						//return Bind(typeof(TDependency)) as Binding<TDependency>;
				}

				private static readonly BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

				public static IEnumerable<Assembly> GetAssembliesWithInjectAttribute ()
				{
					
					foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
						if (asm.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
							//Debug.Log ("[Assembly] " + asm.FullName);
							yield return asm;
						}
					}
				}

				public static IEnumerable<Assembly> GetAssembliesWithInjectAttributeV2 ()
		{
			
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
				AssemblyDefinition asmDef = AssemblyDefinition.ReadAssembly (asm.Location); 
							

								if (asm.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
										//Debug.Log ("[Assembly] " + asm.FullName);
										yield return asm;
								}
						}
				}

				public static IEnumerable<Type> GetTypesWithInjectAttribute (Assembly asm)
				{

						foreach (Type type in asm.GetTypes()) {
								if (type.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
//					Debug.Log ("[Type] " + type.Name);
										yield return type;
								}
						}
				}
		
				public static IEnumerable<PropertyInfo> GetPropertiesWithInjectAttribute (Type type)
				{

						foreach (PropertyInfo prop in type.GetProperties(FLAGS)) {
								if (prop.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
										Debug.Log ("[Property] " + prop.Name);
										yield return prop;
								}
						}
				}
		
				public static IEnumerable<PropertyInfo> GetInjectProperties ()
				{
						foreach (Assembly asm in GetAssembliesWithInjectAttribute()) {
								foreach (Type type in GetTypesWithInjectAttribute(asm)) {
										foreach (PropertyInfo prop in GetPropertiesWithInjectAttribute(type)) {
												yield return prop;
										}
								}
						}
				}

				[Obsolete]
				public static IEnumerable<PropertyInfo> GetInjectAttributesV2 ()
				{

						foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
								if (asm.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
										//Debug.Log ("[Assembly] " + asm.FullName);
										foreach (Type type in asm.GetTypes()) {
												if (type.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
														//Debug.Log ("[Type] " + type.Name);
														foreach (PropertyInfo prop in type.GetProperties(FLAGS)) {
																if (prop.GetCustomAttributes (typeof(InjectAttribute), true).Length > 0) {
																		//Debug.Log ("[Property] " + prop.Name);
																		yield return prop;
																}
														}
												}
										}
								}
						}
				}


//		public BindingBase Bind(Type typeDependency) {
//
//			if (_bindings.ContainsKey (typeDependency)) {
//				return _bindings [typeDependency];
//			} else {
//				return _bindings[typeDependency] = new Binding<>
//			}
//		}

		#region Celi

				public static bool TryGetCustomAttribute (TypeDefinition type,
		                                          string attributeType, out CustomAttribute result)
				{
						result = null;
						if (!type.HasCustomAttributes)
								return false;
			
						foreach (CustomAttribute attribute in type.CustomAttributes) {
								if (attribute.AttributeType.FullName != attributeType)
										continue;
				
								result = attribute;
								return true;
						}
			
						return false;
				}

				public static void GetInjectAttribute (TypeDefinition type)
				{
						InjectAttribute ignoreAttribute;
//						if (!TryGetCustomAttribute (type, "com.kumakore.utils.InjectAttribute", out ignoreAttribute))
//								return string.Empty;
			
//						if (ignoreAttribute.ConstructorArguments.Count != 1)
//								return string.Empty;
//			
//						return (string)ignoreAttribute.ConstructorArguments [0].Value;
				}
    #endregion

		#region singleton
				private static Injector _instance;
				private static object _lock = new object ();
		
				public static Injector instance {
						get {
								if (_applicationIsQuitting) {
										Debug.LogWarning ("[Singleton] Instance '" + typeof(Injector) +
												"' already destroyed on application quit." +
												" Won't create again - returning null.");
										return null;
								}
				
								lock (_lock) {
										if (_instance == null) {
												_instance = (Injector)FindObjectOfType (typeof(Injector));
						
												if (FindObjectsOfType (typeof(Injector)).Length > 1) {
														Debug.LogError ("[Singleton] Something went really wrong " +
																" - there should never be more than 1 singleton!" +
																" Reopenning the scene might fix it.");
														return _instance;
												}
						
												if (_instance == null) {
														GameObject singleton = new GameObject ();
														_instance = singleton.AddComponent<Injector> ();
														singleton.name = typeof(Injector).ToString ();
							
														//							DontDestroyOnLoad(singleton);
//							
//														Debug.Log ("[Singleton] An instance of " + typeof(Injector) + 
//																" is needed in the scene, so '" + singleton +
//																"' was created with DontDestroyOnLoad.");
												} 
//													else {
//
//														Debug.Log ("[Singleton] Using instance already created: " +
//																_instance.gameObject.name);
//												}
										}
					
										return _instance;
								}
						}
				}
		
				private static bool _applicationIsQuitting = false;
		
				/// <summary>
				/// When Unity quits, it destroys objects in a random order.
				/// In principle, a Singleton is only destroyed when application quits.
				/// If any script calls Instance after it have been destroyed, 
				///   it will create a buggy ghost object that will stay on the Editor scene
				///   even after stopping playing the Application. Really bad!
				/// So, this was made to be sure we're not creating that buggy ghost object.
				/// </summary>
				public void OnDestroy ()
				{
						_applicationIsQuitting = true;
				}
		
		#endregion
		}

}