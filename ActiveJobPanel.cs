using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveJobPanel : MonoBehaviour {

	public GameObject[] positions;
	public List<GameObject> currentJobs;
	public JobReceiver jobReceiver;

	[HideInInspector]
	public bool activated;

	private bool startLerp;
	public GameObject lerpObject;

	public float movementTime;
	private float timeStarted;
	private float timeSinceStarted;
	private Vector3 startPos;
	private Vector3 newPos;

	private bool moving;

	// Use this for initialization
	void Start () {
		currentJobs = new List<GameObject>();
		startLerp = false;
		moving = false;
	}

	// Update is called once per frame
	void Update () {

		if(activated)
		{
			MoveJobOver();
		}

		if(startLerp)
		{
			moving = true;
		}

		if(moving)
		{
			LerpJob();
		}
	}

	void MoveJobOver()
	{
		activated = false;

		for(int i = 0; i < currentJobs.Count; i++)
		{
			if(currentJobs[i].transform.position != positions[i].transform.position)
			{
				//currentJobs[i].transform.position = positions[i].transform.position;
				lerpObject = currentJobs[i].gameObject;
				newPos = positions[i].transform.position;

				startLerp = true;

				jobReceiver.DestroyNotice(0);

				i += positions.Length;
			}
		}

	}

	void LerpJob()
	{

		if(startLerp)
		{
			timeStarted = Time.time;
			startPos = lerpObject.transform.position;
			startLerp = false;
		}




		float timeSinceStarted = Time.time - timeStarted;
		float percentComplete = timeSinceStarted / movementTime;

		lerpObject.transform.position = new Vector3(Mathf.Lerp(startPos.x, newPos.x, percentComplete), lerpObject.transform.position.y, 0f);// , Mathf.Lerp(startPos.y, newPos.y, percentComplete), 0f);

		if(percentComplete >= 1.0f)
		{
			moving = false;
			timeStarted = 0;
		}

	}
}
