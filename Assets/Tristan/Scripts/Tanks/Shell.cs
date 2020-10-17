using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rB; 

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rB = this.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Set z-axis direction to velocity vector direction. 
        this.transform.forward = rB.velocity; 
    }
}
