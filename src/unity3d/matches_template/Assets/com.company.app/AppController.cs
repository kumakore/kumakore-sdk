using UnityEngine;
using System.Collections;
using com.kumakore.utils;

[assembly:Inject()]

[RequireComponent(typeof(Animator))]
[Inject]
public class AppController : MonoBehaviour
{
		public GameObject ViewContainer;

		[Inject]
//	public IAppService app { get; set; }
		public IAppService app { get { return Injector.instance.Get<IAppService>(); } }

//		[Inject]
		AppController () {

		}

		// Use this for initialization
		void Awake ()
		{

			Injector.instance.Load (new CompanyAppConfig1 ());
			
//				StartCoroutine (ChangeState ()); 
		}

		void Start ()
		{
			
			//				StartCoroutine (ChangeState ()); 
		}
	
		// Update is called once per frame
		void Update ()
		{
//				foreach (Transform child in ViewContainer.transform) {
//			if (child.name == _app.CurrentMode.ToString ()) {
//								child.gameObject.SetActive (true);
//						} else {
//								child.gameObject.SetActive (false);
//						}
//				}
		}

		IEnumerator ChangeState ()
		{
//				_app.CurrentMode = AppModes.brand;
				yield return new WaitForSeconds (1.5f);
//				_app.CurrentMode = AppModes.loading;
//				yield return new WaitForSeconds (3.0f);
//				_app.CurrentMode = AppModes.home;



//				yield return new WaitForSeconds (3.0f);
//				CurrentMode = AppModes.game;
//				Application.LoadLevelAdditive("Level");
//				yield return new WaitForSeconds (3.0f);
//				CurrentMode = AppModes.loading;
//				GameObject.DestroyImmediate (GameContainer);
//				yield return new WaitForSeconds (3.0f);
//				Application.LoadLevelAdditive("Level");
//				CurrentMode = AppModes.home;
//				GameObject.DestroyImmediate (GameContainer);
		}
}
