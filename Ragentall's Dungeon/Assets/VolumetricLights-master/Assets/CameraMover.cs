using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	[SerializeField] Transform parentRotY, parentRotX;
	Rigidbody body;
	float h,v;
	float x,y;
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpForce;
	void Start()
	{
		//SetParents ();
		Cursor.lockState = CursorLockMode.Locked;
		body = GetComponent<Rigidbody> ();
	}

	void SetParents()
	{
		parentRotY = GetComponentInParent <Transform> ();
		parentRotX = parentRotY.GetComponentInParent<Transform> ();
	}
	// Update is called once per frame
	void Update () {
		GetInput ();
		Rotate ();
		Move ();
	}

	void GetInput()
	{
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		x = Input.GetAxis ("Mouse X");
		y = Input.GetAxis ("Mouse Y");
		if (Input.GetMouseButton (0))
			Cursor.lockState = CursorLockMode.Locked;
	}

	void Rotate()
	{
		if (Cursor.lockState != CursorLockMode.Locked)
			return;
		parentRotY.transform.localRotation *= Quaternion.Euler (Vector3.up * x);
		//parentRotX.transform.localRotation *= Quaternion.Euler (Vector3.right * y);
	}

	void Move()
	{
		body.velocity = (transform.right * h + transform.forward * v) * moveSpeed + transform.up * body.velocity.y;
		if (Input.GetButtonDown ("Jump")) {
			body.AddForce (Vector3.up * jumpForce);
		}

	}
}
