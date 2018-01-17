using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnCollide : MonoBehaviour {
	[SerializeField] MUSIC_TYPE music;
	[SerializeField] bool loop;

	void OnTriggerEnter(Collider other)
	{
		GlobalAudioManager.Instance.Play(music,loop);
		Destroy (gameObject);
	}

	public void Init(MUSIC_TYPE _music, bool _loop)
	{
		music = _music;
		loop = _loop;
	}
}
