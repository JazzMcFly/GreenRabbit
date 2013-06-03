using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class ProjectileBasic : MonoBehaviour {
	
	public float damage = 1.0f;
	public float projectileSpeed = 10.0f;
	public float lifespan = 3.0f;
	public bool fixedRotation = true;
	public bool penetrating = false;
	public bool hasGravity = false;
	public int team = 0;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		lifespan -= Time.fixedDeltaTime;
		
		if(lifespan <= 0.0f) {
			GameObject.Destroy(gameObject);	
		}
	}
	
	//MUST BE NORMALIZED VECTOR
	public void SetDirection(FVector2 newDir) {
		SetupPhysics();
		body.LinearVelocity = new FVector2(projectileSpeed*newDir.X, projectileSpeed*newDir.Y);	
		
		float angleInRads = Mathf.Acos(newDir.X);
		if(newDir.Y < 0.0f)
			angleInRads = 2*Mathf.PI - angleInRads;
		body.Rotation = angleInRads;
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
			return false;
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			body.FixedRotation = true;
			//body.LinearVelocity = new FVector2(0.0f, projectileSpeed);
			body.OnCollision += OnCollisionEvent;
		}
	}
	
}
