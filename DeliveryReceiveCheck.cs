using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryReceiveCheck : MonoBehaviour {

	public GameObject bossy;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//itemInQuestion = collision.gameObject;
		//Debug.Log("iteminquestion: " + itemInQuestion.gameObject);
		bossy.GetComponent<JobReceiverold>().CheckReceipt(collision.gameObject.tag);
	}
}
