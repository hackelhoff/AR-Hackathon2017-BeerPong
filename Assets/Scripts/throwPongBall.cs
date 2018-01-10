using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwPongBall : MonoBehaviour {

	public float ballThrowingForce = 30f;
	public GameObject playerCamera;

	public float ballDistance = .5f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = playerCamera.transform.position + playerCamera.transform.forward * ballDistance;

		if (Input.touchCount > 0) {
			Debug.Log ("touched");
			TossBall ();
		
		}


		// if ball tossed
			// reset, return to position

	}

	void TossBall() {
	
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<Rigidbody> ().AddForce (playerCamera.transform.forward * ballThrowingForce);
	
	}

//	void Reset() {
//
//		CancelInvoke ();
//		transform.position = playerCamera.transform.position + playerCamera.transform.forward * ballDistance;
//
//	}


}
