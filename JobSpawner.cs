using UnityEngine;
using System.Collections;

public class JobSpawner : MonoBehaviour {

	public GameObject[] items;
	public GameObject[] locations;
	public float[] payments;


	private GameObject deliveryItem;
	private GameObject deliveryLocation;
	private float deliveryPay;
	private float deliveryTimeFrame;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.E))
		{
			pickNewJob();
		}
	}

	void pickNewJob()
	{
		//pick random item between all items in items[]
		//pick random location between all items in locations[]
		//decide pay based on item
		//decide time frame based on item. (probably a pretty large time frame)
		//send that data to spawn new job
		int newItem = Random.Range(0,items.Length - 1);

		deliveryItem = items[newItem];
		deliveryLocation = locations[Random.Range(0, locations.Length - 1)];
		deliveryPay = payments[newItem];
		deliveryTimeFrame = 30f;

		spawnNewJob(deliveryItem, deliveryLocation, deliveryPay, deliveryTimeFrame);
	}

	void spawnNewJob(GameObject item, GameObject location, float pay, float timeframe)
	{
		//add job items ("delivery" items above) to a job receiver script list.
		//including item, location, pay, and timeframe
		location.GetComponent<JobReceiver>().AddToList(item,pay,timeframe);
	}

	void displayJob()
	{
		//display the job on the UI so the player knows what they have in the queue.
	}
}
