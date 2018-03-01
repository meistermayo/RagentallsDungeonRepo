using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutTimer : MonoBehaviour {
    [SerializeField] float timer;
    [SerializeField] Ragentall_Chase1_Script Ragentall;
    [SerializeField] GameObject Lights;
    AudioSource noise;
    private void Start()
    {
        StartCoroutine(TurnOffStuff());
        noise = GetComponent<AudioSource>();
    }

    IEnumerator TurnOffStuff()
    {
        yield return new WaitForSeconds(timer);
        noise.Play();
        Ragentall.enabled = (true);
        Lights.SetActive(false);
    }
}
