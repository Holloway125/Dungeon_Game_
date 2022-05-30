using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    Rigidbody myRB;

    [SerializeField] float throwSpeed;
    [SerializeField] GameObject Burst;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.velocity = transform.forward * throwSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Burst, transform.position, transform.rotation);
        // Add interactions here
        if(collision.transform.tag == "Enemy")
        {
            //get comp for enemy script
            //enemy - damage from health
        }
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
