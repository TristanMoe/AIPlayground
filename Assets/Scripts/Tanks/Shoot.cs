using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject target;
    public GameObject parent; 
    public float speed = 15;
    public float turnSpeed = 2;
    bool canShoot = true; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CanShootAgain()
    {
        canShoot = true; 
    }

    void Fire()
    {
        if(canShoot)
        {
            GameObject shell = Instantiate(shellPrefab,
           shellSpawnPos.transform.position,
           shellSpawnPos.transform.rotation);

            // Shoot bullets towards z-axis. 
            shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
            canShoot = false;

            // Invoke method every 0.5 second.
            Invoke("CanShootAgain", 1f); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Set direction and rotation towards target
        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Set for parent (all components) 
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret(); 
        
        // Shoot if angle is within approximation. 
        if(angle != null && Vector3.Angle(direction, parent.transform.forward) < 5)
        {
            Fire();
        }
    }


    float? RotateTurret()
    {
        float? angle = CalculateAngle(true);

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle; 
    }

    /// <summary>
    // Calculate angle required to hit coordinate (x,y) 
    // See Wikipedia article: https://en.wikipedia.org/wiki/Projectile_motion
    // Chapter: "Angle \theta required to hit coordinate (x,y) 
    /// </summary>
    /// <param name="low">There are always to possible angles (low/high)</param>
    /// <returns></returns>
    float? CalculateAngle(bool low)
    {
        // Direction to target        
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr); 

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg); 
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }      

        return null; 
    }
}
