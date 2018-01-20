using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilerController : Usable {
	[SerializeField] float screenDist;
	[SerializeField] float maxDist;
	[SerializeField] float delay;
	[SerializeField] Vector3 offset;
	public Material[] faces;
	int faceCurrent=0;
	public MeshRenderer faceRenderer;

	public AudioClip[] noises;
	AudioSource _audioSource;
	float starePercent;
	bool dead;
	bool canChange=true;
	[Header("Dead variables")]
	public GameObject defaultHead, deadJaw, deadHead;
	public float launchForce;
	public DoorController[] doorsToOpen;
	public DoorController[] doorsToClose;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource> ();
	}

	void OpenDoors(DoorController[] doors)
	{
		foreach (DoorController door in doors) {
			door.Activate (false);
		}
	}

	void CloseDoors(DoorController[] doors)
	{
		foreach (DoorController door in doors) {
			door.Deactivate ();
		}
	}

	void Update()
	{
		/*/
		if (CheckPlayerLooking ())
			starePercent += 1f;
		else

			starePercent -= .5f;
		if (dead)
			return;
		if (starePercent > 110f) {
			if (faceCurrent + 1 == faces.Length) {
				PopHeadOff ();
			} else {
				ChangeFace (faceCurrent + 1, true);
				starePercent = 5f;
			}
		} else if (starePercent < 0f) {
			starePercent = 0f;
			if (faceCurrent > 0)
				ChangeFace (faceCurrent - 1, false);
		}

		//*/
		if (CheckPlayerLooking () < screenDist) {
			if (faceCurrent != 2) {
				ChangeFace (2, true);
				OpenDoors (doorsToOpen);
				CloseDoors (doorsToClose);
			}
		} else if (CheckPlayerLooking() < screenDist * 2f){
			if (faceCurrent != 1) {
				ChangeFace (1, false);
			}
		}else {
			if (faceCurrent != 0) {
				ChangeFace (0, false);
				OpenDoors (doorsToClose);
				CloseDoors (doorsToOpen);
			}
		}
	}

	void ChangeFace(int i, bool noise)
	{
		if (!canChange)
			return;
		canChange = false;
		StartCoroutine (RestoreChange ());
		faceRenderer.material = faces [i];
		faceCurrent = i;
		_audioSource.pitch = Random.Range (.9f, 1.1f);
		if (noise) {
			_audioSource.clip = noises [i - 1];
			_audioSource.Play ();
		}
	}

	IEnumerator RestoreChange()
	{
		//yield return new WaitForSeconds(delay);
		yield return null;
		canChange = true;
	}

	void PopHeadOff()
	{
		for (int i = 0; i < doorsToOpen.Length; i++) {
			//doorsToOpen [i].Activate ();
		}
		_audioSource.clip = noises [noises.Length-1];
		_audioSource.Play ();
		dead = true;
		deadJaw.SetActive (true);
		deadHead.SetActive (true);
		deadHead.transform.parent = null;
		defaultHead.SetActive (false);
		deadHead.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1),Random.Range(1,2), Random.Range(-1,1)) * launchForce);
	}

	float CheckPlayerLooking()
	{
		//*/
		//return false;
		/*/
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Camera.main.transform.position, out hit, 100f)) {
			return  (hit.collider.tag == "Player");
		} else
			return false; // outside range
		/*/
		if (Vector3.Distance (transform.position, GlobalVars.Instance.Player.transform.position) > maxDist)
			return 999f;
		Vector3 smilerCameraPos = Camera.main.WorldToViewportPoint (transform.position + offset);
		if (smilerCameraPos.z < 0f) 
			return 999f;
		smilerCameraPos.z = 0f;
		Vector3 screenCenter =new Vector3(.5f,.5f,0f);
		return (Vector3.Distance (smilerCameraPos, screenCenter));
		//*/
	}

	public override void Use ()
	{
		//starePercent += 33f;
	}

}
