using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    public float speed=9f;
    public float speedmax=19f;
    public float jumpSpeed=1.8f;


    private int sneaked=1;
    public GameObject player;
    public Collider coll;
    public Rigidbody rigidbody;
    public Animator animator;

    private float coolDownDash=1f;
    private float dashUp;
    public List<Vector3> respawnPoint=new List<Vector3>();
    public int respawnFlag=0;
    private int faceR=1;
    bool grounded=false;
    private bool dashed=false;
    // Start is called before the first frame update

    [SerializeField]
    private float deathLevel = -15.0f;


    void Start()
    {
        player.name="bob"; // temp name
        coll=GetComponent<Collider>();
        coll.isTrigger = false;
        rigidbody =GetComponent<Rigidbody>();
        rigidbody.freezeRotation=true;       
        dashUp=Time.time;
        animator.Play("BobRigging|Bob-Idle1");
    }

    private void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,-9.81f,0) );
        Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 1.21f);
        grounded=false;
        if(player.transform.position.y<-15){return;}
        foreach(Collider collid in hitColliders){
            if(collid.transform.position==rigidbody.position){continue;}
            if(collid.transform.position.y < rigidbody.position.y){grounded=true;}
            if(collid.name.Contains("spike")){Respawn(respawnFlag);}
        }
        if(!grounded && rigidbody.velocity.x>12){rigidbody.AddForce(-12f,0,0);}
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Death"){Respawn(respawnFlag);}

        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(other.transform, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sneaked=1;
        if(Input.GetKeyDown("r")){
            Debug.Log(rigidbody.position.y);
        }
        if(rigidbody.position.y < deathLevel){
            Debug.Log("mort");
            Respawn(respawnFlag);
        }
        float horizontal=0f;
        float jump=0f;
        //faceR= Input.GetAxis("Horizontal")>0 ? -1 :  ((Input.GetAxis("Horizontal")==0) ? faceR : 1);
        if(Input.GetKey("left shift")){
            Debug.Log("dash");
            if(Time.time<dashUp){Debug.Log("can't dash");}
            else if(Input.GetKey("d") ){horizontal=Dash(1);dashUp=coolDownDash+Time.time;}
            else if(Input.GetKey("q")){horizontal=Dash(-1);dashUp=coolDownDash+Time.time;}
            rigidbody.AddForce(horizontal,0f,0f);
            horizontal=0f;
        }
        else{
            if(Input.GetKey("d")){horizontal+=-rigidbody.velocity.x +speed;}
            else if(Input.GetKey("q")){horizontal+=-(rigidbody.velocity.x +speed);}
            if(!grounded){horizontal/=6;}
        }
       
        
        // if (Input.GetAxis("Vertical")<0){
        //     sneaked=2;
        //     return;
        // }
        
        if(Input.GetKeyDown("space") && grounded && rigidbody.velocity.x<speedmax){ //jump
            jump=jumpSpeed*3;
            grounded=false;
            dashed=false;
        }
        rigidbody.AddForce(new Vector3(horizontal,jump,0f));
    }

    public void Respawn(int currentLevel){
        //Debug.Log(currentLevel);
        transform.SetParent(null);
        transform.position=respawnPoint[currentLevel];
    }
    public float Dash(int direction){
        return direction * 500;
    }
}