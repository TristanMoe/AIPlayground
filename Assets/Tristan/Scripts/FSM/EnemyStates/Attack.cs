using UnityEngine;
using UnityEngine.AI;
namespace Assets.Scripts.FSM.EnemyStates
{
    public class Attack : EnemyState
    {
        float rotationSpeed = 2.0f;
        AudioSource shoot; 

        public Attack(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.ATTACK;
            // TODO: Should not be reloaded for each state. 
            shoot = _npc.GetComponent<AudioSource>();
        }

        public override void Enter()
        {
            anim.SetTrigger("isShooting");
            agent.isStopped = true;
            shoot.Play();
            base.Enter();
        }

        public override void Update()
        {
            // Get direction to player
            Vector3 direction = player.transform.position - npc.gameObject.transform.position;

            // Rotate character, but not in the y-axis. 
            direction.y = 0;
            // Rotate to player.
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotationSpeed); 
            if(!CanAttackPlayer())
            {
                nextState = new Idle(npc, agent, anim, player);
                stage = EVENT.EXIT; 
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isShooting");
            shoot.Stop(); 
            base.Exit();
        }
    }
}
