using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


//Armor shell for enemy that blocks/disarms player shots
//Much like a mobile piece of scenery that the enemy is carrying
public class ArmorFan : MonoBehaviour {
	
	public float radius = 1.0f;
	public float maxAngle = 180.0f;
	public int shardCount = 10;	
	
	public float rotationRate = 0.0f;
	
	private Body body;
	private float rectWidth;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;	
			PolygonShape[] shapeList = TriangleIsosceles.CreateFanSet(radius, maxAngle, shardCount, 1);	
			for(int i = 0; i < shardCount; i++) {			
				body.CreateFixture(shapeList[i]);				
			}
			
			body.OnCollision += OnCollisionEvent;
			
			body.AngularVelocity = rotationRate*Mathf.PI/180.0f;
		}
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		GameObject fixObject = fixtureB.Body.UserFSBodyComponent.gameObject;
		if(fixObject.tag == "Bullet") {
			ProjectileBasic projComponent = fixObject.GetComponent<ProjectileBasic>();
			if(projComponent != null) {
				projComponent.Disarm();
			}
		}
		return true;
	}
}
