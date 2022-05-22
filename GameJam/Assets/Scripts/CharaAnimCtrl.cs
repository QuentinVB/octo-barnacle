using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkMode
{
    idle,
    walking,
    running,
    crouching
}
public class CharaAnimCtrl : MonoBehaviour
{
    public Animator anim;
    public RuntimeAnimatorController animCtrl;
    public WalkMode walkmode = WalkMode.idle;

    private bool isRunning;
    private bool isJumping;
    private bool isDashing;
    private bool isCrouching;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //SECURITY (load the animatorController into the animator if empty)
        if (anim.runtimeAnimatorController == null)
        {
            Debug.Log(string.Format("load default Animation Controller"));
            anim.runtimeAnimatorController = animCtrl;
        }
        isRunning = anim.GetBool("isRunning");
        isJumping = anim.GetBool("isJumping");
        isDashing = anim.GetBool("isDashing");
        isCrouching = anim.GetBool("isCrouching");
    }

    // Update is called once per frame
    void Update()
    {
        resetBool();
        switch (walkmode)
        {
            case WalkMode.idle:
                break;
            case WalkMode.running:
                isRunning = true;
                break;
            case WalkMode.crouching:
                isCrouching = true;
                break;
            default:
                break;
        }

        //set the state of animation

        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isCrouching", isCrouching);

    }

    private void resetBool()
    {
        isRunning = false;
        isJumping = false;
        isDashing = false;
        isCrouching = false;
    }

    public WalkMode WalkMode { get { return walkmode; } internal set { walkmode = value; } }
}
