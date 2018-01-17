using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : AudioManager {
	[SerializeField] GameObject[] ToDelete; 
	[SerializeField] bool used;

	public virtual void Use()
	{
		if (used)
			return;
		foreach (GameObject obj in ToDelete) {
			CreateOnDestroy c = obj.GetComponent<CreateOnDestroy> ();
			if (c != null)
				c.CreateObject ();
			Destroy (obj);
		}
		used = true;
		if (audioClips.Length > 0)
			Play (0, false);
	}
}
