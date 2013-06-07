using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


//Armor shell for enemy that blocks/disarms player shots
//Much like a mobile piece of scenery that the enemy is carrying
public class Armor : MonoBehaviour {
	
	private Body body;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;	
		}
	}
}
