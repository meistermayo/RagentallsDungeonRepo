using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoints : MonoBehaviour {
	[SerializeField] float moveSpeed;
	[SerializeField] float minDist;
	[SerializeField] float delayTime;
	[SerializeField] Transform[] points;
	int currentPoint=0;
	bool isMoving;
	public bool IsMoving { get { return isMoving; } }
	void Start(){
		//StartCoroutine (Travel ());
	}

	public void TravelToPoint(int pointInd)
	{
		StopAllCoroutines ();
		StartCoroutine(Travel (pointInd));
	}

	IEnumerator Travel(int pointInd)
	{
		isMoving = true;
		while (Vector3.Distance (transform.position, points [pointInd].position) > minDist) {
			Vector3 dir = Vector3.Normalize (points [pointInd].position - transform.position) * moveSpeed;
				dir = new Vector3 (dir.x, 0f, dir.z);
				transform.position += dir;
				yield return new WaitForFixedUpdate ();
			}
			yield return new WaitForSeconds (delayTime);
		isMoving = false;
	}
}
