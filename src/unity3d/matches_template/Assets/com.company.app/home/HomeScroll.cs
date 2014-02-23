using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class HomeScroll : MonoBehaviour {

	private bool _active = false;
	private Vector3 _previousMousePos;
	public Vector3 _displacement = Vector3.zero;
	public float Factor = 1.0f;
	public Vector3 Velocity = Vector3.zero;

	void Start() {
		_active = false;
	}

	void OnMouseDown() {
		//Debug.Log ("OnMouseDown");
		_active = true;
	}

	void OnMouseUp() {
		//Debug.Log ("OnMouseUp");
		_active = false;
	}

	void FixedUpdate() {
		if (_active) {
			Vector3 currentMousePos = Input.mousePosition;
			_displacement = currentMousePos - _previousMousePos;
			_previousMousePos = currentMousePos;
			rigidbody2D.velocity = Factor * _displacement * Time.deltaTime;
		} else if (Mathf.Abs (rigidbody2D.velocity.magnitude) > 0.0f) {
			rigidbody2D.AddForce (-rigidbody2D.velocity);
		} else {
			rigidbody2D.velocity = Vector2.zero;
		}
		
		Velocity = rigidbody2D.velocity;
	}
}
