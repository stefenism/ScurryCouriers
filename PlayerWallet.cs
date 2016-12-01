using UnityEngine;
using System.Collections;

public class PlayerWallet : MonoBehaviour {

	public int money;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void AddMoney(int amt)
	{
		money += amt;
	}
}
