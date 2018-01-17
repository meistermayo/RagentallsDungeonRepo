using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDestroy : MonoBehaviour {
	[SerializeField] protected GameObject toCreate;
	[SerializeField] protected Vector3 position;
	protected GameObject myObject;

	public virtual void CreateObject()
	{
		myObject = Instantiate (toCreate,position,Quaternion.identity) as GameObject;
	}
}
