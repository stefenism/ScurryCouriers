using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour {


	public float spawnTime;
	public GameObject item;
	public GameObject spawnPoint;
	[HideInInspector]
	public bool activated;

	public bool spawning;
	private float currentTime;
	private float startTime;

	private GameObject clone;


	//private Animator anim;
	// Use this for initialization
	void Start () {

		//anim = GetComponent<Animator>();

		activated = false;

	}

	// Update is called once per frame
	void Update () {

		if(activated)
		{
			startSpawn();
		}

		if(spawning)
		{
			spawnObject();
		}
	}

	void startSpawn()
	{
		spawning = true;
	}

	void spawnObject()
	{
		if(activated)
		{
			activated = false;
			startTime = Time.time;
		}

		currentTime = Time.time - startTime;
		if(currentTime >= spawnTime)
		{
			spawning = false;
			clone = Instantiate(item, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		}
	}
}
