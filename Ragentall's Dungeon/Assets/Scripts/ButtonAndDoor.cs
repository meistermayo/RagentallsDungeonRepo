using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDoor : MonoBehaviour {
	[SerializeField] GameObject doorObject;
	[SerializeField] GameObject doorTarget;
	Animator animator;
	bool isDown;

	void Awake()
	{
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Weight") {
			isDown = true;
			animator.SetBool ("isDown", isDown);
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Weight") {
			isDown = true;
			animator.SetBool ("isDown", isDown);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Weight") {
			isDown = false;
			animator.SetBool ("isDown", isDown);
		}
	}
}
