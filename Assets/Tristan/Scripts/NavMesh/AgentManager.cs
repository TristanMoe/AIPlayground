using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    // Requires GameObject in View. 
    GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        // Find all agents in scene.
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        // Move all agents to position.
        if (Input.GetMouseButtonDown(0))
        {
            // Sends raycast from mouse. 
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100))
            {
                foreach (var agent in agents)
                {
                    agent.GetComponent<AIControl>().agent.SetDestination(hit.point);
                }
            }
        }
    }
}
