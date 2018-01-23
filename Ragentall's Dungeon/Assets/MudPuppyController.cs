using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MudPuppyController : MonoBehaviour {
	AudioManager audioManager;
	[SerializeField] AudioSource childAudioSource;
	GameObject playerObject;
	FirstPersonController playerController;
	bool isAttacking;
	bool canAttack;
	[SerializeField] float lerpTime;
	[SerializeField] float finalSinkDepth;
	[SerializeField] float eyeContactOffset;
	[SerializeField] Vector3 offset;
	[SerializeField] GameObject _head;
	[SerializeField] GameObject _neck;
	[SerializeField] GameObject _body;

	void Start()
	{
		audioManager = GetComponent<AudioManager> ();
		//childAudioSource = GetComponentInChildren<AudioSource> ();
		StartCoroutine(CanAttackRoutine());
	}

	void Update()
	{
		if (isAttacking) {
			AttackPlayer ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (canAttack) {
			if (other.CompareTag ("Player")) {
				// Grab player, start sinking
				isAttacking = true;
				Appear ();
				playerObject = other.gameObject;
				playerObject.transform.position = transform.position + offset;
				playerController = playerObject.GetComponent<FirstPersonController> ();
				playerController.StartSinking ();
				audioManager.Play (0, false);
				childAudioSource.Play ();
			}
		}
	}

	void AttackPlayer(){

		if (playerObject.transform.position.y <= finalSinkDepth) {
			playerObject.GetComponent<KillPlayer> ().Kill ();
		}

		if (!playerController.Swimming) {
			playerController.StopSinking ();
			isAttacking = false;
			childAudioSource.Stop ();
			StopAllCoroutines ();
			StartCoroutine (CanAttackRoutine ());
			Disappear ();
		}

		transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, playerObject.transform.position.y + eyeContactOffset, transform.position.z),lerpTime);
	}

	void Disappear()
	{
		_head.SetActive (false);
		_neck.SetActive (false);
		_body.SetActive (false);
		//StopAllCoroutines ();
		//StartCoroutine (DisappearRoutine ());
	}

	void Appear()
	{
		_head.SetActive (true);
		_neck.SetActive (true);
		_body.SetActive (true);
	}

	IEnumerator DisappearRoutine()
	{
		for (int i = 0; i < 60; i++) {
			transform.position += Vector3.down * .25f;
			yield return null;
		}
		_head.SetActive (false);
		_neck.SetActive (false);
		_body.SetActive (false);
	}

	IEnumerator CanAttackRoutine()
	{
		while (true){
			canAttack = false;
			yield return new WaitForSeconds (10f);
			canAttack = true;
			yield return new WaitForSeconds (10f);
		}
	}
}
