using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NavMesh
{
    public class ChasePlayer : MonoBehaviour
    {
        GameObject player;
        NavMeshAgent agent;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            agent = this.GetComponent<NavMeshAgent>(); 
        }

        private void Update()
        {
            agent.SetDestination(player.transform.position);  
        }
    }
}
