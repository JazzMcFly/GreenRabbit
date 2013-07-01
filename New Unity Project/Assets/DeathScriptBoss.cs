using UnityEngine;
using System.Collections;

public class DeathScriptBoss : MonoBehaviour {
	
	public GameObject deathExplosion;
	
	
	// Use this for initialization
	void Start () {
		Health health = GetComponent<Health>();
		health.OnDeath += OnDeath;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnDeath() {
		Instantiate(deathExplosion, gameObject.transform.position, gameObject.transform.rotation);
	}
}
