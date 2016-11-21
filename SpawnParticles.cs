using UnityEngine;
using System.Collections;

public class SpawnParticles : MonoBehaviour {

	public GameObject particle;
	private GameObject clone;

	public GameObject spawnPoint;

	public bool play;
	public float playtime;
	// Use this for initialization
	void Start () {
		play = false;

	}

	// Update is called once per frame
	void Update () {

		if(play)
		{
			spawnParticle();
		}
	}

	void spawnParticle()
	{
		play = false;

		clone = Instantiate(particle, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		//clone.GetComponent<ParticleSystem>().enableEmission = true;
		transform.localScale = this.transform.parent.transform.localScale;

		Destroy(clone,playtime);
	}
}
