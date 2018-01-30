using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mouse_Script : Pickupable_Item {
    NavMeshAgent navMesh;
    Rigidbody body;
    [SerializeField] float moveSpeed;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void SetFree(Vector3 forward)
    {
        body.velocity = forward * moveSpeed;
    }
}
