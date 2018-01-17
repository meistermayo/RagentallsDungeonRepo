using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoints : MonoBehaviour {
	[SerializeField] float moveSpeed;
	[SerializeField] float minDist;
	[SerializeField] float delayTime;
	[SerializeField] Transform[] points;
	int currentPoint=0;

	void Start(){
		StartCoroutine (Travel ());
	}

	IEnumerator Travel()
	{
		while (currentPoint < points.Length) {
			while (Vector3.Distance (transform.position, points [currentPoint].position) > minDist) {
				Vector3 dir = Vector3.Normalize (points [currentPoint].position - transform.position) * moveSpeed;
				dir = new Vector3 (dir.x, 0f, dir.z);
				transform.position += dir;
				yield return new WaitForFixedUpdate ();
			}
			yield return new WaitForSeconds (delayTime);
			currentPoint++;
		}
	}
}
