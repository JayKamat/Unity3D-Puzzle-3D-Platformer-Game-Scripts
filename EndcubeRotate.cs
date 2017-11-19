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

            //Set Constraints
            rbMyPlayer.constraints = ~rbMyPlayer.constraints & ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            //Fix Player Position
            goMyCurrentPlayer.transform.position = tRotation.position;

            if (fXpos == -100 & fZpos==0)
            {
                //Rotate Cam
                if ((rbMyPlayer.constraints & RigidbodyConstraints.FreezePositionX) != 0)
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, 90);
                }
                else {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, -90);
                }

            }
            else if (fXpos == 100 & fZpos == 0)
            {
                //Rotate Cam
                if ((rbMyPlayer.constraints & RigidbodyConstraints.FreezePositionZ) != 0)
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, 90);
                }
                else
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, -90);
                }

            }
            else if (fXpos == -100 & fZpos == 100)
            {
                //Rotate Cam
                if ((rbMyPlayer.constraints & RigidbodyConstraints.FreezePositionZ) != 0)
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, 90);
                }
                else
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, -90);
                }

            }
            else if (fXpos == 100 & fZpos == 100)
            {
                //Rotate Cam
                if ((rbMyPlayer.constraints & RigidbodyConstraints.FreezePositionX) != 0)
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, 90);
                }
                else
                {
                    tMyPlayerSetup.RotateAround(tRotation.position, Vector3.up, -90);
                }

            }

        }
    }
}
