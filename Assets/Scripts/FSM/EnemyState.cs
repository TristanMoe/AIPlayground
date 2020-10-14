using UnityEngine;
using UnityEngine.AI; 

namespace Assets.Scripts.FSM
{
    public class EnemyState
    {
        public enum STATE
        {
            IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
        };

        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };

        public STATE name;
        protected EVENT stage;
        protected GameObject npc;
        protected Animator anim;
        protected Transform player;
        protected EnemyState nextState;
        protected NavMeshAgent agent; 

        // Line of sight.
        float visDist = 10.0f;
        float visAngle = 30.0f;
        float shootDist = 7.0f;
        float behindDist = 2.0f; 

        public EnemyState(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player)
        {
            npc = _npc;
            agent = _agent;
            anim = _anim;
            player = _player;
            stage = EVENT.ENTER; 
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public EnemyState Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update(); 
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState; 
            }
            return this; 
        }

        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward); 

            // Magnitude of direction vector must be within visual
            if(direction.magnitude < visDist && angle < visAngle)
            {
                return true; 
            }
            return false; 
        }

        public bool CanAttackPlayer()
        {
            Vector3 direction = player.position - npc.transform.position;
            
            // Magnitude of direction vector must be within visual
            if (direction.magnitude < shootDist)
            {
                return true;
            }
            return false;
        }

        public bool IsPlayerBehind()
        {
            Vector3 direction = npc.transform.position - player.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);

            if (direction.magnitude < behindDist && angle < 30)
            {
                return true; 
            }

            return false; 
        }
    }
}
