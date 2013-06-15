using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Weapon : MonoBehaviour {
	
	public enum WeaponInput {
		Primary,
		Secondary,
		Tertiary,
		Enemy
	}
	
	public enum WeaponType {
		Auto,
		SingleShot,
		Charge
	}	
	
	public float fireRate = 0.05f;
	public WeaponType weaponType;
	public WeaponInput weaponInput;
	public GameObject projectile;
	
	public float minChargeToFire = 20.0f;
	public float maxCharge = 100.0f;
	public float chargePerSec = 40.0f;
	
	private float fireRateTimer = 0.0f;
	private KeyCode weaponButton;
	private FVector2 direction = new FVector2(0.0f, 1.0f);
	
	private float currentCharge = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		switch (weaponInput) {
			
		case WeaponInput.Primary:
			//print ("Set as primary");
			weaponButton = KeyCode.Mouse0;
			break;
		case WeaponInput.Secondary:
			weaponButton = KeyCode.Mouse1;
			break;
		case WeaponInput.Tertiary:
			weaponButton = KeyCode.Mouse2;
			break;
		default:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		fireRateTimer -= Time.deltaTime;
		
		switch(weaponType) {
		case WeaponType.Auto:
			if(Input.GetKey(weaponButton))
				Fire();
			break;
		case WeaponType.SingleShot:
			if(Input.GetKeyDown(weaponButton))
				Fire();
			break;
		case WeaponType.Charge: 
			if(Input.GetKey(weaponButton))
				ChargeIncrease();
			if(Input.GetKeyUp(weaponButton))
				ChargeRelease();
			break;
		default:
			print ("NO WEAPON TYPE!");
			break;
		}
	}
	
	public void Fire() {
		//print ("attempting to fire");
		if(fireRateTimer <= 0.0f) {
			fireRateTimer = fireRate;	
			GameObject shot =(GameObject) Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
			//shot.GetComponent<ProjectileBasic>().SetDirection(direction);
		}
	}
			
	public void ChargeIncrease() {
		currentCharge += chargePerSec*Time.deltaTime;
	}
	
	public void ChargeRelease() {
		if(currentCharge >= minChargeToFire) {
			Fire ();	
		}
		currentCharge = 0.0f; 
	}
	
	//Returns raw charge value
	public float GetCurrentCharge() {
		return currentCharge;				
	}
	
	public float GetCurrentChargePercentage() {
		return currentCharge/maxCharge;			
	}
	
	public float GetMaxCharge() {
		return maxCharge;	
	}
	
	public float GetMinCharge() {
		return minChargeToFire;	
	}
	
	public void SetDirection(FVector2 newDir) {
		newDir.Normalize();
		direction = new FVector2(newDir.X, newDir.Y);
		
		float zAngle = Mathf.Acos(newDir.X);
		if(newDir.Y < 0.0f) {
			zAngle = 2*Mathf.PI - zAngle;	
		}
		
		gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f,zAngle);
	}
	
}
