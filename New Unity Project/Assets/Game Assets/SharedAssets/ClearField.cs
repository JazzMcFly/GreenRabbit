using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


public class ClearField : Explosion {
	

	// Use this for initialization
	void Start () {
		damage = 0.0f;
		SetupPhysics();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected override bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		if(fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<ProjectileBasic>() != null) {
			GameObject.Destroy(fixtureB.Body.UserFSBodyComponent.gameObject);	
		}
		return false;	
	}
}
