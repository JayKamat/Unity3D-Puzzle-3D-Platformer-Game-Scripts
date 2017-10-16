using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndcubeRotate : MonoBehaviour {

    //Public Variables
    public Transform tRotation;

    //Private Variables
    float fXpos, fYpos, fZpos;
    Transform tCurrentEndCube;

    Rigidbody rbMyPlayer;
    GameObject goMyCurrentPlayer;

    //CameraFollow scCameraScript;
    Transform tMyPlayerSetup;

    bool Turn;

    // Use this for initialization
    void Start () {
        tCurrentEndCube = GetComponent<Transform>();

        //Get Camera Follow script
        //scCameraScript = Camera.main.gameObject.GetComponent<CameraFollow>();

        fXpos = tCurrentEndCube.position.x;
        fYpos = tCurrentEndCube.position.y;
        fZpos = tCurrentEndCube.position.z;

        Turn = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goMyCurrentPlayer = other.gameObject;
            rbMyPlayer = goMyCurrentPlayer.GetComponent<Rigidbody>();
            tMyPlayerSetup = goMyCurrentPlayer.transform.parent.transform;


            if (fXpos == -100)
            {
                if (Turn)
                {
                    //Set Constraints
                    rbMyPlayer.constraints = RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    //Flip Bool
                    Turn = !Turn;
                    //Fix Player Position
                    goMyCurrentPlayer.transform.position = tRotation.position;


                    //scCameraScript.enabled = false;
                    //Rotate Cam
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, 90);
                    //scCameraScript.OffsetReset();
                    //scCameraScript.enabled = true;
                }
                else {
                    rbMyPlayer.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    Turn = !Turn;
                    goMyCurrentPlayer.transform.position = tRotation.position;

                    //scCameraScript.enabled = false;
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, -90);
                    //scCameraScript.OffsetReset();
                    //scCameraScript.enabled = true;
                }
                
            }

        }
    }
}
