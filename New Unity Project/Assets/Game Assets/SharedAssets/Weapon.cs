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
		fireRateTimer -= Time.deltaTime;
	}
	
	
	public void Fire() {
		print ("attempting to fire");
		if(fireRateTimer <= 0.0f) {
			print ("FIRING!!");
			Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
			
			fireRateTimer = fireRate;	
		}
	}
	

}
