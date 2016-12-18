using UnityEngine;
using System.Collections;

public class LerpObject : MonoBehaviour {

	private Vector3 position;

	private Vector3 startPos;
	public Vector3 newPos;
	public float movementTime;

	private float timeStarted;
	private float timeSinceStarted;

	private bool moving;
	public bool startLerp;

	[HideInInspector]
	public bool activated;
	// Use this for initialization
	void Start () {

		startLerp = false;
		moving = false;
	}

	// Update is called once per frame
	void Update () {


	}

	void FixedUpdate()
	{
		if(startLerp)
		{
			moving = true;
		}

		if(moving)
		{
			LerpJob();
		}
	}

	/*
	void MoveJobOver()
	{
		activated = false;

			if(position != positions[activeJobs.Count].transform.position)
			{
				//currentJobs[i].transform.position = positions[i].transform.position;
				lerpObject = currentJobs[i].gameObject;
				newPos = positions[activeJobs.Count].transform.position;

				startLerp = true;

				if(jobReceiver.clones.Count > 0)
				{
					jobReceiver.DestroyNotice(0);
				}

				activeJobs.Add(currentJobs[i].gameObject);
				currentJobs.RemoveAt(i);


				i += positions.Length;
			}
		}

	}
	*/

	void LerpJob()
	{

		if(startLerp)
		{
			timeStarted = Time.time;
			startPos = transform.position;
			startLerp = false;
		}




		float timeSinceStarted = Time.time - timeStarted;
		float percentComplete = timeSinceStarted / movementTime;
		//float percentComplete = Mathf.Abs(lerpObject.transform.position.x) / Mathf.Abs(newPos.x);

		transform.position = new Vector3(Mathf.Lerp(startPos.x, newPos.x, percentComplete), transform.position.y, 0f);// , Mathf.Lerp(startPos.y, newPos.y, percentComplete), 0f);

		if(percentComplete >= 1.0f)
		{
			moving = false;
			timeStarted = 0;
		}

	}
}
