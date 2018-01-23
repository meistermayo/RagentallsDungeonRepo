using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			GetComponent<AudioSource>().Play();
		
			Destroy (gameObject,1f);
		}
	}
}
