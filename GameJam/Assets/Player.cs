using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Player : MonoBehaviour
{
    public float speed=9f;
    public float jumpSpeed=1.8f;
    private int sneaked=1;
    public GameObject player;
    public Collider coll;
    public Rigidbody rigidbody;
    public List<Vector3> respawnPoint=new List<Vector3>();
    private int faceR=1;
    private bool jumped=false;
    bool grounded=false;
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
        
        //Debug.Log(coll.attachedRigidbody.name);
        
    }

    private void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,-9.81f,0) * rigidbody.mass);
        Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 1.25f);
        grounded=false;
        foreach(Collider collid in hitColliders){
            if(collid.transform.position==rigidbody.position){continue;}
            if(collid.transform.position.y < rigidbody.position.y){grounded=true;}
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name=="Death"){
            Respawn(0);
        }

        if (other.gameObject.tag == "MovingPlatform")
            transform.SetParent(other.transform, true);
        //Debug.Log("grounded : " + grounded);
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
            Respawn(0);
        }
        float horizontal=0f;
        float jump=0f;
        horizontal = Input.GetAxis("Horizontal") * speed; // negatif gauche , positif droite
        faceR= Input.GetAxis("Horizontal")>0 ? -1 :  ((Input.GetAxis("Horizontal")==0) ? faceR : 1);

        if(Input.GetKeyDown("d")){horizontal=Dash(1);}
        else if(Input.GetKeyDown("q")){horizontal=Dash(-1);}
        else if(Input.GetAxis("Vertical")>0 && grounded){
            Debug.Log("Jump");
            jump=jumpSpeed;
            jumped=true;
            grounded=false;
        }
        if (Input.GetAxis("Vertical")<0){
            sneaked=2;
            return;
        }
        Debug.Log(grounded);
        
        if(Input.GetKeyDown("space") && grounded && rigidbody.velocity.y<1){ //jump
            //horizontal=speed *faceR * 2000f * Time.deltaTime;
            jump=jumpSpeed;
        }
        Vector3 vec=new Vector3(horizontal*Time.deltaTime/sneaked,jump,0.0f);
        transform.Translate(vec);

    }

    public void Respawn(int currentLevel){
        rigidbody.position=respawnPoint[currentLevel];
    }
    public float Dash(int direction){
        Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 3.6f);
        foreach(Collider collid in hitColliders){
            if(collid.transform.position!=rigidbody.transform.position){
                if(direction==-1 && collid.transform.position.x<rigidbody.transform.position.x){
                    Debug.Log(" pas Dash yeah!");
                    return 0.0f;
                }
                else if(direction==1 && collid.transform.position.x>rigidbody.transform.position.x){
                    Debug.Log("pas Dash yeah!");
                    return 0.0f;
                }
            }
        }
        return speed *direction * 2000f * Time.deltaTime;
    }
}