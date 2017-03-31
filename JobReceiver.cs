using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobReceiver : MonoBehaviour {

	public List<GameObject> items;
	public List<GameObject> activeItems;
	public List<int> activeItemsIndex;
	public PlayerController player;
	public JobManager manager;

	public bool jobActivated;

	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
		activeItems = new List<GameObject>();
		activeItemsIndex = new List<int>();

		jobActivated = false;

	}

	// Update is called once per frame
	void Update () {

		/*
			if(jobActivated)
			{
				ActivateJobs();
			}
		*/
	}

	/*
	public void CheckReceipt(string item)
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
						manager.received = true;
						manager.backpackReceived = true;
						manager.itemreceived = activeItemsIndex[i];
						itemInQuestion = player.backPack[j].gameObject;
						player.ClearBackPack(j);
						j = player.backPack.Count;
					}
				}

				if(activeItems[i].gameObject.tag == manager.item)
				{
					manager.received = true;
					manager.itemreceived = activeItemsIndex[i];
					manager.itemInQuestion = player.carryObject;//items[i].gameObject;
				}
			}

			if(activeItems.Count != 0)
			{
				if(activeItems[i].gameObject.tag == item)
				{
						manager.received = true;
						manager.itemreceived = activeItemsIndex[i];
						manager.itemInQuestion = player.carryObject;//items[i].gameObject;
				}
			}

			if(received)
			{
				i = activeItems.Count;
			}



			else if(i + 1 >= activeItems.Count)
			{
				Debug.Log("failed");
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

	*/

	void OnTriggerEnter2D(Collider2D collision)
	{
		//CheckReceipt(collision.gameObject.tag);
	}
}
