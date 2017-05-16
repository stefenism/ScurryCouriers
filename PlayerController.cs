using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	[HideInInspector]
	public Rigidbody2D rb;
	private Animator anim;

	//private PlaySounds playSounds;

	private bool canControl;
	private bool facingRight;
	[HideInInspector]
	public bool jumping;
	private bool canCarry;
	private bool canBackpack;
	private bool canJump;
	private bool jumpAllowed;
	private bool dropAllowed;


	private bool interact;
	private bool holding;


	private bool elevatorInteract;
	private bool itemSpawnInteract;
	private bool jobSpawnInteract;

	[HideInInspector]
	public GameObject carryObject;
	private GameObject carryObjectParent;

	public List<GameObject> backPack;
	private GameObject backPackObject;
	private Sprite backPackImage;
	public Sprite backPackBlank;
	public GameObject backPackDisplay;

	private GameObject interactObject;
	private string interactObjectTag;

	public float speed;
	public float maxSpeed;
	public float carrySpeed;
	private float maxVelocity;
	public float jumpForce;
	public float jumpTime;
	private float jumpDuration;

	public bool grounded;
	public bool landed;

	public SpriteRenderer exclamation;
	public GameObject carryPosition;

	public GameObject jumpDust;

	public ActiveJobPanel jobPanel;

	public CameraPan cameraPan;

	// Use this for initialization
	void Start () {


		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//playSounds = GetComponent<PlaySounds>();

		backPack = new List<GameObject>();

		grounded = false;
		landed = false;
		canControl = true;
		facingRight = true;
		canCarry = false;
		canBackpack = false;
		canJump = true;
		jumpAllowed = true;
		dropAllowed = true;
		interact = false;
		holding = false;

		elevatorInteract = false;
		itemSpawnInteract = false;
		jobSpawnInteract = false;

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

		if(canCarry || canBackpack)
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

		if(itemSpawnInteract)
		{
			Spawn();
		}

		if(jobSpawnInteract)
		{
			SpawnJobDisplay();
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
		if(!grounded)
		{
			anim.SetFloat("vspeed", rb.velocity.y);
		}

		anim.SetBool("grounded", grounded);
		//Debug.Log("vspeed: " + rb.velocity.y);

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

			if(grounded)
			{
				jumpDust.GetComponent<SpawnParticles>().play = true;
			}
			if(!grounded)
			{
				jumpDust.GetComponent<SpawnParticles>().play = false;
			}
			//grounded = false;
		}

	}

	void Drop()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Physics2D.IgnoreLayerCollision(12,10);
			Physics2D.IgnoreLayerCollision(12,13);
		}
		else
		{
			Physics2D.IgnoreLayerCollision(12,10,false);
			Physics2D.IgnoreLayerCollision(12,13,false);
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
			if(canBackpack)
			{
				canBackpack = false;
				BackPack();
			}
			if(canCarry)
			{
				PickupPutdown();
			}
			//PickupPutdown();
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			interact = false;
		}

		//print("interact " + interact);
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

	public void PickupPutdown()
	{
		if(holding)
		{
			canCarry = false;
			holding = false;
			StartCoroutine(DropWait());
			maxVelocity = maxSpeed;
			speed = maxSpeed;
			carryObjectParent.GetComponent<MovePickupParent>().enabled = false;
			carryObject.transform.parent = carryObjectParent.transform;
			carryObject.tag = interactObjectTag;
			carryObject = null;
		}

		else //if(!holding)
		{
			if(carryObject.tag == "Water" || carryObject.tag == "Honey")
			{
				maxVelocity = carrySpeed;
				speed = carrySpeed;
			}
			holding = true;
			exclamation.enabled = false;
			carryObjectParent = carryObject.transform.parent.gameObject;
			carryObjectParent.GetComponent<MovePickupParent>().enabled = true;
			carryObject.transform.parent = transform;
			carryObject.transform.position = carryPosition.transform.position;
			interactObjectTag = carryObject.tag;
			carryObject.tag = "Player";
		}
	}

	public void BackPack()
	{
		if(backPack != null)
		{
			backPack.Add(backPackObject.transform.parent.gameObject);
			backPackObject.transform.parent.gameObject.SetActive(false);
			backPackImage = backPackObject.transform.parent.GetComponent<SpriteRenderer>().sprite;
			backPackDisplay.GetComponent<UpdateBackpackImage>().UpdateImage(backPackImage);
			//Destroy(carryObject.transform.parent.gameObject);
		}
	}

	public void ClearBackPack(int position)
	{
		backPackDisplay.GetComponent<UpdateBackpackImage>().UpdateImage(backPackBlank);
		Destroy(backPack[position]);
		backPack.RemoveAt(position);
	}

	void Elevator()
	{

		if(Input.GetKeyDown(KeyCode.Space) && interactObject.GetComponent<ElevatorLever>().moving == false)
		{
			interactObject.GetComponent<ElevatorLever>().activated = true;
		}
	}

	void Spawn()
	{
		if(Input.GetKeyDown(KeyCode.Space) && interactObject.GetComponent<SpawnItem>().spawning == false)
		{
			interactObject.GetComponent<SpawnItem>().activated = true;
		}
	}

	void SpawnJobDisplay()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(jobPanel.currentJobs.Count > 0 && jobPanel.currentJobs.Count <= 4);// != null)
			{
				jobPanel.activated = true;
				Debug.Log("currentjobs.count " + jobPanel.currentJobs.Count);
				//interactObject.GetComponent<JobReceiverold>().jobActivated = true;
				interactObject.GetComponent<JobManager>().jobActivated = true;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collision)
	{

		//Debug.Log("collision object: " + collision.gameObject.tag);
		if(collision.gameObject.tag == "Water"
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

		if(collision.gameObject.tag == "Food")
		{
			exclamation.enabled = true;
			canBackpack = true;
			backPackObject = collision.gameObject;
		}

		if(collision.gameObject.tag == "ShopRat")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
		}

		if(collision.gameObject.tag == "Bossy")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
			canJump = false;
			jobSpawnInteract = true;
		}

		if(collision.gameObject.tag == "Lever")
		{
			exclamation.enabled = true;
			canJump = false;
			elevatorInteract = true;
			interactObject = collision.gameObject;
		}

		if(collision.gameObject.tag == "Well")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
			itemSpawnInteract = true;
			canJump = false;
		}

		if(collision.gameObject.tag == "HoneyHive")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
			itemSpawnInteract = true;
			canJump = false;
		}

		if(collision.gameObject.tag == "RamenShop")
		{
			exclamation.enabled = true;
			interactObject = collision.gameObject;
			itemSpawnInteract = true;
			canJump = false;
		}

		if(collision.gameObject.tag == "Desk")
		{
			exclamation.enabled = true;
			//interactObject = collision.gameObject;
			//itemSpawnInteract = true;
			canJump = false;
		}

		if(collision.gameObject.tag == "RoomTrigger")
		{
			//cameraPan = collision.gameObject.transform.parent.gameObject.GetComponent<CameraPan>();
			//cameraPan.enabled = true;
			//Debug.Log("enabled: " + cameraPan.enabled);

			if(collision.gameObject.name == "LeftBorder")
			{
				//set min to outside min, set max to leftborder position
				cameraPan.enabled = true;
				cameraPan.leftSide = true;
			}
			if(collision.gameObject.name == "RightBorder")
			{
				//set min to RightBorder pos, set max to outside max
				cameraPan.enabled = true;
				cameraPan.rightSide = true;
			}
		}
		/*
		if(collision.gameObject.tag == "Disabler")
		{
			cameraPan = collision.gameObject.transform.parent.gameObject.GetComponent<CameraPan>();
			if(collision.gameObject.name == "InDisabler")
			{
				cameraPan.inside.SetActive(true);
				cameraPan.outside.SetActive(false);
			}
			if(collision.gameObject.name == "OutDisabler")
			{
				cameraPan.inside.SetActive(false);
				cameraPan.outside.SetActive(true);
			}
		}
		*/

	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Water"
			|| collision.gameObject.tag == "Honey")
		{
				exclamation.enabled = false;
				interact = false;
				if(!holding)
				{
					canCarry = false;
					canJump = true;
					carryObject = null;
				}
		}

		if(collision.gameObject.tag == "Food")
		{
			exclamation.enabled = false;
			canBackpack = false;
		}

		if(collision.gameObject.tag == "ShopRat")
		{
			exclamation.enabled = false;
			interactObject = null;
		}

		if(collision.gameObject.tag == "Bossy")
		{
			exclamation.enabled = false;
			interactObject = null;
			canJump = true;
			jobSpawnInteract = false;
		}

		if(collision.gameObject.tag == "Lever")
		{
			exclamation.enabled = false;
			canJump = true;
			elevatorInteract = false;
			interactObject = null;
		}

		if(collision.gameObject.tag == "Well")
		{
			exclamation.enabled = false;
			interactObject = null;
			itemSpawnInteract = false;
			canJump = true;
		}

		if(collision.gameObject.tag == "HoneyHive")
		{
			exclamation.enabled = false;
			interactObject = null;
			itemSpawnInteract = false;
			canJump = true;
		}

		if(collision.gameObject.tag == "RamenShop")
		{
			exclamation.enabled = false;
			interactObject = null;
			itemSpawnInteract = false;
			canJump = true;
		}

		if(collision.gameObject.tag == "Desk")
		{
			exclamation.enabled = false;
			//interactObject = null;
			//itemSpawnInteract = false;
			canJump = true;
		}
	}

	private IEnumerator DropWait()
	{
		yield return new WaitForSeconds(.075f);
		canJump = true;
	}
}


//rb.AddForce(moveDir * horMov * runSpeed, ForceMode.Impulse);
