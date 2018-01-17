using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	bool moving;
	[SerializeField] float moveSpeed;
	[SerializeField] Vector3 moveVec;
	[SerializeField] float moveTime;
	float distMoved;

	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public void Activate(bool destroy)
	{
		if (moving == false) {
			if (audioSource!=null)
			audioSource.Play ();
		}
		moving = true;
		if (destroy)
			Destroy (gameObject, moveTime);
	}

	public void Deactivate()
	{
		moving = false;
	}

	void Update()
	{
		if (moving) {
			if (distMoved < moveTime) {
				transform.position += moveVec * moveSpeed;
				distMoved += moveSpeed;
			}
		}

		if (!moving) {
			if (distMoved > 0.0f) {
				transform.position -= moveVec * moveSpeed;
				distMoved -= moveSpeed;
			}
		}
	}


}
