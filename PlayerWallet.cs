using UnityEngine;
using System.Collections;

public class PlayerWallet : MonoBehaviour {

	public int money;
	public UpdateMoney moneyUI;

	// Use this for initialization
	void Start () {

		moneyUI.SetMoney(money);
	}

	// Update is called once per frame
	void Update () {

	}

	public void AddMoney(int amt)
	{
		money += amt;

		moneyUI.SetMoney(money);

	}
}
