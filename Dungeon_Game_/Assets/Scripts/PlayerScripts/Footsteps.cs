using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Footsteps : MonoBehaviour
{
    CharacterController cc;
    AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cc.isGrounded == true && cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {
            AS.volume = Random.Range(0.8f, 1);
            AS.pitch = Random.Range(0.8f, 1.1f);
            AS.Play();
        }
    }
}
