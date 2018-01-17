using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour {
	public Image redSolid;
	public float timer;
	bool dead;

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "KillPlayer") {
			Kill ();
		}
	}

	void FixedUpdate()
	{
		if (dead)
		{
			Color newColor = new Color (1f, 0f, 0f, redSolid.color.a +.1f);
			redSolid.color = newColor;
			if (--timer <= 0f)
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void Kill()
	{
		if (!dead) {
			if (!GlobalAudioManager.Instance.IsPlaying (MUSIC_TYPE.MONTY_JUMPSCARE))
				GlobalAudioManager.Instance.Play (MUSIC_TYPE.MONTY_JUMPSCARE, false);
			dead = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
		}
	}
}
