using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JobReceiver : MonoBehaviour {

	public PlayerController player;
	public PlayerWallet wallet;
	public GenerateJobImage generateJobImage;

	[Header("Items currently desired to be delivered")]
	public List<GameObject> items;
	[Header("Items currently actively being delivered")]
	public List<GameObject> activeItems;
	public List<GameObject> clones;
	public List<Sprite> itemImages;
	public List<Sprite> destinationImages;
	public List<int> payments;
	public List<float> deliveryTime;


	private List<float> startTimes;
	private List<float> currentTimes;

	private bool received;
	private bool backpackReceived;
	private bool jobInProgress;
	private bool activated;
	[HideInInspector]
	public bool jobActivated;

	private int itemreceived = 100;

	public GameObject jobAlertPrefab;
	public GameObject noticePosition;
	private GameObject clone;
	private GameObject itemInQuestion;


	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
		activeItems = new List<GameObject>();
		clones = new List<GameObject>();
		payments = new List<int>();
		deliveryTime = new List<float>();
		itemImages = new List<Sprite>();
		destinationImages = new List<Sprite>();

		startTimes = new List<float>();
		currentTimes = new List<float>();

		received = false;
		backpackReceived = false;
		jobInProgress = false;
		activated = false;
		jobActivated = false;
	}

	// Update is called once per frame
	void Update () {
		if(jobInProgress)
		{
			JobStart();
		}

		if(received)
		{
			Debug.Log("received");
			Destroy(itemInQuestion);
			received = false;

			jobReceived(itemreceived);
		}

		if(jobActivated)
		{
			ActivateJobs();
		}
	}

	public void AddToList(GameObject item, int payment, float time, Sprite jobImage, Sprite destinationImage)
	{
		items.Add(item);
		payments.Add(payment);
		deliveryTime.Add(time);
		//itemImages.Add(jobImage);
		//destinationImages.Add(destinationImage);
		generateJobImage.GenerateImageObject(jobImage, destinationImage);

		StartTimer();
	}

	void StartTimer()
	{
		jobInProgress = true;
		activated = true;
	}

	void JobStart()
	{
		if(activated)
		{
			activated = false;
			startTimes.Add(Time.time);
			currentTimes.Add(Time.time);

			clone = Instantiate(jobAlertPrefab, noticePosition.transform.position, Quaternion.identity) as GameObject;
			clone.GetComponent<OffScreenNotifier>().originalPosition = noticePosition;

			clones.Add(clone);
			//clone.GetComponent<ParticleSystem>().enableEmission = true;
			//transform.localScale = this.transform.parent.transform.localScale;
		}

		for(int i = 0; i < startTimes.Count; i++)
		{
			currentTimes[i] = Time.time - startTimes[i];

			if(currentTimes[i] >= deliveryTime[i])
			{
				items.RemoveAt(i);
				activeItems.RemoveAt(i);
				activeItems.RemoveAt(i);
				payments.RemoveAt(i);
				deliveryTime.RemoveAt(i);
				currentTimes.RemoveAt(i);
				startTimes.RemoveAt(i);
				if(clones.Count > 0)
				{
					//Destroy(clones[i].transform.parent);
					//Destroy(clones[i].gameObject);
					//Destroy(clones[i]);
					//clones.RemoveAt(i);
					DestroyNotice(i);
				}

				generateJobImage.RemoveJobDisplay(i);
			}
		}
	}

	void ActivateJobs()
	{
		jobActivated = false;

		if(activeItems.Count < items.Count)
		{
			activeItems.Add(items[activeItems.Count]);
		}

	}

/*
	I don't think this code works yet.  But the Job Spawning and receiving works
	other than that
*/
	void CheckReceipt(string item)
	{
		for(int i = 0; i < activeItems.Count; i++)
		{

			if(player.backPack.Count != 0 && activeItems.Count != 0)
			{

				print("index: " + i);
				print("items.count: " + items.Count);
				Debug.Log("backpack.count: " + player.backPack.Count);
				Debug.Log("current item check: " + items[i].gameObject);

				for(int j = 0; j < player.backPack.Count; j++)
				{
					Debug.Log("backpack item: " + player.backPack[j].gameObject);
					if(activeItems[i].gameObject.tag == player.backPack[j].gameObject.tag)
					{
						received = true;
						backpackReceived = true;
						itemreceived = i;
						itemInQuestion = player.backPack[j].gameObject;
						player.ClearBackPack(j);
						j = player.backPack.Count;
					}
				}

				if(activeItems[i].gameObject.tag == item)
				{
					received = true;
					itemreceived = i;
					itemInQuestion = player.carryObject;//items[i].gameObject;
				}
			}

			if(activeItems.Count != 0)
			{
				if(activeItems[i].gameObject.tag == item)
				{
						received = true;
						itemreceived = i;
						itemInQuestion = player.carryObject;//items[i].gameObject;
				}
			}

			if(received)
			{
				i = items.Count;
			}



			else if(i + 1 >= items.Count)
			{
				Debug.Log("failed");
			}
		}
	}

	void jobReceived(int position)
	{
		wallet.AddMoney(payments[position]);
		Debug.Log(wallet.money + " dollars");

		items.RemoveAt(position);
		activeItems.RemoveAt(position);
		payments.RemoveAt(position);
		deliveryTime.RemoveAt(position);
		currentTimes.RemoveAt(position);
		startTimes.RemoveAt(position);

		generateJobImage.RemoveJobDisplay(position);

		itemreceived = 100;
		if(!backpackReceived)
		{
			player.PickupPutdown();
			Destroy(itemInQuestion.transform.parent.gameObject);
		}
		backpackReceived = false;

		//Destroy(clones[position].transform.parent);
		//clones.RemoveAt(position);

	}

	public void DestroyNotice(int position)
	{
		Destroy(clones[position]);//.transform.parent);
		clones.RemoveAt(position);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//itemInQuestion = collision.gameObject;
		//Debug.Log("iteminquestion: " + itemInQuestion.gameObject);
		CheckReceipt(collision.gameObject.tag);
	}
}
