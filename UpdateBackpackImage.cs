using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBackpackImage : MonoBehaviour {

	public Sprite backPackImage;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	public void UpdateImage (Sprite newImage) {
		GetComponent<Image>().sprite = newImage;
	}
}
