using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    public float speed=2000f;
    public float jumpSpeed=20000f;
    private bool sneaked=false;
    public GameObject player;
    public Collider coll;
    public Rigidbody rigidbody;
    public List<Vector3> respawnPoint=new List<Vector3>();
    private int faceR=1;
    private bool jumped=true;
    // Start is called before the first frame update

    [SerializeField]
    private float deathLevel = -15.0f;

    void Start()
    {
        respawnPoint.Add(new Vector3(0f,2f,0f));
        Physics.gravity=new Vector3(0, -1.0F, 0);
        player.name="bob"; // temp name
        coll=GetComponent<Collider>();
        coll.isTrigger = false;
        rigidbody =GetComponent<Rigidbody>();
        rigidbody.freezeRotation=true;
        
        //Debug.Log(coll.attachedRigidbody.name);
    }

    private void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,-12f,0) * rigidbody.mass);
    }
    private void OnCollisionEnter(Collision other) {
        //Debug.Log("test");
        jumped=false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r")){
            Debug.Log(rigidbody.position.y);
            }
        if(rigidbody.position.y < deathLevel){
            Debug.Log("mort");
            Respawn(0);
        }
        float horizontal=0f;
        float jump=0f;
        horizontal = Input.GetAxis("Horizontal") * speed; // negatif gauche , positif droite
        faceR= Input.GetAxis("Horizontal")>0 ? 1 :  ((Input.GetAxis("Horizontal")==0) ? faceR : -1);
        //Debug.Log("translation * speed" + horizontal); 

        if(Input.GetAxis("Vertical")>0.2 && rigidbody.velocity.y==0f && !jumped){
            jump=jumpSpeed;
        }
        if(Input.GetAxis("Vertical")<0){
            //sneaked=true;
            return;
        }
        if(Input.GetKeyDown("space")){ //Dash
            horizontal=faceR * 2f;
        }
        Vector3 vec=new Vector3(horizontal*Time.deltaTime,jump*Time.deltaTime,0.0f);
        rigidbody.AddForce(vec);
    }

    void Respawn(int currentLevel){
        rigidbody.position=respawnPoint[currentLevel];
    }
}