﻿using UnityEngine;
using UnityEngine.AI;


namespace Assets.Scripts.FSM.EnemyStates
{
    public class Patrol : EnemyState
    {
        private int currentIndex = -1; 

        public Patrol(GameObject _npc,
            NavMeshAgent _agent,
            Animator _anim,
            Transform _player) : base(_npc, _agent, _anim, _player)
        {
            name = STATE.PATROL;
            agent.speed = 2;
            agent.isStopped = false; 
        }

        public override void Enter()
        {
            // All distances compared are lesser.
            float lastDist = Mathf.Infinity; 

            // Find closest waypoint. 
            for(int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
            {
                GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
                float distance = Vector3.Distance(npc.transform.position,
                                        thisWP.transform.position); 
                if(distance < lastDist)
                {
                    currentIndex = i-1;
                    lastDist = distance; 
                }
            }


            anim.SetTrigger("isWalking");
            base.Enter();
        }

        public override void Update()
        {
            if(agent.remainingDistance < 1)
            {
                if(currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                {
                    currentIndex = 0; 
                } 
                else
                {
                    currentIndex++; 
                }
                agent.SetDestination(
                    GameEnvironment.Singleton.Checkpoints[currentIndex]
                    .transform.position);
            }

            if (CanSeePlayer())
            {
                nextState = new Pursue(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }

            if(IsPlayerBehind())
            {
                nextState = new Safe(npc, agent, anim, player);
                stage = EVENT.EXIT; 
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isWalking");
            base.Exit();
        }
    }
}
