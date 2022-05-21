using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed=10f;
    private float jumpSpeed=5f;
    private bool sneaked=false;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        //10 metres par seconde
        float horizontal = Input.GetAxis("Horizontal") * speed; // negatif gauche , positif droite
        float vertical = Input.GetAxis("Horizontal") ;
        horizontal *=Time.deltaTime;
        transform.Translate(horizontal,0,0);
        //Debug.Log("translation * speed" + horizontal); 

        if(Input.GetAxis("Vertical")>0.1){
            Debug.Log("jump");
            float jump=jumpSpeed + (sneaked ? 2f : 0f);
            Debug.Log(jump);
        }
        else if(Input.GetAxis("Vertical")<0){
            Debug.Log("sneak");
            sneaked=true;
            return;
        }     
        sneaked=false;
        

    }
}
