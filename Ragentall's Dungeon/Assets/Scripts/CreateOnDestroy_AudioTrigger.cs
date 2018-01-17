using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDestroy_AudioTrigger : CreateOnDestroy{
	[SerializeField] MUSIC_TYPE music;
	[SerializeField] bool loop;

	/*/
	public override void CreateObject ()
	{
		base.CreateObject ();
	}
	/*/
	public override void CreateObject()
	{
		base.CreateObject ();
		PlayMusicOnCollide pmoc = myObject.GetComponent<PlayMusicOnCollide> ();
		pmoc.Init (music, loop);
		Destroy (gameObject);
	}
	//*/
}
