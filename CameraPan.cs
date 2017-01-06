using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	public bool enabled;

	public GameObject focalPoint;
	public GameObject inside;
	public GameObject outside;
	public GameObject outsideDisabler;
	public GameObject insideDisabler;
	public PlayerController player;
	public GameObject playerFocalPoint;

	private CameraFollow camera;

	public Vector2 maxBounds;
	public Vector2 minBounds;
	public Vector2 outerBounds;
	// Use this for initialization
	void Start () {

		camera = Camera.main.GetComponent<CameraFollow>();
		enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if(enabled)
		{
			Debug.Log("Camera Focus: " + camera.player);
			enabled = false;
			PanCamera();

		}
		//Debug.Log("enabled: " + enabled);
	}

	void PanCamera()
	{
		//remove player gravity
		//change camera focal point
		//change camera bounds
		//start lerp on player and camera?

		if(camera.player == playerFocalPoint.transform)
		{
			camera.maxXAndY = new Vector2(outerBounds.y, maxBounds.y);
			camera.minXAndY = new Vector2(outerBounds.x, minBounds.y);
			camera.player = focalPoint.transform;
		}

		else if(camera.player == focalPoint.transform)
		{
			camera.maxXAndY = new Vector2(maxBounds.x, maxBounds.y);
			camera.minXAndY = new Vector2(minBounds.x, minBounds.y);
			camera.player = playerFocalPoint.transform;
		}


		Debug.Log("is camera on playerFocalPoint: " + (camera.player == playerFocalPoint.transform));
		Debug.Log("is camera on FocalPoint: " + (camera.player == focalPoint.transform));


	}
}
