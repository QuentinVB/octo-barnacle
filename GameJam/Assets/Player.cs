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
    private int respawnFlag=0;
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
        
        //Debug.Log(coll.attachedRigidbody.name);
        
    }

    private void FixedUpdate() {
        rigidbody.AddForce(new Vector3(0,-9.81f,0) * rigidbody.mass);
        Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 1.25f);
        grounded=false;
        foreach(Collider collid in hitColliders){
            if(collid.transform.position==rigidbody.position){continue;}
            if(collid.transform.position.y < rigidbody.position.y){grounded=true;}
            if(collid.name.Contains("spike")){Respawn(respawnFlag);}
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name=="Death"){Respawn(respawnFlag);}
        Debug.Log(other);
        
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(other.transform, true);
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
            grounded=false;
        }
        if (Input.GetAxis("Vertical")<0){
            sneaked=2;
            return;
        }
        
        if(Input.GetKeyDown("space") && grounded){ //jump
            //horizontal=speed *faceR * 2000f * Time.deltaTime;
            jump=jumpSpeed*Time.deltaTime;
        }
        Vector3 vec=new Vector3(horizontal*Time.deltaTime/sneaked,0f,0.0f);
        //transform.Translate(vec);
        rigidbody.AddForce(new Vector3(horizontal,jump,0f));

    }

    public void Respawn(int currentLevel){
        rigidbody.position=respawnPoint[currentLevel];
    }
    public float Dash(int direction){
        if(!grounded && dashed){return 0.0f;}
        Collider[] hitColliders = Physics.OverlapSphere(rigidbody.position, 10f);
        foreach(Collider collid in hitColliders){
            if(collid.transform.position!=rigidbody.transform.position && collid.name!="sol"){
                if(direction==-1 && collid.transform.position.x<rigidbody.transform.position.x){
                    Debug.Log(collid.name);
                    dashed=true;
                    return 9 *direction * 5000f * Time.deltaTime/10 * (-collid.transform.position.x + rigidbody.position.x);
                }
                else if(direction==1 && collid.transform.position.x>rigidbody.transform.position.x){
                    Debug.Log(collid.name);
                    dashed=true;
                    return 9 *direction * 5000f * Time.deltaTime/10 * (collid.transform.position.x - rigidbody.position.x);
                }
            }
        }
        dashed=true;
        return 9 *direction * 5000f * Time.deltaTime;
    }
}