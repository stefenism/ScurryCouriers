using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveJobPanel : MonoBehaviour {

	public GameObject[] positions;
	public List<GameObject> currentJobs;
	public JobReceiver jobReceiver;

	[HideInInspector]
	public bool activated;

	// Use this for initialization
	void Start () {
		currentJobs = new List<GameObject>();
	}

	// Update is called once per frame
	void Update () {

		if(activated)
		{
			MoveJobOver();
		}
	}

	void MoveJobOver()
	{
		activated = false;

		for(int i = 0; i < positions.Length -1; i++)
		{
			if(currentJobs[i].transform.position != positions[i].transform.position)
			{
				currentJobs[i].transform.position = positions[i].transform.position;

				//jobReceiver.DestroyNotice(i);

				i += positions.Length;
			}
		}

	}
}
