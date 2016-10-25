using UnityEngine;
using System.Collections;

public class ShopRat : MonoBehaviour {

	public PlayerController player;
	public GameObject rayOrigin;
	public float range;
	private float timer;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		timer = Random.Range(1f, 3f);
	}

	// Update is called once per frame
	void Update () {

		DetectPlayerRange();
	}

	void DetectPlayerRange()
	{
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, player.transform.position - rayOrigin.transform.position, range);

		Ray2D landingRay = new Ray2D(rayOrigin.transform.position, player.transform.position - rayOrigin.transform.position);

		Debug.DrawRay(rayOrigin.transform.position, player.transform.position * range);

		//if(!player.Grounded)
		// //first argument WAS landingRay
		//print(hit.collider + " collided tag");

		timer -= Time.deltaTime;

		if(hit.distance <= range && timer <= 0)
		{
			anim.SetBool("InRange", true);
			StartCoroutine(waiting(4f));
			//player.airJump = 2;
			//player.anim.SetBool("jumping", false);
		}
		else
		{
			anim.SetBool("InRange", false);
		}
	}

	private IEnumerator waiting(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		timer = seconds;
	}
}
