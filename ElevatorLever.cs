using UnityEngine;
using System.Collections;

public class ElevatorLever : MonoBehaviour {

	public GameObject elevator;
	public GameObject[] positions;

	[HideInInspector]
	public bool activated;
	[HideInInspector]
	public bool moving;

	public float movementTime;

	private Animator anim;
	private float timeStarted;
	private Vector2 startPos;
	private Vector2 newPos;
	// Use this for initialization
	void Start () {

		activated = false;
		moving = false;
	}

	// Update is called once per frame
	void Update () {
		if(activated)
		{
			MoveElevator();
		}

		if(moving)
		{
			startLerp();
			activated = false;
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
			timeStarted = Time.time;
			startPos = this.gameObject.transform.position;
			if(this.gameObject.transform.position == positions[0].transform.position)
			{
				newPos = positions[1].transform.position;
			}
			else if(this.gameObject.transform.position == positions[1].transform.position)
			{
				newPos = positions[0].transform.position;
			}
		}
		float timeSinceStarted = Time.time - timeStarted;
		float percentComplete = timeSinceStarted / movementTime;
		elevator.transform.position = Vector2.Lerp(startPos,newPos, percentComplete);

		if(percentComplete >= 1.0f)
		{
			moving = false;
		}
	}
}
