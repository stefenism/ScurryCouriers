using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	public bool enabled;
	public GameObject rightBorderTrigger;
	public GameObject leftBorderTrigger;

	[HideInInspector]
	public bool leftSide;
	[HideInInspector]
	public bool rightSide;
	[HideInInspector]
	public bool middle = true;

	private bool cameraMoving = false;

	//public GameObject focalPoint;
	//public GameObject inside;
	//public GameObject outside;
	//public GameObject outsideDisabler;
	//public GameObject insideDisabler;
	public PlayerController player;
	//public GameObject playerFocalPoint;

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

			if(camera.minXAndY.x == minBounds.x)
			{
				middle = true;
			}
			else
			{
				middle = false;
			}
			enabled = false;
			PanCamera();

		}
		//Debug.Log("enabled: " + enabled);
	}

	void PanCamera()
	{
		StartCoroutine(lerpBounds(1f));
	}

	private IEnumerator lerpBounds(float timeToFinish)
	{
		float runTime = 0;
		float timer = 0;
		float tempDeltaTime;

		cameraMoving = true;

		while(timer<timeToFinish)
		{
			if(rightSide)// && (camera.minXAndY.x == -3.28f))
			{
				leftBorderTrigger.SetActive(false);
				rightBorderTrigger.SetActive(false);
				if(middle)//camera.minXAndY.x >= -3.28f && middle)
				{
					camera.minXAndY = Vector2.Lerp(camera.minXAndY, new Vector2 (outerBounds.y -1, minBounds.y), runTime);
					camera.maxXAndY = Vector2.Lerp(camera.maxXAndY, new Vector2(outerBounds.y, maxBounds.y), runTime);
				}
				else
				{
					camera.minXAndY = Vector2.Lerp(camera.minXAndY, new Vector2(minBounds.x, minBounds.y), runTime);
					camera.maxXAndY = Vector2.Lerp(camera.maxXAndY, new Vector2(maxBounds.x, maxBounds.y), runTime);
				}
			}
			if(leftSide)
			{
				leftBorderTrigger.SetActive(false);
				rightBorderTrigger.SetActive(false);
				if(middle)//camera.minXAndY.x == -3.28f)
				{
					camera.minXAndY = Vector2.Lerp(camera.minXAndY, new Vector2 (outerBounds.x, minBounds.y), runTime);
					camera.maxXAndY = Vector2.Lerp(camera.maxXAndY, new Vector2(outerBounds.x + 1, maxBounds.y), runTime);
				}
				else
				{
					camera.minXAndY = Vector2.Lerp(camera.minXAndY, new Vector2(minBounds.x, minBounds.y), runTime);
					camera.maxXAndY = Vector2.Lerp(camera.maxXAndY, new Vector2(maxBounds.x, maxBounds.y), runTime);
				}
			}

			tempDeltaTime = Time.deltaTime;
			runTime += tempDeltaTime / timeToFinish;
			timer += tempDeltaTime;
			yield return null;
		}

		leftSide = false;
		rightSide = false;
		leftBorderTrigger.SetActive(true);
		rightBorderTrigger.SetActive(true);
		cameraMoving = false;

	}

}
