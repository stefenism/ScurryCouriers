using UnityEngine;
using System.Collections;

public class AiAnimate : MonoBehaviour {

	private Animator anim;

	public int IdleTime;
	private float timer = 3f;
	private float waitTimer = 10000f;
	private bool waiting;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		waiting = false;
	}

	// Update is called once per frame
	void Update () {



			StartCounter();

	}

	void StartCounter()
	{


		timer -= Time.deltaTime;


		if(timer <= 0f && !waiting)
		{
			StartCoroutine(Idle());
		}

	}

	private IEnumerator Idle()
	{
		anim.SetBool("Idle", true);
		waiting = true;
		timer = waitTimer;
		yield return new WaitForSeconds(IdleTime);
		anim.SetBool("Idle", false);
		timer = Random.Range(7f, 12f);
		waiting = false;
	}
}
