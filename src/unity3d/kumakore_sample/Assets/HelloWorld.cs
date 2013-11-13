using UnityEngine;
using System.Collections;
using com.kumakore;

public class HelloWorld : MonoBehaviour {
	
	void Start () {
		KumakoreApp app = new KumakoreApp("729683e2d7dc955fb32a44b1958f8d1b", 1383666947);
		
		app.signin("test@sinuouscode.com", "test").sync(delegate(ActionAppSignin action) {
			if (action.getStatusCode() == StatusCodes.SUCCESS) {
				Debug.Log("SUCCESS: " + action.getStatusMessage());
			} else {
				Debug.LogError("FAIL: " + action.getStatusMessage());				
			}
		});
	}
	
	void Update () {
	
	}
}
