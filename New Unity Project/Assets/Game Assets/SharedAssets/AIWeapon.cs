using UnityEngine;
using System.Collections;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class AIWeapon : Weapon {
	
	public bool autofire = false;
	
	// Use this for initialization
	void Start () {
		this.direction = new FVector2(0.0f, -1.0f);
	}
	
	
	
	// Update is called once per frame
	void Update () {
		TickWeaponCoolDown();	
		if(autofire) {
			Fire();
		}
	}
}
