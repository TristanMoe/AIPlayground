using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target; 

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        // Run towards opposite direction.
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector); 
    }

    void Pursuit()
    {
        // Vector to target
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        // If target has stopped moving go directly towards it.
        if((toTarget > 50 && relativeHeading < 20) || target.GetComponent<Drive>().currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return; 
        }        
        
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);    
    }

    void Evade()
    {
        // Vector to target
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }


    Vector3 wanderTarget = Vector3.zero;
    /// <summary>
    /// Use circle to define wander area
    /// Jitter will make it more irratic with direction change
    /// https://learn.unity.com/tutorial/wander-o5?uv=2019.3&courseId=5dd851beedbc2a1bf7b72bed&projectId=5e0b9220edbc2a14eb8c9356#5e0bacb6edbc2a1dfc28ba6e
    /// </summary>
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 5;
        float wanderJitter = 10;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f)
            * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld); 
    }

    /// <summary>
    /// See https://learn.unity.com/tutorial/hide-h1zl?uv=2019.3&courseId=5dd851beedbc2a1bf7b72bed&projectId=5e0b9220edbc2a14eb8c9356#5e0bb03eedbc2a143a24dfba
    /// </summary>
    void Hide()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // Set stopping distance via NavMeshAgent.
        Wander();
    }
}
