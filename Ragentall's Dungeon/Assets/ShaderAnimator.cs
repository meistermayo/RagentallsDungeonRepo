using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAnimator : MonoBehaviour {
	[SerializeField] float value;
	[SerializeField] string floatName;
	bool hasBegun;

	Material thisMaterial;

	void Start()
	{
		thisMaterial = GetComponent<MeshRenderer> ().material;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			RunAnimateShader ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ResetAnimateShader ();
		}
	}

	protected virtual void RunAnimateShader()
	{
		if (hasBegun)
			return;
		hasBegun = true;
		StartCoroutine (AnimateShader ());
	}

	protected virtual void ResetAnimateShader()
	{
		StopCoroutine ("AnimateShader");
		hasBegun = false;
		thisMaterial.SetFloat (floatName, 0f);
	}

	protected IEnumerator AnimateShader()
	{
		while (true) {
			thisMaterial.SetFloat(floatName,thisMaterial.GetFloat(floatName)+value);
			yield return new WaitForEndOfFrame ();
		}
	}
}
