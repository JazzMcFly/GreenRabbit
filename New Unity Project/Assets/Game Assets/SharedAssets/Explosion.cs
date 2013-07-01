using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


public class Explosion : MonoBehaviour {
	
	public float minRadius = 1.0f;
	public float maxRadius = 5.0f;
	public float expansionTime = 0.2f;
	public float lifeSpan = 1.0f;
	public float damage = 100.0f;
	public float maxImpactOnPlayer = 0.0f;
	public float minImpactOnPlayer = 0.0f;
	
	protected Body body;
	protected Shape shape;
	protected float expansionRate;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(shape.Radius < maxRadius){		
			shape.Radius += expansionRate*Time.fixedDeltaTime;
		} else {
			shape.Radius = maxRadius;	
		}		
		body.DestroyFixture(body.FixtureList[0]);
		body.CreateFixture(shape);
		gameObject.transform.localScale = new Vector3(2*shape.Radius, 2*shape.Radius, 1.0f);
		//print (shape.Radius);
		SetupPhysics();
		
		lifeSpan -= Time.fixedDeltaTime;
		if(lifeSpan <= 0.0f) {
			GameObject.Destroy(gameObject);	
		}
	}
	
	public float GetDamage() {
		return damage;	
	}
	
	protected void SetupPhysics() {	
		if(body == null) {			
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			shape = GetComponent<FSShapeComponent>().GetShape();
			shape.Radius = minRadius;
			
			body.DestroyFixture(body.FixtureList[0]);
			body.CreateFixture(shape);
		
			expansionRate = (maxRadius - minRadius) / expansionTime; 
			gameObject.transform.localScale = new Vector3(2*minRadius, 2*minRadius, 1.0f);
		}
		body.IsSensor = true;
		body.GravityScale = 0.0f; //Explosions aren't affected by gravity
		body.OnCollision += OnCollisionEvent;
	}

	protected virtual bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		
		if(fixtureB.Body.UserFSBodyComponent.gameObject.tag == "Player") {
			//print ("IMPACT!!");
			FVector2 playerPos = fixtureB.Body.Position;
			FVector2 explosionPos = body.Position;
			
			FVector2 result = playerPos - explosionPos;
			
			float distance = result.Length();
			result.Normalize();
			
			float playerImpact = result.Y * distance * (maxImpactOnPlayer - minImpactOnPlayer) / maxRadius;
			fixtureB.Body.LinearVelocity = new FVector2(fixtureB.Body.LinearVelocity.X, minImpactOnPlayer + playerImpact);
		} else {
			Health objectHealth = fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<Health>();
			if(objectHealth != null) {
				objectHealth.Damage(GetDamage());	
			}
		}
		return false;
	}
}