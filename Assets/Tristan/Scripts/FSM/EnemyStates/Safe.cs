using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.FSM.EnemyStates
{
    public class Safe : EnemyState
    {
        public Transform safePoint; 

        public Safe(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.RUNAWAY;
            agent.speed = 6;
            agent.isStopped = false;
            safePoint = GameObject.FindGameObjectWithTag("Safe").transform; 
        }

        public override void Enter()
        {
            anim.SetTrigger("isRunning");
            agent.SetDestination(safePoint.position);
            base.Enter();
        }

        public override void Update()
        {      
            // If within safe area set patrolling
            if(Vector3.Distance(safePoint.position, npc.gameObject.transform.position) < 2)
            {
                nextState = new Idle(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isRunning");
            base.Exit();
        }
    }
}
