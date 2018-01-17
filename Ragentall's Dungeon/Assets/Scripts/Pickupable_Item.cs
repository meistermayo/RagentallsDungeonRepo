using UnityEngine;
using System.Collections;

public class Pickupable_Item : MonoBehaviour
{
	[SerializeField] PICKUP_TYPE pickup;
	public PICKUP_TYPE Pickup{
		get { return pickup; }
		set { }
	}
		
	void SetPickup(PICKUP_TYPE _pickup)
	{
		pickup = _pickup;
	}
}

