using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.FSM.EnemyStates
{
    public class Pursue : EnemyState
    {
        public Pursue(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.PURSUE;
            agent.speed = 5;
            agent.isStopped = false; 
        }

        public override void Enter()
        {
            anim.SetTrigger("isRunning");
            base.Enter();
        }

        public override void Update()
        {
            agent.SetDestination(player.position);

            // Must check if has path to ensure navmesh system has set path.
            if(agent.hasPath)
            {
                if (CanAttackPlayer())
                {
                    nextState = new Attack(npc, agent, anim, player);
                    stage = EVENT.EXIT; 
                }
                else if (!CanSeePlayer())
                {
                    nextState = new Patrol(npc, agent, anim, player);
                    stage = EVENT.EXIT; 
                }
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isRunning");
            base.Exit();
        }
    }
}
