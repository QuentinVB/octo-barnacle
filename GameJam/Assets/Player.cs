using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    public float speed=9f;
    public float jumpSpeed=20000f;
    private int sneaked=1;
    public GameObject player;
    public Collider coll;
    public Rigidbody rigidbody;
    public List<Vector3> respawnPoint=new List<Vector3>();
    private int faceR=1;
    private bool jumped=false;
    // Start is called before the first frame update

    [SerializeField]
    private float deathLevel = -15.0f;

    void Start()
    {
        respawnPoint.Add(new Vector3(738.08f,1.42f,-800.0405f));
        Physics.gravity=new Vector3(0, -1.0F, 0);
        player.name="bob"; // temp name
        coll=GetComponent<Collider>();
        coll.isTrigger = false;
        rigidbody =GetComponent<Rigidbody>();
        rigidbody.freezeRotation=true;
        
        //Debug.Log(coll.attachedRigidbody.name);
        
    }

    private void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,-9.81f,0) * rigidbody.mass);
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log("test");
        if(other.gameObject.name=="Death"){Respawn(0);}
        jumped=false;
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
            Respawn(0);
        }
        float horizontal=0f;
        float jump=0f;
        horizontal = Input.GetAxis("Horizontal") * speed; // negatif gauche , positif droite
        faceR= Input.GetAxis("Horizontal")>0 ? 1 :  ((Input.GetAxis("Horizontal")==0) ? faceR : -1);

        if(Input.GetAxis("Vertical")>0 && rigidbody.velocity.y<0f &&!jumped){
            jump=jumpSpeed;
            jumped=true;
            Debug.Log("lkgdfjnlkg");
        }
        if(Input.GetAxis("Vertical")<0){
            sneaked=2;
            return;
        }
        if(Input.GetKeyDown("space")){ //Dash
            Debug.Log("Dash");
            horizontal=faceR * 2000f * Time.deltaTime;
        }
        Vector3 vec=new Vector3(horizontal*Time.deltaTime/sneaked,jump*Time.deltaTime/sneaked,0.0f);
        transform.Translate(vec);

    }

    public void Respawn(int currentLevel){
        rigidbody.position=respawnPoint[currentLevel];
    }
}