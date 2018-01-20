using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CanvasSwim : MonoBehaviour {
	[SerializeField] float alpha;
	[SerializeField] FirstPersonController player;
	[SerializeField] Image image;

	void Update () {
		float newAlpha;
		newAlpha = player.Swimming ? alpha : 0f;
		image.color = new Color (image.color.r, image.color.g, image.color.b, newAlpha);
	}
}
