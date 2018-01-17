using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class OnTriggerSwitchGate : MonoBehaviour {
	[SerializeField] GameObject[] objectsToDisable;
	[SerializeField] GameObject[] objectsToEnable;

	void OnTriggerEnter(Collider other)
	{
		foreach (GameObject go in objectsToDisable) {
			go.SetActive (false);
		}

		foreach (GameObject go in objectsToEnable) {
			go.SetActive (true);
		}

		Destroy (gameObject);
	}
}
