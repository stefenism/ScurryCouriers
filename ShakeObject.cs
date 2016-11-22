using UnityEngine;
using System.Collections;

public class ShakeObject : MonoBehaviour {

	private Vector3 originalPosition;
	public GameObject shakeObject;

	public float shakeAmt;
	public float shakeInterval;
	public float shakeLength;

	public bool shake;
	private bool newShake;

	private float shakeTimer;
	// Use this for initialization
	void Start () {
		newShake = true;
		shake = false;

		originalPosition = shakeObject.transform.position;
	}

	// Update is called once per frame
	void Update () {

		if(shake)
		{
			InvokeRepeating("Shake", 0, .01f);
			Invoke("StopShake", shakeLength);
		}

		else
		{
			ShakeWait();
		}
	}

	void Shake()
	{

			shakeObject.transform.position = originalPosition;
			float xOffset = (shakeAmt * Random.value * 2 - shakeAmt) * .0025f;
			float yOffset = (shakeAmt * Random.value * 2 - shakeAmt) * .0025f;

			Vector3 pp = shakeObject.transform.position;
			pp = new Vector3(pp.x + xOffset,pp.y + yOffset,0);
			shakeObject.transform.position = pp;
	}

	void StopShake()
	{
		CancelInvoke("Shake");
		shakeObject.transform.position = originalPosition;
		shake = false;
		newShake = true;

	}

	void ShakeWait()
	{
		if(newShake)
		{
			newShake = false;
			shakeTimer = shakeInterval;
		}
		if(shakeTimer <= 0)
		{
			shake = true;
		}
		shakeTimer -= Time.deltaTime;
	}
}
