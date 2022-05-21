using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    private float speed=.05f;
    private float jumpSpeed=.05f;
    private bool sneaked=false;
    public GameObject player;
    public Collider coll;
    private int faceR=1;
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
        faceR= Input.GetAxis("Horizontal")>0 ? 1 :  ((Input.GetAxis("Horizontal")==0) ? faceR : -1);
        //Debug.Log("translation * speed" + horizontal); 

        if(Input.GetAxis("Vertical")>0.1){
            jump=jumpSpeed + (sneaked ? .02f : 0f);
            //Debug.Log("Jump value : " + jump);
        }
        else if(Input.GetAxis("Vertical")<0){
            Debug.Log("sneak");
            sneaked=true;
            return;
        }
        if(Input.GetKeyDown("space")){ //Dash
            horizontal=faceR * 2f;
        }
        sneaked=false;
        //player.transform.forward=new Vector3(horizontal,0,0);
        player.transform.Translate(horizontal,jump,0);
    }
}