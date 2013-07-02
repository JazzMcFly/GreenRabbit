using UnityEngine;
using System.Collections;


//Right now just tracks the weapon power of the player.

public class PlayerStats : MonoBehaviour {
	
	

	
	public GameObject [] primaryProjectileList;
	public int [] powerThresholds;
	public int maxPower;
	
	private Weapon[] weaponList;
	private int powerLevel = 0;	
	private int powerIndex = 0; //index into primaryProjectileList and powerThresholds
	
	// Use this for initialization
	void Start () {
		if(primaryProjectileList.Length < 1 || primaryProjectileList.Length != powerThresholds.Length){
			print ("Player projectiles aren't properly setup in inspector!!");	
		}
		weaponList = gameObject.GetComponentsInChildren<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void IncreasePower(int amount) {
		if( amount <= 0 || powerIndex >= powerThresholds.Length - 1) {
			//do nothing
		} else {
			powerLevel += amount;
			if(powerLevel >= powerThresholds[powerIndex+1]) {
				powerIndex++;
				while(powerIndex < powerThresholds.Length && powerLevel >= powerThresholds[powerIndex+1]) {
					powerIndex++;	
				}
				weaponList[0].SetProjectile(primaryProjectileList[powerIndex]);
			}
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
			}
		}
	}
	
}
