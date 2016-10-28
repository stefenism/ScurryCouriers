using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;

	//private PlaySounds playSounds;

	private bool canControl;
	private bool facingRight;
	private bool jumping;
	private bool canCarry;
	private bool canJump;
	private bool jumpAllowed;
	private bool dropAllowed;


	private bool interact;
	private bool holding;

	private bool elevatorInteract;

	private GameObject carryObject;
	private GameObject carryObjectParent;

	private GameObject interactObject;

	public float speed;
	public float maxSpeed;
	public float carrySpeed;
	private float maxVelocity;
	public float jumpForce;
	public float jumpTime;
	private float jumpDuration;

	public bool grounded;

	public SpriteRenderer exclamation;
	public GameObject carryPosition;
	// Use this for initialization
	void Awake () {


		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//playSounds = GetComponent<PlaySounds>();

		grounded = false;
		dropAllowed = false;
		canControl = true;
		facingRight = true;
		canCarry = false;
		canJump = true;
		jumpAllowed = true;
		dropAllowed = true;
		interact = false;
		holding = false;

		elevatorInteract = false;

		maxVelocity = maxSpeed;
	}

	// Update is called once per frame
	void Update () {

		//print(jumping + " jumping");
		CheckInput();

	}
	void FixedUpdate()
	{
		if(jumping)
		{
			Jump();
		}
	}

	void CheckInput()
	{
		float moveDir = Input.GetAxis("Horizontal");
		Run(moveDir);
		if(grounded)
		{
			jumping = false;
			jumpDuration = jumpTime;
			dropAllowed = false;
			this.gameObject.layer = 12;  //12 is the player layer
		}

		else if(!grounded)
		{
			dropAllowed = true;
		}

		if(canJump && !canCarry)
		{
			JumpButton();
		}

		if(canCarry)
		{

			CarryButton();
		}

		if(dropAllowed)
		{
			Drop();
		}

		if(elevatorInteract)
		{
			Elevator();
		}

		if(moveDir > 0 && !facingRight)
		{
			Flip();
		}

		else if(moveDir < 0 && facingRight)
		{
			Flip();
		}

		else
		{
			jumpDuration -= Time.deltaTime;
		}

		DetermineJumpButton();

	}

	void Run(float moveDir)
	{
		rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

		if(Mathf.Abs(rb.velocity.x) > maxVelocity)
		{
			rb.velocity = new Vector2((maxSpeed ), rb.velocity.y);
		}
	}

	void JumpButton()
	{
		if(Input.GetKey(KeyCode.Space) && jumpAllowed)
		{
			jumping = true;
		}
		anim.SetFloat("vspeed", rb.velocity.y);

	}

	void Drop()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Physics2D.IgnoreLayerCollision(12,10);
		}
		else
		{
			Physics2D.IgnoreLayerCollision(12,10,false);
		}
	}

	void DetermineJumpButton()
	{
		if(!Input.GetKey(KeyCode.Space))
		{
			jumpAllowed = true;
		}
	}

	void CarryButton()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			interact = true;
			PickupPutdown();
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			interact = false;
		}

		print("interact " + interact);
	}

	void Jump()
	{
		if(jumpDuration > 0f)
		{
			rb.AddForce(new Vector2(0f,1f * jumpForce), ForceMode2D.Impulse);
		}
		else if(jumpDuration <= 0f)
		{
			jumpAllowed = false;
		}

	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void PickupPutdown()
	{
		if(holding)
		{
			canCarry = false;
			holding = false;
			StartCoroutine(DropWait());
			maxVelocity = maxSpeed;
			speed = maxSpeed;
			carryObject.transform.parent = carryObjectParent.transform;
		}

		else if(!holding)
		{
			if(carryObject.tag == "Water" || carryObject.tag == "Honey")
			{
				maxVelocity = carrySpeed;
				speed = carrySpeed;
			}
			holding = true;
			carryObjectParent = carryObject.transform.parent.gameObject;
			carryObject.transform.parent = transform;
			carryObject.transform.position = carryPosition.transform.position;
		}
	}

	void Elevator()
	{

		if(Input.GetKeyDown(KeyCode.Space))
		{
			interactObject.GetComponent<ElevatorLever>().activated = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Food" || collision.gameObject.tag == "Water"
			|| collision.gameObject.tag == "Honey")
		{
				exclamation.enabled = true;
				canCarry = true;
				canJump = false;

				if(!holding)
				{
					carryObject = collision.gameObject;
				}

		}

		if(collision.gameObject.tag == "ShopRat")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
		}

		if(collision.gameObject.tag == "Lever")
		{
			exclamation.enabled = true;
			canJump = false;
			elevatorInteract = true;
			interactObject = collision.gameObject;
		}

	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Food" || collision.gameObject.tag == "Water"
			|| collision.gameObject.tag == "Honey")
		{
				exclamation.enabled = false;
				interact = false;
				if(!holding)
				{
					canCarry = false;
					canJump = true;
				}
		}

		if(collision.gameObject.tag == "ShopRat")
		{
			exclamation.enabled = false;
			interactObject = null;
		}

		if(collision.gameObject.tag == "Lever")
		{
			exclamation.enabled = false;
			canJump = true;
			elevatorInteract = false;
			interactObject = null;
		}
	}

	private IEnumerator DropWait()
	{
		yield return new WaitForSeconds(.075f);
		canJump = true;
	}
}


//rb.AddForce(moveDir * horMov * runSpeed, ForceMode.Impulse);
