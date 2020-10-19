using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent; 
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        var goal = GameObject.FindGameObjectWithTag("Safe");
        animator.SetTrigger("isWalking");
        agent.SetDestination(goal.transform.position); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
