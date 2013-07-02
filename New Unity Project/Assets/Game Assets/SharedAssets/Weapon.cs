using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public delegate void OnKill();

public class Weapon : MonoBehaviour {
	

	public float fireRate = 0.05f;

	public GameObject projectile;
	
	protected float fireRateTimer = 0.0f;
	protected FVector2 direction = new FVector2(0.0f, 1.0f);
		
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		TickWeaponCoolDown();	

	}
	
	public void Fire() {
		//print ("attempting to fire");
		if(fireRateTimer <= 0.0f) {
			fireRateTimer = fireRate;	
			GameObject newObject =(GameObject) Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
			if(newObject.GetComponent<ProjectileBasic>() != null) {
				//probably need to pass weapon info down for OnHit callbacks
			} else if(newObject.GetComponent<Laser>() != null) {
				newObject.transform.parent = gameObject.transform;
				newObject.GetComponent<Laser>().SetAnchor(gameObject);
				//probably need to pass info about host object in order to kill the laser when the host dies.	
			}
		}
	}

	public void SetDirection(FVector2 newDir) {
		newDir.Normalize();
		direction = new FVector2(newDir.X, newDir.Y);
		
		float zAngle = Mathf.Acos(newDir.X)*180.0f/Mathf.PI;
		if(newDir.Y < 0.0f) {
			zAngle = 360.0f - zAngle;	
		}
		
		zAngle -= 90.0f; //Up is our 0 degrees here :P
		
		gameObject.transform.rotation = Quaternion.identity;
		gameObject.transform.Rotate( new Vector3(0.0f, 0.0f,zAngle));
	}
	
	public void SetProjectile(GameObject proj) {
		projectile = proj;
	}
	
	protected void TickWeaponCoolDown() {
		fireRateTimer -= Time.deltaTime;
	}
}
