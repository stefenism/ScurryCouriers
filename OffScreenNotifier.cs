using UnityEngine;
using System.Collections;

public class OffScreenNotifier : MonoBehaviour {

	private Vector3 screenPos;
	private Vector2 OnScreenPos;
	private float max;
	private Camera camera;

	public GameObject originalPosition;
	private Vector2 displayPosition;

	public GameObject shakeObject;


	// Use this for initialization
	void Start () {
		camera = Camera.main;

		//originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {

		screenPos = camera.WorldToViewportPoint(originalPosition.transform.position); //get viewport position

		if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1)
		{
			Debug.Log("already on screen");
			transform.position = originalPosition.transform.position;
			shakeObject.GetComponent<ShakeObject>().originalPosition = transform.position;
		}

		else
		{
			CalculatePosition();
		}

	}

	void CalculatePosition()
	{


		/*
		if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 0)
		{
			Debug.Log("already on screen");
			transform.position = originalPosition;
			return;
		}
		*/

		OnScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;

		//get largest offset
		max = Mathf.Max(Mathf.Abs(OnScreenPos.x), Mathf.Abs(OnScreenPos.y));

		OnScreenPos = (OnScreenPos/(max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

		OnScreenPos = new Vector2(Mathf.Clamp(OnScreenPos.x, 0.01f, 0.99f),
															Mathf.Clamp(OnScreenPos.y, 0.05f, 0.95f));
		//displayPosition = camera.ScreenToWorldPoint(OnScreenPos);
		transform.position = camera.ViewportToWorldPoint(OnScreenPos);
		shakeObject.GetComponent<ShakeObject>().originalPosition = transform.position;
		Debug.Log(OnScreenPos);
	}
}
