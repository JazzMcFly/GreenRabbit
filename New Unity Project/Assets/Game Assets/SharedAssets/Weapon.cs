using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public float fireRate = 0.05f;
	public float weaponAngle = 90.0f; // default upward
	public GameObject projectile;
	
	private float fireRateTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fireRateTimer -= Time.fixedDeltaTime;
	}
	
	
	public void Fire() {
		if(fireRateTimer >= 0.0f) {
			Instantiate(projectile);
			
			fireRateTimer = fireRate;	
		}
	}
}
