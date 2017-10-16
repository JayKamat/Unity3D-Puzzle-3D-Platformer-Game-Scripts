using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour {

    public Transform LedgeRaycast;
    public Invector.CharacterController.vThirdPersonController InvectorMovementScript;
    public Invector.CharacterController.vThirdPersonInput InvectorMovementInput;

    //Private
    Animator PlayerAnimController;
    Rigidbody PlayerRB;
    RaycastHit LedgeDetect;

    // Use this for initialization
    void Start () {
        PlayerAnimController = GetComponent<Animator>();
        PlayerRB = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        
        if(Physics.Raycast(LedgeRaycast.position, LedgeRaycast.forward, out LedgeDetect, 0.6f))
        {
            if (LedgeDetect.transform.CompareTag("Cube"))
            {
                PlayerAnimController.SetBool("canGrab", true);
                //InvectorMovementScript.enabled = false;
                InvectorMovementInput.enabled = false;
            }

        }
        else
        {
            PlayerAnimController.SetBool("canGrab", false);
            //InvectorMovementScript.enabled = true;
            InvectorMovementInput.enabled = true;
        }

    }

    // Update is called once per frame
    void Update () {
        
	}
}
