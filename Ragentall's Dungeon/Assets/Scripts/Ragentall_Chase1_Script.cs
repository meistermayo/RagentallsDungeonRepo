using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragentall_Chase1_Script : MonoBehaviour {
	[SerializeField] GameObject cameraObject;
	[SerializeField] float moveSpeed;
	[SerializeField] float jawOpen_minDist;
	[SerializeField] float jawOpen_maxDist;
	[SerializeField] float maxDist;
	[SerializeField] Transform[] points;
	int pointIndex;
	bool chasingPlayer;

	float doubleSpeed;
	float jawOpen_dist;
	bool active=true;
	bool caught;

	Rigidbody rBody;
	Animator anim;
	GameObject playerObject;
	AudioManager audioManager;
	NavMeshAgent navMeshAgent;

	void Start()
	{
		Application.targetFrameRate = 60;
		navMeshAgent = GetComponent<NavMeshAgent> ();
		rBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		doubleSpeed = moveSpeed * 2;
		audioManager = GetComponent<AudioManager> ();
		audioManager.Play (2, true);// Random.Range(-.3f,-.1f));
	}

	void FixedUpdate()
	{
		if (active) {
			if (chasingPlayer) {
				ChasePlayer ();
				StopChasingPlayer();
			}
			else
				LookForPlayer ();
				
			UpdateJaw ();
		}
	}

	void StopChasingPlayer()
	{
		if (Vector3.Distance (GlobalVars.Instance.Player.transform.position, transform.position) > maxDist) {
			rBody.velocity = Vector3.zero;
			navMeshAgent.enabled = true;
			GlobalAudioManager.Instance.Play(MUSIC_TYPE.MONTY_VIBES,true);
			audioManager.Play (2, true);// Random.Range(-.3f,-.1f));
			chasingPlayer=false;
		}
	}

	void ChasePlayer()
	{
		if (Vector3.Distance (transform.position, playerObject.transform.position) < 3f) {
			if (moveSpeed < doubleSpeed) {
				caught=true;
				//GetComponent<AudioManager> ().Play (1, false);
			}
			moveSpeed = doubleSpeed;
		}
		rBody.velocity = Vector3.Normalize (playerObject.transform.position - transform.position) * moveSpeed;
		rBody.velocity = new Vector3 (rBody.velocity.x, 0f, rBody.velocity.z);
	}

	void LookForPlayer()
	{
		navMeshAgent.destination = points [pointIndex].position;
		navMeshAgent.speed = moveSpeed;

		if (Vector3.Distance (transform.position, navMeshAgent.destination) < 1f) {
			pointIndex = (pointIndex + 1) % points.Length;
		}

		if (Vector3.Distance (transform.position, playerObject.transform.position) < maxDist) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, playerObject.transform.position - transform.position, out hit, 1000f)) {
				if (hit.collider.CompareTag ("Player")) {
					//audioManager.Play (1, false);
					GlobalAudioManager.Instance.Play(MUSIC_TYPE.ROAR,false);
					audioManager.Play (1, true);// Random.Range(-.3f,-.1f));
					chasingPlayer = true;
					navMeshAgent.enabled = false;
					StopAllCoroutines ();
					StartCoroutine (StopChasing ());
				}
			}
		}
	}
	void UpdateJaw()
	{
		jawOpen_dist = (Mathf.Clamp(Vector3.Distance (transform.position, playerObject.transform.position),jawOpen_minDist,jawOpen_maxDist) - jawOpen_minDist)/(jawOpen_maxDist - jawOpen_minDist);
		anim.SetFloat("Blend",jawOpen_dist);
	}

	public void Remote_KillPlayer()
	{
		playerObject.GetComponent<KillPlayer> ().Kill ();
	}

	void CatchPlayer()
	{
		caught = true;
		if (active)
			active = false;
		else
			return;
		audioManager.Stop();
		rBody.velocity = Vector3.zero;
		playerObject.transform.parent = cameraObject.transform;
		playerObject.transform.localPosition = Vector3.zero;
		playerObject.transform.localRotation = Quaternion.identity;
		playerObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
		GlobalAudioManager.Instance.Play (MUSIC_TYPE.MONTY_JUMPSCARE, false);
		playerObject.GetComponent<CharacterController> ().enabled = false;
		Camera.main.transform.localRotation = Quaternion.identity;
		GetComponent<Billboard_LookAt> ().enabled = false;
		GetComponent<CapsuleCollider> ().enabled = false;
		anim.SetTrigger ("CatchPlayer");
	}

	void CatchGoblin(GameObject goblin)
	{
		if (active)
			active = false;
		else
			return;
		rBody.velocity = Vector3.zero;
		goblin.SetActive (false);
		anim.SetTrigger ("CatchGoblin");
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Player") {
			CatchPlayer ();
		} else if (other.collider.tag == "Goblin") {
			CatchGoblin (other.gameObject);
		}
	}

	public void Reset()
	{
		active = true;
		anim.SetTrigger ("Reset");
	}

	IEnumerator ClackNoise()
	{
		float angle = 0f;
		float mult = .75f;
		float pitch;
		int count = 0;
		while (true)
		{
			count++;
			angle += mult;
			pitch = Mathf.Sin (angle)/2f;
			if (caught)
				yield break;
			if (rBody.velocity != Vector3.zero) {
				GetComponent<AudioManager> ().Play (2, pitch+.8f);// Random.Range(-.3f,-.1f));
			}
			float delay = .05f;
			if (count % 9 == 0)
				delay = .1f;
			yield return new WaitForSeconds (delay);
		}
	}

	IEnumerator StopChasing()
	{
		bool stop = false;
		while (!stop) {
			yield return new WaitForSeconds (5f);
			if (playerObject.transform.position.y < transform.position.y) {
				rBody.velocity = Vector3.zero;
				navMeshAgent.enabled = true;
				GlobalAudioManager.Instance.Play(MUSIC_TYPE.MONTY_VIBES,true);
				audioManager.Play (2, true);// Random.Range(-.3f,-.1f));
				chasingPlayer=false;
				stop = true;
			}
		}
	}

}
