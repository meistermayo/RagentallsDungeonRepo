using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {
	Rigidbody rBody;
	[SerializeField] float durationInSeconds;
	[SerializeField] float floatSpeed;
	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * Random.Range (.25f, 1f);
		rBody = GetComponent<Rigidbody> ();
		rBody.velocity = transform.up * floatSpeed + transform.right * Random.Range (-1f, 1f) + transform.up * Random.Range (-1f, 1f);
		Destroy (gameObject, durationInSeconds);
	}

}
