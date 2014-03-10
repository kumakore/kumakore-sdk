using UnityEngine;
using System.Collections;

public class HomeActivity : MonoBehaviour {
	
	public Vector2 scrollPos = Vector2.zero;

	public GUISkin skin;

	void OnGUI () {

		GUI.skin = skin;

		guiHomeArea ();
	}

	private void guiHomeArea () {
		
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));

		guiHeaderArea ();

		guiBodyScroll ();
		
		GUILayout.EndArea ();
	}
	
	private void guiHeaderArea() {
		
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height*0.25f));

		guiNewsButton ();

		guiStoreButton ();

		GUILayout.EndArea ();
	}

	private void guiBodyScroll() {
		
		scrollPos = GUI.BeginScrollView(new Rect(0, Screen.height*0.25f, Screen.width, Screen.height*0.75f), 
		                                scrollPos, new Rect(0, 0, 220, 200));
		
		guiCreateAGameButton ();

		GUI.EndScrollView();
	}

	private void guiCreateAGameButton() {
		GUI.Button(new Rect(0, 0, 100, 20), "Create a Game");
	}

	private void guiNewsButton() {
		
	}
	
	private void guiStoreButton() {
		
	}

	private void guiMatchesArea() {
		guiYourTurns ();
		guiTheirTurns ();
	}

	private void guiYourTurns() {
		
	}

	private void guiTheirTurns() {
		
	}
}
