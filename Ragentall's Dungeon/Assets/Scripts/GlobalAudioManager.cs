using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MUSIC_TYPE{
	MONTY_VIBES = 0,
	HORROR_AMBIENCE,
	MONTY_JUMPSCARE,
	RAGENTALL_VIBES,
	ROAR
}
public class GlobalAudioManager : MonoBehaviour {

	private static GlobalAudioManager instance;
	[SerializeField]  AudioClip[] audioClips;
	 AudioSource audioSource;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} 
		else 
		{
			DestroyImmediate (gameObject);
			Debug.Log (instance);
		}
		audioSource = GetComponent<AudioSource> ();
	}

	public static GlobalAudioManager Instance
	{
		get{return instance;}
		set{}
	}

	public void Play(MUSIC_TYPE music_type, bool loop)
	{
		int a = (int)music_type;
		if (a >= 0 && a < audioClips.Length) 
		{
			audioSource.clip = audioClips [a];
			audioSource.loop = loop;
			audioSource.Play ();
		}
	}

	public bool IsPlaying(MUSIC_TYPE music_type)
	{
		if (audioSource.isPlaying) {
			if (music_type == MUSIC_TYPE.MONTY_VIBES && audioSource.clip == audioClips[0])
				return true;
			if (music_type == MUSIC_TYPE.HORROR_AMBIENCE && audioSource.clip == audioClips[1])
				return true;
			if (music_type == MUSIC_TYPE.MONTY_JUMPSCARE && audioSource.clip == audioClips[2])
				return true;
			if (music_type == MUSIC_TYPE.RAGENTALL_VIBES && audioSource.clip == audioClips[3])
				return true;
		}
		return false;
	}
}
