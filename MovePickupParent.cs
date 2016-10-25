using UnityEngine;
using System.Collections;

public class MovePickupParent : MonoBehaviour {

	private GameObject triggerChild;
	// Use this for initialization
	void Start () {
		triggerChild = this.gameObject.transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update () {
		transform.position = triggerChild.transform.position;
	}
}
