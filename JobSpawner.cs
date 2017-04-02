using UnityEngine;
using System.Collections;

public class JobSpawner : MonoBehaviour {

	public GameObject[] items;
	public GameObject[] locations;
	public Sprite[] itemImages;
	public Sprite[] destinationImages;
	public int[] payments;


	private GameObject deliveryItem;
	private GameObject deliveryLocation;
	private int deliveryPay;
	private float deliveryTimeFrame;
	public float deliveryTimeAllotted;
	private Sprite itemImage;
	private Sprite destinationImage;

	public float maxTimeBetweenDeliveries;
	public float minTimeBetweenDeliveries;

	private bool activated;
	private float currentTime;
	private float startTime;
	private float nextDelivery;


	// Use this for initialization
	void Start () {

		activated = false;

	}

	// Update is called once per frame
	void Update () {


		NextDeliveryCountDown();

	}

	void NextDeliveryCountDown()
	{
		if(activated)
		{
			activated = false;
			startTime = Time.time;
			currentTime = Time.time;

			nextDelivery = Random.Range(minTimeBetweenDeliveries, maxTimeBetweenDeliveries) + startTime;

			pickNewJob();
		}

		if(currentTime > nextDelivery)
		{
			activated = true;
		}
		currentTime += Time.deltaTime;

		//Debug.Log("currentTime: " + currentTime);
		//Debug.Log("nextDelivery: " + nextDelivery);
	}

	void pickNewJob()
	{
		//pick random item between all items in items[]
		//pick random location between all items in locations[]
		//decide pay based on item
		//decide time frame based on item. (probably a pretty large time frame)
		//send that data to spawn new job
		int newItem = Random.Range(0,items.Length);
		int newLocation = Random.Range(0, locations.Length);

		deliveryItem = items[newItem];
		deliveryLocation = locations[newLocation];
		deliveryPay = payments[newItem];
		deliveryTimeFrame = deliveryTimeAllotted;
		itemImage = itemImages[newItem];
		destinationImage = destinationImages[newLocation];

		spawnNewJob(deliveryItem, deliveryLocation, deliveryPay, deliveryTimeFrame, itemImage, destinationImage);
	}

	void spawnNewJob(GameObject item, GameObject location, int pay, float timeframe, Sprite itemImage, Sprite destinationImage)
	{
		//add job items ("delivery" items above) to a job receiver script list.
		//including item, location, pay, and timeframe
		GetComponent<JobManager>().AddToList(item, location, pay,timeframe, itemImage, destinationImage);
		//location.GetComponent<JobReceiverold>().AddToList(item,pay,timeframe, itemImage, destinationImage);
	}

	void displayJob()
	{
		//display the job on the UI so the player knows what they have in the queue.
	}
}
