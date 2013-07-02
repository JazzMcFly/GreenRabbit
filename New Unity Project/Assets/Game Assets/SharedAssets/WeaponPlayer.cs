using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

//PlayerWeapon is different from Weapon in that it cares for I/O


public class WeaponPlayer : Weapon {
	
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
	
	public WeaponType weaponType;
	public WeaponInput weaponInput;
	
	public float minChargeToFire = 20.0f;
	public float maxCharge = 100.0f;
	public float chargePerSec = 40.0f;
	
	private KeyCode weaponButton;
	
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
		case WeaponInput.Enemy:
		default:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		TickWeaponCoolDown();	
		
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
		//print (gameObject.transform.rotation.eulerAngles);
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
}
