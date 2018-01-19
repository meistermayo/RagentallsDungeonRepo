using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	[SerializeField] protected AudioClip[] audioClips;
	protected AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public void Play(int a, bool loop)
	{
		audioSource.pitch = 1f;
		if (a >= 0 && a < audioClips.Length) {
			audioSource.clip = audioClips [a];
			audioSource.loop = loop;
			audioSource.Play ();
		}
	}
	public void Play(int a, float pitch)
	{
		if (a >= 0 && a < audioClips.Length) {
			audioSource.clip = audioClips [a];
			audioSource.pitch = pitch+1f;
			audioSource.Play ();
		}
	}

	public void Stop()
	{
		audioSource.Stop ();
	}

	public AudioClip GetClip(int a)
	{
		if (a > -1 && a < audioClips.Length) {
			return audioClips [a];
		} else
			return null;
	}
}
