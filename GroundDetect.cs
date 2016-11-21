using UnityEngine;
using System.Collections;

public class GroundDetect : MonoBehaviour {
	private PlayerController player;
	public float groundDistance;
	public LayerMask groundLayer;
	//public LayerMask Source;
	// Use this for initialization
	void Start () {

		player = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void LateUpdate () {

		//print(player.Grounded + " grounded");


			GroundDetection();

	}

	void GroundDetection()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundDistance, groundLayer);

		Ray2D landingRay = new Ray2D(transform.position, -Vector2.up);

		Debug.DrawRay(transform.position, -Vector2.up * groundDistance);

		//if(!player.Grounded)
		//{ //first argument WAS landingRay
		//print(hit.collider + " collided tag");

		if(hit.collider != null)
		{

			if(hit.collider.gameObject.tag == "Ground" && (player.rb.velocity.y > 0))
			{
				player.grounded = false;
			}

			else
			{
				player.grounded = true;
			}


			/*
			if(hit.collider.gameObject.tag != "Ground")
			{
				Debug.Log("collided with: " + hit.collider.name);
			}
			*/

			//player.airJump = 2;
			//player.anim.SetBool("jumping", false);
		}
		else
		{
			player.grounded = false;
		}

			//player.anim.SetBool("jumping", true);
		//}
	}
}
