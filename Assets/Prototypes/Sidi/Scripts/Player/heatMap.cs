using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatMap : MonoBehaviour {
	public GameObject heat;
	public GameObject deathHeat;

	float interval; 
	Transform transform;
	float timer;
	PlayerHealth playerHealth;
	bool dead = false;

	// Use this for initialization
	void Start () {
		interval = 15*Time.deltaTime;
		transform = GetComponent<Transform> ();
		playerHealth = GetComponent<PlayerHealth> ();
		timer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > interval) {
			addHeat ();
		}
	}

	void addHeat(){
		timer = 0.0f;
		if (playerHealth.getCurrentHealth () > 0) {
			Instantiate (heat, transform.position, Quaternion.identity);
			dead = true;
		} else {
			Instantiate (deathHeat, transform.position, Quaternion.identity);
			this.enabled = false;
		}
	}
}
