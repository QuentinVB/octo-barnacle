using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Number=0;
    public Rigidbody rb;
    private void FixedUpdate() {
        Collider[] hitColliders = Physics.OverlapSphere(rb.position, 1f);
        foreach(Collider collid in hitColliders){
            if(collid.attachedRigidbody==rb){continue;}
            if(collid.name=="bob"){collid.gameObject.GetComponent<Player>().respawnFlag=Number;}
        }
    }
}
