using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreArea : MonoBehaviour {

	public GameObject effectObject;
	public GameObject cups;
	public GameObject activeScoreText;


	void Start () {
		effectObject.SetActive (false);
//		cupsMade.text = "Cups Made: " + cupCount.ToString ();
	}

	void CloseScoreArea() {
	
		cups.SetActive (false);
		activeScoreText.SetActive (false);
	
	}


	void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.GetComponent<SphereCollider> () != null) {
			effectObject.SetActive (true);
			activeScoreText.SetActive (true);

			Invoke ("CloseScoreArea", 3.0f);

		} 

	}

}
