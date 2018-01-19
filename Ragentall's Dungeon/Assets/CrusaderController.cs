using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderController : MonoBehaviour {
	AudioManager audioManager;
	MoveToPoints moveToPoints;
	Animator animator;
	bool coroutineIsGoing;
	[SerializeField] float minDist;
	[SerializeField] GameObject tempBarrier;
	bool startConvo;

	void Start()
	{
		audioManager = GetComponent<AudioManager> ();
		moveToPoints = GetComponent<MoveToPoints> ();
		animator = GetComponent<Animator> ();
		StartCoroutine (FirstMeeting ());
	}

	IEnumerator FirstMeeting()
	{
		while (!startConvo) {
			yield return null;
		}
		animator.enabled = true;
		audioManager.Play(0,false);
		yield return new WaitForSeconds (audioManager.GetClip(0).length);
		audioManager.Play(1,false);
		yield return new WaitForSeconds (audioManager.GetClip(1).length);
		moveToPoints.TravelToPoint (0);
		while (moveToPoints.IsMoving) {
			yield return null;
		}
		audioManager.Play (2, false);
		Destroy (tempBarrier);
		//yield return new WaitForSeconds (audioManager.GetClip(2).length* 1.5f);
		//while (true) {
		//	audioManager.Play (3, false);
		//	yield return new WaitForSeconds (audioManager.GetClip (3).length * 2f);
		//}

	}


	public void Notify()
	{
		startConvo = true;
	}
}
