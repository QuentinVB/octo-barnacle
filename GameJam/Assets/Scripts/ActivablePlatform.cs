using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlatform : MovingPlatform
{
    private void Awake()
    {
        active = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            active = true;
            startTime = Time.time;
        }
    }
}
