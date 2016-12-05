using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateJobImage : MonoBehaviour {

	/*
	[HideInInspector]
	public Sprite jobItem;
	[HideInInspector]
	public Sprite jobDestination;
	*/

	private SpriteRenderer item;
	private SpriteRenderer destination;

	public GameObject jobDisplay;
	private GameObject clone;

	public GameObject[] positions;
	public List<GameObject> clones;



	// Use this for initialization
	void Start () {

		clones = new List<GameObject>();
	}

	// Update is called once per frame
	void Update () {

	}

	private void GenerateImage(Sprite jobItem, Sprite jobDestination)
	{
		item.sprite = jobItem;
		destination.sprite = jobDestination;

	}

	public void GenerateImageObject(Sprite jobItem, Sprite jobDestination)
	{
		clone = Instantiate(jobDisplay, positions[0].transform.position, Quaternion.identity) as GameObject;
		clones.Add(clone);
		clone.transform.parent = this.gameObject.transform;

		item = clone.GetComponent<GrabChildren>().jobItem.GetComponent<SpriteRenderer>();
		destination = clone.GetComponent<GrabChildren>().jobDestination.GetComponent<SpriteRenderer>();

		GenerateImage(jobItem, jobDestination);
	}

	public void RemoveJobDisplay(int position)
	{
		Destroy(clones[position]);
		clones.RemoveAt(position);
	}

}
