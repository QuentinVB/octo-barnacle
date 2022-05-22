using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Number=0;
    public Rigidbody rb;
    GameObject sphere;
    private void Start() {
        rb.isKinematic=true;
        rb.useGravity=false;
    }
    
    private void FixedUpdate() {
        Collider[] hitColliders = Physics.OverlapSphere(rb.position, 1f);
        foreach(Collider collid in hitColliders){
            if(collid.tag=="Player"){
                collid.gameObject.GetComponent<Player>().respawnFlag=Number;
                rb.gameObject.SetActive(false);
            }
        }
    }
}
