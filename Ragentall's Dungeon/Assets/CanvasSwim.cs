using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CanvasSwim : MonoBehaviour {
	[SerializeField] float alpha;
	[SerializeField] FirstPersonController player;
	[SerializeField] Image image;
	[SerializeField] Image image1;
	[SerializeField] float breathDecay;
	float breath = 0f;

	void Update () {
		float newAlpha;
		newAlpha = player.Swimming ? alpha : 0f;
		image.color = new Color (image.color.r, image.color.g, image.color.b, newAlpha);
		if (player.Swimming) {
			breath += breathDecay;
			image1.color = new Color (0f, 0f, 0f, breath);
			if (breath >= 1f)
				GetComponent<KillPlayer> ().Kill ();
		} else {
			image1.color = new Color (0f, 0f, 0f, 0f);
			breath = 0f;
		}
	}
}
