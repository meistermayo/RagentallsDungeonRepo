using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour {
    NavMeshAgent navMeshAgent;
    Animator animator;
    Vector3 startPos;

    enum GOBLINSTATE
    {
        IDLE=0,
        CHASE=1,
        RETURN=2
    }
    GOBLINSTATE state = GOBLINSTATE.IDLE;

    private void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (state == GOBLINSTATE.IDLE)
            LookForPlayer();
        else if (state == GOBLINSTATE.CHASE)
            ChasePlayer();
        else
            Return();
    }

    void LookForPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GlobalVars.Instance.Player.transform.position - transform.position, out hit, 100f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                animator.SetBool("Running", true);
                state = GOBLINSTATE.CHASE;
            }
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.destination = GlobalVars.Instance.Player.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GlobalVars.Instance.Player.transform.position - transform.position, out hit, 100f))
        {
            if (!hit.collider.CompareTag("Player"))
            {
               // state = GOBLINSTATE.RETURN;
            }
        }
    }

    void Return()
    {
        navMeshAgent.destination = startPos;
        if (Vector3.Distance(transform.position, startPos) < 1f)
        {
            state = GOBLINSTATE.IDLE;
            animator.SetBool("Running", false);
        }
    }
}
