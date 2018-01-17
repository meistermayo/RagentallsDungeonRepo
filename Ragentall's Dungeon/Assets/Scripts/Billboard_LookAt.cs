using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_LookAt : MonoBehaviour {
	private Transform cam;

	void Awake()
	{
		cam = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (cam);
		transform.rotation = Quaternion.Euler (new Vector3 (0f,180f+ transform.rotation.eulerAngles.y,0f));
	}
}
