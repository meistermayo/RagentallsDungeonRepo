using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKUP_TYPE
{
	SMILER_HEAD = 0,
	MOUSE,
	NONE
}

public class Player_Pickup : MonoBehaviour {
	[SerializeField] float useDistance;
	[SerializeField] GameObject handObject;
	float handObjectMin = -.5f, handObjectMax = -.135f;
	float handSpeed = .025f;
	bool handRising;
    bool heldItem;

	bool click;
	bool holdingSomething;

	ItemDisplay itemDisplay;

	void Awake()
	{
		itemDisplay = GetComponentInChildren<ItemDisplay> ();
	}
	void Update()
	{
		GetInput ();
		AttemptUse ();
		LookForUse ();
		AnimateHand ();
	}

	void FixedUpdate()
	{
	}

	void GetInput(){
		click = Input.GetMouseButtonDown (0);
	}

	void AttemptUse(){
        if (click)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward);
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, useDistance))
            {
                if (hit.collider.tag == "Usable")
                {
                    hit.collider.GetComponentInParent<Usable>().Use();
                }
                else if (hit.collider.tag == "Weight")
                {
                    PickupItem(hit.collider.gameObject, hit.collider.GetComponent<Pickupable_Item>().Pickup);
                    heldItem = true;
                }
                else if (holdingSomething)
                {
                    DropItem();
                    heldItem = false;
                }
            }
            else if (holdingSomething)
            {
                DropItem();
                heldItem = false;
            }
        }
	}

	void LookForUse()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Camera.main.transform.forward, out hit, useDistance)) {
			if (hit.collider.tag == "Usable" || hit.collider.tag == "Weight") {
				handRising = true;
			} else
				handRising = false;
		} else
			handRising = false;
	}

	void AnimateHand()
	{
		handObject.transform.localPosition += Vector3.up * handSpeed * ((handRising || heldItem) ? 1f : -1f);
		if (handObject.transform.localPosition.y > handObjectMax)
		handObject.transform.localPosition = new Vector3 (handObject.transform.localPosition.x, handObjectMax, handObject.transform.localPosition.z); else
			if (handObject.transform.localPosition.y < handObjectMin)
			handObject.transform.localPosition = new Vector3 (handObject.transform.localPosition.x, handObjectMin, handObject.transform.localPosition.z); 
	}
	// !!!
	void PickupItem(GameObject itemObject,PICKUP_TYPE itemType)
	{
		holdingSomething = true;
		itemDisplay.PickUpItem (itemObject, itemType);
	}

	void DropItem()
	{
		holdingSomething = false;
		itemDisplay.DropItem ();
	}
}
