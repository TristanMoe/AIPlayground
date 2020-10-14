using Assets.Scripts.FSM.EnemyStates;
using UnityEngine;
using UnityEngine.AI; 

namespace Assets.Scripts.FSM
{
    public class AI : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;
        public Transform player;
        EnemyState currentState;

        private void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            currentState = new Idle(this.gameObject, agent, anim, player); 
        }

        private void Update()
        {
            // Will return either same state or next state.
            currentState = currentState.Process(); 
        }

    }
}
