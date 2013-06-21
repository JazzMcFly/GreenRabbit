using UnityEngine;
using System.Collections;

public class AIWeapon : Weapon {
	
//	fireRate = 0.0f;
	
	// Use this for initialization
	//void Start () {
	//}
	
	
	
	// Update is called once per frame
	void Update () {
		TickWeaponCoolDown();	
		Fire();
	}
}
