using UnityEngine;
using System.Collections;

public class ElevatorLever : MonoBehaviour {

	public GameObject elevator;
	public GameObject[] positions;
	public Rigidbody2D rb;

	[HideInInspector]
	public bool activated;
	[HideInInspector]
	public bool moving;

	public float movementTime;

	private Animator anim;
	private float timeStarted;
	private Vector3 startPos;
	private Vector3 newPos;
	private float distanceToMove;

	private bool animSwitch;
	// Use this for initialization
	void Start () {

		activated = false;
		moving = false;
		animSwitch = true;

		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(activated)
		{
			MoveElevator();
		}

		if(moving)
		{
			startLerp();

		}
	}

	void MoveElevator()
	{
		moving = true;
	}

	void startLerp()
	{
		if(activated)
		{
			activated = false;
			animSwitch = !animSwitch;
			anim.SetBool("Switched", animSwitch);
			timeStarted = Time.time;
			startPos = elevator.gameObject.transform.position;
			if(elevator.gameObject.transform.position.y == positions[0].transform.position.y)
			{
				newPos = positions[1].transform.position;
				distanceToMove = newPos.y - startPos.y;
			}
			else if(elevator.gameObject.transform.position.y > positions[0].transform.position.y)
			{
				newPos = positions[0].transform.position;
				distanceToMove = startPos.y - newPos.y;
			}

		}
		float timeSinceStarted = Time.time - timeStarted;
		float percentComplete = timeSinceStarted / movementTime;
		elevator.transform.position = new Vector3(elevator.transform.position.x, Mathf.Lerp(startPos.y, newPos.y, percentComplete), 0f);//.Lerp(startPos,newPos, percentComplete);

		//Debug.Log("percent complete: " + percentComplete * 100f + "%");
		if(percentComplete >= 1.0f)
		{
			moving = false;
			timeStarted = 0;
		}
	}
}
