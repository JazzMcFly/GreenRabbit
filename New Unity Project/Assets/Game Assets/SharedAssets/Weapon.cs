using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Weapon : MonoBehaviour {
	
	public float fireRate = 0.05f;
	public GameObject projectile;
	
	private float fireRateTimer = 0.0f;
	
	private FVector2 direction = new FVector2(0.0f, 1.0f);
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fireRateTimer -= Time.deltaTime;
	}
	
	
	public void Fire() {
		//print ("attempting to fire");
		if(fireRateTimer <= 0.0f) {
			GameObject shot =(GameObject) Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
			shot.GetComponent<ProjectileBasic>().SetDirection(direction);
			fireRateTimer = fireRate;	
		}
	}
	
	public void SetDirection(FVector2 newDir) {
		direction = new FVector2(newDir.X, newDir.Y);	
	}
	

}
