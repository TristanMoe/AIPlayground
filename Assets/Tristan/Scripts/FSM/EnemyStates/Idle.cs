using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.FSM.EnemyStates
{
    public class Idle : EnemyState
    {
        public Idle(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.IDLE; 
        }

        public override void Enter()
        {
            // Trigger animator - see animator for specifics
            anim.SetTrigger("isIdle"); 
            base.Enter();
        }

        public override void Update()
        {
            if(CanSeePlayer())
            {
                nextState = new Pursue(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if(Random.Range(0, 100) < 10)
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT; 
            } 
        }

        public override void Exit()
        {
            // Clean up animation
            anim.ResetTrigger("isIdle");
            base.Exit();
        }
    }
}
