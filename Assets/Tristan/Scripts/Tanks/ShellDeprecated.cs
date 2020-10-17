using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDeprecated : MonoBehaviour
{
    public GameObject explosion;

    // 10 kg bullet
    float mass = 10;
    float force = 100;
    float acceleration;
    float speedZ;
    float speedY;
    float gravity = -9.8f;
    float gAcceleration;


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        acceleration = force / mass;
        gAcceleration = gravity / mass;
        speedZ += acceleration * Time.deltaTime;
        speedY += gAcceleration * Time.deltaTime;
        this.transform.Translate(0, speedY, speedZ);

        // Ensure it is not negative force. 
        force = Mathf.Clamp(force, 0.1f, 10);
        force -= 0.5f;
    }
}
