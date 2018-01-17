using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {
	private GameObject player;
	public GameObject Player{
		get{ if (player == null)
				Awake (); return player; }
		set{ }
	}

	private static GlobalVars instance;
	public static GlobalVars Instance {
		get { 
			return instance; }
		set { }
	}

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
			DestroyImmediate (gameObject);

	}
}
