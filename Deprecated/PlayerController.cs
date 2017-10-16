using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //movement variables
    bool canMove;
	public float runSpeed;
	public float crouchSpeed;

	//jumping variables
	bool grounded;
	Collider[] groundCollisions;
	float groundCheckRadius = 0.2f;
    float jumpTimer;
    float timeBetweenJump=1.5f;
    public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

	//CharacterController pcontroller;
	Animator PlayerAnimator;
	Rigidbody PlayerRB;
	Vector3 PlayerCurrent;
	Vector3 pmovement;

	bool facingRight=true;

	// Use this for initialization
	void Start () {
		PlayerAnimator = GetComponent<Animator>();
		PlayerRB = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		
		//Idle to Running Code

		if (Input.GetAxis("Horizontal") > 0.1f) {
			PlayerAnimator.SetBool ("isRunning", true);
			//pmovement = transform.forward * Input.GetAxis ("Horizontal") * pspeed;
			//pcontroller.SimpleMove (pmovement);
		} else {
			PlayerAnimator.SetBool ("isRunning", false);
		}


        jumpTimer += Time.deltaTime;
		//Player Jump Code
		if (Input.GetButtonDown ("Jump") && jumpTimer >= timeBetweenJump) {
			PlayerAnimator.SetBool ("isJumping", true);
            jumpTimer = 0f;
		} else {
			PlayerAnimator.SetBool ("isJumping", false);
		}

			
		//Player Interact Code
		if (Input.GetKey (KeyCode.KeypadEnter)) {
			PlayerAnimator.SetTrigger("Interact");
			PlayerAnimator.SetBool ("isHanging", true);
		}

        //Checking Ground
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;
            StartCoroutine(SetCanMove());
        }
        else
        {
            grounded = false;
            canMove = false;
        }

        /*
		//Correcting Player Alignment changes caused due to animations.
		PlayerCurrent = transform.position;
		PlayerCurrent.z = 0;
		transform.position = PlayerCurrent;
		*/
    }

	void FixedUpdate(){
		
		//Jumping Code
		if(grounded && Input.GetButtonDown("Jump") && jumpTimer >= timeBetweenJump)
        {
			grounded = false;
			PlayerAnimator.SetBool ("grounded", grounded);
			PlayerRB.AddForce (new Vector3 (0, jumpHeight, 0));
		}


        PlayerAnimator.SetBool("grounded", grounded);

		//Idle to Running Code
		float move = Input.GetAxis ("Horizontal");
		PlayerAnimator.SetFloat ("pspeed", Mathf.Abs (move));

		float crouching = Input.GetAxisRaw ("Fire3");
		PlayerAnimator.SetFloat ("pcrouch", Mathf.Abs (crouching));

        //Movement
        if (canMove)
        {
            if (crouching > 0)
                PlayerRB.velocity = new Vector3(move * crouchSpeed, PlayerRB.velocity.y, 0);
            else
                PlayerRB.velocity = new Vector3(move * runSpeed, PlayerRB.velocity.y, 0);
        }


        //Flipping Direction
        if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();	
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 playerScale = transform.localScale;
		playerScale.z *= -1;
		transform.localScale = playerScale;
	}

    IEnumerator SetCanMove()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
}