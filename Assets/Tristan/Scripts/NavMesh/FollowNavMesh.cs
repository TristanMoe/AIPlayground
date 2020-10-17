using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNavMesh : MonoBehaviour
{
    public GameObject wpManager;
    GameObject[] wps;
    UnityEngine.AI.NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WayPointManager>().waypoints;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void GoToHeli()
    {
        agent.SetDestination(wps[9].transform.position); 
    }

    public void GoToFactory()
    {
        agent.SetDestination(wps[5].transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}
