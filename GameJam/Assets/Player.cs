using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    private float speed=10f;
    private float jumpSpeed=5f;
    private bool sneaked=false;
    public GameObject player;
    public Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        player.name="bob"; // temp name
        coll.isTrigger = true;
        coll.attachedRigidbody.useGravity=true;
        //Debug.Log(coll.attachedRigidbody.name);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal=0f;
        float jump=0f;
        //10 metres par seconde
        horizontal = Input.GetAxis("Horizontal") * speed; // negatif gauche , positif droite
        float vertical = Input.GetAxis("Horizontal") ;
        horizontal *=Time.deltaTime;
        //Debug.Log("translation * speed" + horizontal); 

        if(Input.GetAxis("Vertical")>0.1){
            jump=jumpSpeed + (sneaked ? 2f : 0f);
            //Debug.Log("Jump value : " + jump);
        }
        else if(Input.GetAxis("Vertical")<0){
            Debug.Log("sneak");
            sneaked=true;
            return;
        }     
        sneaked=false;
        transform.Translate(horizontal,jump,0);
    }
}