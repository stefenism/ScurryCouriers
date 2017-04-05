using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour {




	public PlayerController player;
	public PlayerWallet wallet;
	public GenerateJobImage generateJobImage;

	public JobReceiver[] receivers;

	[Header("Items currently desired to be delivered")]
	public List<GameObject> items;
	[Header("Items currently actively being delivered")]
	public List<GameObject> activeItems;
  public List<GameObject> locations;

	public List<int> payments;
	public List<float> deliveryTime;


	private List<float> startTimes;
	private List<float> currentTimes;

  [HideInInspector]
	public bool received;
  [HideInInspector]
	public bool backpackReceived;
	private bool jobInProgress;
	private bool activated;
	[HideInInspector]
	public bool jobActivated;
  public GameObject notifier;

  [HideInInspector]
	public int itemreceived = 100;
  [HideInInspector]
	public GameObject itemInQuestion;
  //[HideInInspector]
  public int activeItemRemoveIndex = 100;

	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
		activeItems = new List<GameObject>();
    locations = new List<GameObject>();
		payments = new List<int>();
		deliveryTime = new List<float>();
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

    CheckNotifier();
	}

  void CheckNotifier()
  {
    if(items.Count > activeItems.Count)
    {
      notifier.SetActive(true);
    }
    else
    {
      notifier.SetActive(false);
    }
  }

	public void AddToList(GameObject item, GameObject location, int payment, float time, Sprite jobImage, Sprite destinationImage)
	{
		items.Add(item);
		payments.Add(payment);
		deliveryTime.Add(time);
    locations.Add(location);
		//itemImages.Add(jobImage);
		//destinationImages.Add(destinationImage);
		generateJobImage.GenerateImageObject(jobImage, destinationImage);
    AddToLocation(item, location, time);
		StartTimer();
	}

  void AddToLocation(GameObject item, GameObject location, float time)
  {
    JobReceiver receiver = location.gameObject.GetComponent<JobReceiver>();

    receiver.items.Add(item);
    receiver.activeItemsIndex.Add(items.Count);
		receiver.deliveryTimes.Add(time);
		receiver.startTimes.Add(Time.time);
		receiver.currentTimes.Add(Time.time);

  }

	void StartTimer()
	{
		jobInProgress = true;
		activated = true;
	}


	//////////////////////////////////////////////////////////////////////////
	//Need to add a method of spawning all these jobs FOR a specific receiver
	//Rather than just adding it to this script.
	//////////////////////////////////////////////////////////////////////////

	void JobStart()
	{
		if(activated)
		{
			activated = false;
			startTimes.Add(Time.time);
			currentTimes.Add(Time.time);

			//need to add a condition that activates the jobalert rather than spawn one

			//clone = Instantiate(jobAlertPrefab, noticePosition.transform.position, Quaternion.identity) as GameObject;
			//clone.GetComponent<OffScreenNotifier>().originalPosition = noticePosition;

			//clones.Add(clone);
			//clone.GetComponent<ParticleSystem>().enableEmission = true;
			//transform.localScale = this.transform.parent.transform.localScale;
		}

		for(int i = 0; i < items.Count; i++)
		{
			currentTimes[i] = Time.time - startTimes[i];

			if(currentTimes[i] >= deliveryTime[i])
			{
				items.RemoveAt(i);
				activeItems.RemoveAt(i);
				payments.RemoveAt(i);
				deliveryTime.RemoveAt(i);
				currentTimes.RemoveAt(i);
				startTimes.RemoveAt(i);



				generateJobImage.RemoveJobDisplay(i);
			}
		}
	}

	void ActivateJobs()
	{
		jobActivated = false;

		if(activeItems.Count < 4 && activeItems.Count < items.Count)
		{
			activeItems.Add(items[activeItems.Count]);
      locations[activeItems.Count -1].gameObject.GetComponent<JobReceiver>().jobActivated = true;
		}

	}

	void AdjustIndexes(int position)
	{
		for(int i = 0; i < receivers.Length; i++)
		{
			for(int n = position; n < receivers[i].activeItemsIndex.Count; n++)
			{
				receivers[i].activeItemsIndex[n]--;
			}
		}
	}

	void jobReceived(int position)
	{
		wallet.AddMoney(payments[position]);
		Debug.Log(wallet.money + " dollars");

		items.RemoveAt(position);
		activeItems.RemoveAt(activeItemRemoveIndex);
		payments.RemoveAt(position);
		deliveryTime.RemoveAt(position);
		currentTimes.RemoveAt(position);
		startTimes.RemoveAt(position);

		AdjustIndexes(activeItemRemoveIndex);
		generateJobImage.RemoveJobDisplay(activeItemRemoveIndex);

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


}
