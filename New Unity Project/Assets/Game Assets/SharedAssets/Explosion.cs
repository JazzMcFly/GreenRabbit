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
	public float maxImpactOnPlayer = 20.0f;
	public float minImpactOnPlayer = 2.0f;
	
	private Body body;
	private Shape shape;
	private float expansionRate;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//shape.Radius += expansionRate*Time.fixedDeltaTime;
		lifeSpan -= Time.fixedDeltaTime;
		
		if(lifeSpan <= 0.0f) {
			GameObject.Destroy(gameObject);	
		}
	}
	
	public float GetDamage() {
		return damage;	
	}
	
	private void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.IsSensor = true;
		body.GravityScale = 0.0f; //Explosions aren't affected by gravity
		
		
		//shape = GetComponent<FSShapeComponent>().GetShape();
		//shape.Radius = minRadius;
		
		expansionRate = (maxRadius - minRadius) / expansionTime; 
		gameObject.transform.localScale = new Vector3(maxRadius, maxRadius, 1.0f);
	}

	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		return false;
	}
}