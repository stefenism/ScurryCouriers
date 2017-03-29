using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveJobPanel : MonoBehaviour {

	public GameObject[] positions;
	public List<GameObject> currentJobs;
	public List<GameObject> activeJobs;
	public JobReceiver jobReceiver;


	[HideInInspector]
	public bool activated;
	/*
	private bool startLerp;
	public GameObject lerpObject;

	public float movementTime;
	private float timeStarted;
	private float timeSinceStarted;
	private Vector3 startPos;
	private Vector3 newPos;

	private bool moving;
	*/

	// Use this for initialization
	void Start () {
		currentJobs = new List<GameObject>();
		activeJobs = new List<GameObject>();
		//startLerp = false;
		//moving = false;
	}

	// Update is called once per frame
	void Update () {

	if(activated)
		{
			MoveJobOver();
		}
	}

	void FixedUpdate()
	{
		/*
		if(startLerp)
		{
			moving = true;
		}

		if(moving)
		{
			LerpJob();
		}
		*/
	}


	void MoveJobOver()
	{
		activated = false;

		for(int i = 0; i < currentJobs.Count; i++)
		{


				if(activeJobs.Count < positions.Length)
				{
					Debug.Log("currentjobs.count " + currentJobs.Count);
					Debug.Log("i = " + i);

					if(currentJobs[i].transform.position != positions[activeJobs.Count].transform.position)
					{
						currentJobs[i].gameObject.GetComponent<LerpObject>().newPos = positions[activeJobs.Count].transform.position;
						currentJobs[i].gameObject.GetComponent<LerpObject>().newPosArrayPosition = activeJobs.Count;
						currentJobs[i].gameObject.GetComponent<LerpObject>().startLerp = true;
						currentJobs[i].gameObject.transform.parent = this.gameObject.transform;

						activeJobs.Add(currentJobs[i].gameObject);
						currentJobs.RemoveAt(i);
					}

				}
				//currentJobs[i].transform.position = positions[i].transform.position;
				//lerpObject = currentJobs[i].gameObject;
				//newPos = positions[activeJobs.Count].transform.position;

				//startLerp = true;



				if(jobReceiver.clones.Count > 0)
				{
					jobReceiver.DestroyNotice(0);
				}


				i += positions.Length;
			}
		}



	public void MoveAllJobsOver()
	{
		for(int i = 0; i < activeJobs.Count; i++)
		{
			activeJobs[i].gameObject.GetComponent<LerpObject>().newPos = positions[i].transform.position;
			activeJobs[i].gameObject.GetComponent<LerpObject>().newPosArrayPosition = i;
			activeJobs[i].gameObject.GetComponent<LerpObject>().startLerp = true;


			//lerpObject = activeJobs[i].gameObject;
			//newPos = positions[i].transform.position;

			//startLerp = true;
		}
	}

	/*
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
		//float percentComplete = Mathf.Abs(lerpObject.transform.position.x) / Mathf.Abs(newPos.x);

		lerpObject.transform.position = new Vector3(Mathf.Lerp(startPos.x, newPos.x, percentComplete), lerpObject.transform.position.y, 0f);// , Mathf.Lerp(startPos.y, newPos.y, percentComplete), 0f);

		if(percentComplete >= 1.0f)
		{
			moving = false;
			timeStarted = 0;
		}

	}

	*/
}
