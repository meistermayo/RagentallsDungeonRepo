using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RATSTATES{
	PASSIVE,
	AGGRO,
	RETREAT
}
public class RatController : MonoBehaviour {
	RATSTATES ratState;
	RaycastHit hit;
	Rigidbody mBody;
	Animator mAnim;
	public float moveSpeed;
	bool inFire;
	bool aggro;
	public AudioClip[] noises;
	public bool canChasePlayer;
	private bool yetPlayedNoise = false;
	void Awake()
	{
		ratState = RATSTATES.PASSIVE;
		mAnim = GetComponent<Animator> ();
		mBody = GetComponent<Rigidbody> ();
		if (canChasePlayer)
			mAnim.SetBool ("CanChase", canChasePlayer);
	}


	void FixedUpdate()
	{
		if (ratState == RATSTATES.RETREAT)
			return;
		if (ratState == RATSTATES.PASSIVE) {
			if (Physics.Raycast (transform.position, Camera.main.transform.position - transform.position, out hit, 100f,LayerMask.GetMask(new string[1] {"Default"}),QueryTriggerInteraction.Ignore)) {
				if (hit.collider.tag == "Player") {
					mAnim.SetTrigger ("Notice");
					ratState = RATSTATES.AGGRO;
				}
			}
		}
		if (canChasePlayer) {
			if (ratState == RATSTATES.AGGRO && mAnim.GetCurrentAnimatorStateInfo (0).IsName ("Rat_Run")) {
				if (!yetPlayedNoise) {
					yetPlayedNoise = true;
					AudioSource audioSource = GetComponent<AudioSource> ();
					audioSource.clip = noises [1];
					audioSource.Play ();
				}
				mBody.velocity = Vector3.Normalize (Camera.main.transform.position - transform.position) * moveSpeed;
				mBody.velocity = mBody.velocity.x * Vector3.right + mBody.velocity.z * Vector3.forward;
			}
		}
	}

	public void PlayNoise(int i)
	{
		AudioSource audioSource = GetComponent<AudioSource> ();
		audioSource.clip = noises [i];
		audioSource.Play ();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.transform.tag != "Fire")
			return;
		inFire = true;
		mBody.velocity = Vector3.Normalize(transform.position - other.transform.position) * moveSpeed/2f;
		StartCoroutine (Retreat ());
	}

	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Fire")
			inFire = false;
	}

	IEnumerator Retreat()
	{
		ratState = RATSTATES.RETREAT;
		yield return new WaitForSeconds (1f);
		while (inFire)
			yield return new WaitForSeconds (.25f);
		ratState = RATSTATES.AGGRO;
	}
}
