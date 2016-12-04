using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JobReceiver : MonoBehaviour {

	public PlayerController player;
	public PlayerWallet wallet;

	public List<GameObject> items;
	public List<GameObject> clones;
	public List<int> payments;
	public List<float> deliveryTime;


	private List<float> startTimes;
	private List<float> currentTimes;

	private bool received;
	private bool jobInProgress;
	private bool activated;

	private int itemreceived = 100;

	public GameObject jobAlertPrefab;
	public GameObject noticePosition;
	private GameObject clone;


	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
		clones = new List<GameObject>();
		payments = new List<int>();
		deliveryTime = new List<float>();

		startTimes = new List<float>();
		currentTimes = new List<float>();

		received = false;
		jobInProgress = false;
		activated = false;
	}

	// Update is called once per frame
	void Update () {
		if(jobInProgress)
		{
			JobStart();
		}
	}

	public void AddToList(GameObject item, int payment, float time)
	{
		items.Add(item);
		payments.Add(payment);
		deliveryTime.Add(time);

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
				payments.RemoveAt(i);
				deliveryTime.RemoveAt(i);
				currentTimes.RemoveAt(i);
				startTimes.RemoveAt(i);
				Destroy(clones[i].transform.parent);
				//Destroy(clones[i]);
				clones.RemoveAt(i);
			}
		}
	}

/*
	I don't think this code works yet.  But the Job Spawning and receiving works
	other than that
*/
	void CheckReceipt(string item)
	{
		for(int i = 0; i < items.Count -1; i++)
		{
			print(i);
			print("items.count: " + items.Count);
			if(received)
			{
				i = items.Count;
			}
			if(items[i].gameObject.tag == item)
			{
				received = true;
				itemreceived = i;
			}
			else
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
		payments.RemoveAt(position);
		deliveryTime.RemoveAt(position);
		currentTimes.RemoveAt(position);
		startTimes.RemoveAt(position);
		Destroy(clones[position].transform.parent);
		clones.RemoveAt(position);

		itemreceived = 100;
		player.PickupPutdown();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		CheckReceipt(collision.gameObject.tag);

		if(received)
		{
			Debug.Log("received");
			Destroy(collision.gameObject);
			received = false;

			jobReceived(itemreceived);
		}
	}
}
