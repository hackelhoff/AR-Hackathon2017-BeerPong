using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereScript : MonoBehaviour {
	[SerializeField]
	private float throwSpeed;
	private float speed;
	private float lastMouseX, lastMouseY;

	private bool thrown, holding;
	private bool makeThrow = false;

	private Rigidbody _rigidbody;
	private Vector3 newPosition;

	public Vector3 offset;
	public GameObject cups;

	public int throwCount;
	public Text ballsShot;

	public int cupCount;
	public Text cupsMade;

	public GameObject gameOverButton;
	public GameObject gameOverText;

//	public GameObject p1cups;
//	public GameObject p2cups;

//	public bool firstPlayer;



	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.1f, Camera.main.nearClipPlane * 7.5f));
		newPosition = transform.position;
		thrown = holding = false;

		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.Euler (0f, 200f, 0f);
		transform.SetParent (Camera.main.transform);
		throwCount = 0;

		ballsShot.text = "Balls Shot: " + throwCount.ToString ();
		cupsMade.text = "Cups Made: " + cupCount.ToString ();
	}

	// Update is called once per frame
	void Update () {

		offset = transform.position - cups.transform.position;

		if (holding) 
			OnTouch ();

		if (thrown)
			return;

		// when touch ball
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100f)) {
				if (hit.transform == transform) {
					holding = true;
					transform.SetParent (null);
				}
			}
		}

		// when loose ball
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			if (lastMouseY < Input.GetTouch (0).position.y) {
				ThrowBall (Input.GetTouch (0).position);
			}
		}

		// when finger is on screen
		if (Input.touchCount == 1) {
			lastMouseX = Input.GetTouch (0).position.x;
			lastMouseY = Input.GetTouch (0).position.y;
		}


		if (cupCount >= 6) {
			Invoke ("GameOver", 1.0f);
		}


	}

	void Reset() {
		CancelInvoke ();
		transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.1f, Camera.main.nearClipPlane * 7.5f));
		newPosition = transform.position;
		thrown = holding = false;

		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.Euler (0f, 200f, 0f);
		transform.SetParent (Camera.main.transform);

//		firstPlayer = !firstPlayer;

	}

	void OnTouch() {
		Vector3 mousePos = Input.GetTouch (0).position;
		mousePos.z = Camera.main.nearClipPlane * 7.5f;

		newPosition = Camera.main.ScreenToWorldPoint (mousePos);

		transform.localPosition = Vector3.Lerp (transform.localPosition, newPosition, 50f * Time.deltaTime);
	}

	void ThrowBall(Vector2 mousePos){
		_rigidbody.useGravity = true;

//		float differenceY = (mousePos.y - lastMouseY) / Screen.height * 100;
		// scale speed = difference between ball obj and cups obj
//		speed = throwSpeed;
//		speed = Mathf.Abs (offset.z * 200);
		speed = 200;

//		float x = (mousePos.x / Screen.width) - (lastMouseX / Screen.width);
//		x = Mathf.Abs (Input.GetTouch (0).position.x - lastMouseX) / Screen.width * 100 * x;
//		float x = mousePos.x;

		Vector3 direction = new Vector3 (0f, 0f, 1f);
		direction = Camera.main.transform.TransformDirection (direction);

		_rigidbody.AddForce ((direction * speed / 2f) + (Vector3.up * speed));

		holding = false;
		thrown = true;
		throwCount += 1;
		ballsShot.text = "Balls Shot: " + throwCount.ToString ();

		Debug.Log (offset);

		Invoke ("Reset", 3.0f);

	}

	void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.GetComponent<SphereCollider> () != null) {

			cupCount += 1;
			cupsMade.text = "Cups Made: " + cupCount.ToString ();

		} 

	}

	void GameOver () {
		gameOverButton.SetActive (true);
		gameOverText.SetActive (true);
		gameObject.SetActive (false);
	}

}
