using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyCrusader : MonoBehaviour {
	[SerializeField] CrusaderController crusader;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			crusader.Notify ();
		}
	}
}
