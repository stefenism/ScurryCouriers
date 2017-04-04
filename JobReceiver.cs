﻿using System.Collections;
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


			if(jobActivated)
			{
				ActivateJobs();
			}

	}


	public void CheckReceipt(string item)
	{

		//Debug.Log("ACTIVATEDDDDDDDd");
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
						ReceiveItems(i);
						manager.itemInQuestion = player.backPack[j].gameObject;
						player.ClearBackPack(j);
						j = player.backPack.Count;
					}
				}

				if(activeItems[i].gameObject.tag == item && activeItems.Count != 0)
				{
					ReceiveItems(i);
				}
			}

			if(activeItems.Count != 0)
			{
				if(activeItems[i].gameObject.tag == item)
				{
						ReceiveItems(i);
				}
			}

			if(manager.received)
			{
				i = activeItems.Count;
			}
		}
	}

	void ReceiveItems(int position)
	{
		manager.received = true;
		manager.itemreceived = activeItemsIndex[position];
		manager.itemInQuestion = player.carryObject;//items[i].gameObject;
		if(activeItemsIndex.Count > 0)
		{
			manager.activeItemRemoveIndex = activeItemsIndex[position] - 1;
		}
		else
		{
			manager.activeItemRemoveIndex = activeItemsIndex[position];
		}

		RemoveItems(position);
	}

	void RemoveItems(int indexLocale)
	{
		//Destroy(activeItems[indexLocale].gameObject);
		activeItems.RemoveAt(indexLocale);
		activeItemsIndex.RemoveAt(indexLocale);

		//Destroy(items[indexLocale].gameObject);
		items.RemoveAt(indexLocale);
	}

	public void ActivateJobs()
	{
		jobActivated = false;

		if(activeItems.Count < items.Count)
		{
			activeItems.Add(items[activeItems.Count]);
		}
	}



	void OnTriggerEnter2D(Collider2D collision)
	{
		CheckReceipt(collision.gameObject.tag);
	}
}
