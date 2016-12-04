using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour {

	public Text Money;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetMoney(int amount)
	{
		Money.text = "Money: $" + amount.ToString();
	}

}
