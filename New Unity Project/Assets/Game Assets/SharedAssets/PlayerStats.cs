using UnityEngine;
using System.Collections;


//Right now just tracks the weapon power of the player.

public class PlayerStats : MonoBehaviour {
	
	
	public int LifeCount;
	
	public GameObject [] primaryProjectileList;
	public int [] powerThresholds;
	
	private Weapon[] weaponList;
	private int powerLevel = 0;	
	private int powerIndex = 0; //index into primaryProjectileList and powerThresholds
	private int maxPower;
	
	public int maxSecondaryAmmo;
	private int secondaryAmmoCount;
	
	public int maxTertiaryAmmo;
	private int tertiaryAmmoCount;
	
	private Health health;

	
	// Use this for initialization
	void Start () {
		if(primaryProjectileList.Length < 1 || primaryProjectileList.Length != powerThresholds.Length){
			print ("Player projectiles aren't properly setup in inspector!!");	
		}
		weaponList = gameObject.GetComponentsInChildren<Weapon>();
		maxPower = powerThresholds[powerThresholds.Length-1];
		weaponList[0].SetProjectile(primaryProjectileList[0]); 
		weaponList[1].OnFire += OnFireSecondary;
		weaponList[2].OnFire += OnFireTertiaryWeapon;
		
		secondaryAmmoCount = maxSecondaryAmmo;
		tertiaryAmmoCount = maxTertiaryAmmo;
		
		health = GetComponent<Health>();
		health.OnDeath += SubtractLifeCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void IncreasePower(int amount) {
		if( amount <= 0 || powerIndex >= powerThresholds.Length - 1) {
			//do nothing
		} else {
			powerLevel += amount;
			while(powerIndex < powerThresholds.Length - 1 && powerLevel >= powerThresholds[powerIndex+1]) {
				powerIndex++;
			}
			weaponList[0].SetProjectile(primaryProjectileList[powerIndex]);
			
		}
	}
	
	public void DecreasePower(int amount) {
		if(amount >= 0) {
			//do nothing	
		} else {
			powerLevel -= amount;
			if(powerLevel < 0) {
				powerLevel = 0;	
			}
			while(powerLevel < powerThresholds[powerIndex]){
				powerIndex--;
				weaponList[0].SetProjectile(primaryProjectileList[powerIndex]);
				print ("changed Weapon");
			}
		}
	}
	
	public int GetAmmoCount(WeaponPlayer.WeaponInput type) {
		switch (type) {
		case WeaponPlayer.WeaponInput.Primary:
			return -1;
		case WeaponPlayer.WeaponInput.Secondary:
			return GetSecondaryAmmoCount();
		case WeaponPlayer.WeaponInput.Tertiary:
			return GetTertiaryAmmoCount();
		default:
			return -1;
		}
	}
	
	public int GetMaxAmmoCount(WeaponPlayer.WeaponInput type) {
		switch (type) {
		case WeaponPlayer.WeaponInput.Primary:
			return -1;
		case WeaponPlayer.WeaponInput.Secondary:
			return GetMaxSecondaryAmmoCount();
		case WeaponPlayer.WeaponInput.Tertiary:
			return GetMaxTertiaryAmmoCount();
		default: 
			return -1;
		}
	}
	
	public void AddAmmoCount(int amount, WeaponPlayer.WeaponInput type) {
		switch(type) {
		case WeaponPlayer.WeaponInput.Primary:
			break;
		case WeaponPlayer.WeaponInput.Secondary:
			AddSecondaryAmmoCount(amount);
			break;
		case WeaponPlayer.WeaponInput.Tertiary:
			AddTertiaryAmmoCount(amount);
			break;
		default:
			break;
		}
	}
	
	public int GetSecondaryAmmoCount() {
		return secondaryAmmoCount;	
	}
	
	public void AddSecondaryAmmoCount(int amount) {
		secondaryAmmoCount += amount;
		if(secondaryAmmoCount > maxSecondaryAmmo) {
			secondaryAmmoCount = maxSecondaryAmmo;	
		}
	}
	
	public void SetSecondaryAmmoCount(int amount) {
		if(amount > maxSecondaryAmmo) {
			secondaryAmmoCount = maxSecondaryAmmo;	
		} else {
			secondaryAmmoCount = amount;	
		}
	}
	
	public void SubtractSecondaryAmmoCount() {
		if(secondaryAmmoCount > 0) {
			secondaryAmmoCount--;	
		}
	}
	
	protected void OnFireSecondary() {
		SubtractSecondaryAmmoCount();
	}
	
	public int GetMaxSecondaryAmmoCount() {
		return maxSecondaryAmmo;	
	}
	
	
	
	
	
	public int GetTertiaryAmmoCount() {
		return tertiaryAmmoCount;
	}
	
	public void AddTertiaryAmmoCount(int amount) {
		tertiaryAmmoCount += amount; 
		if(tertiaryAmmoCount > maxTertiaryAmmo) {
			tertiaryAmmoCount = maxTertiaryAmmo;
		}
	}	
	
	public void SetTertiaryAmmoCount(int amount) {
		if(amount > maxTertiaryAmmo) {
			tertiaryAmmoCount = maxTertiaryAmmo;
		} else {
			tertiaryAmmoCount = amount;	
		}
	}
	
	public void SubtractTertiaryAmmoCount() {
		if(tertiaryAmmoCount > 0) {
			tertiaryAmmoCount--;	
		}
	}
	
	public int GetMaxTertiaryAmmoCount() {
		return maxTertiaryAmmo;	
	}
	
	public void OnFireTertiaryWeapon() {
		SubtractTertiaryAmmoCount();
		health.HealToMax();
		health.SetInvulnrable(5.0f);
	}
	
	
	
	public void SubtractLifeCount() {
		LifeCount--;	
	}
	
	public void AddLifeCount(int amount) {
		LifeCount += amount;	
	}
	
}
