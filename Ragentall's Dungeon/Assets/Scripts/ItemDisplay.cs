using UnityEngine;
using System.Collections;

public class ItemDisplay : MonoBehaviour
{
	[SerializeField] float throwForce;
	[SerializeField] float upForce;
	[SerializeField] GameObject[] itemChildren;

	PICKUP_TYPE pickup;
	public PICKUP_TYPE Pickup{
		get { return pickup; }
		set { }
	}

	GameObject heldItemObject;

	void Awake()
	{
		//IniChildren ();
	}

	void IniChildren()
	{
		Transform[] temp = GetComponentsInChildren<Transform> ();
		itemChildren = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++) {
			itemChildren [i] = temp [i].gameObject;
		}
	}

	// !!!
	public void PickUpItem(GameObject itemObject, PICKUP_TYPE _pickup)
	{
		if (_pickup == PICKUP_TYPE.NONE)
			return;

		itemChildren [(int)_pickup].SetActive (true);
		itemObject.transform.localPosition = transform.position;
		heldItemObject = itemObject;
		heldItemObject.SetActive (false);

		pickup = _pickup;

	}

	public void DropItem()
	{
		if (pickup == PICKUP_TYPE.NONE)
			return;

		itemChildren [(int)pickup].SetActive (false);
		heldItemObject.SetActive (true);
        heldItemObject.transform.position = transform.position + transform.forward * 1f;// + Vector3.up * 1f;
		heldItemObject.GetComponent<Rigidbody> ().velocity = transform.forward * throwForce + transform.up * upForce;
        if (pickup == PICKUP_TYPE.MOUSE)
            heldItemObject.GetComponent<Mouse_Script>().SetFree(transform.forward);
		pickup = PICKUP_TYPE.NONE;
	}
}

