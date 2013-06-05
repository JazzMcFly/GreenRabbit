using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class ProjectileBasic : MonoBehaviour {
	
	public float damage = 1.0f;
	public float projectileSpeed = 22.0f;
	public float lifespan = 1.5f;
	public bool fixedRotation = true;
	public bool penetrating = false;
	public bool hasGravity = false;
	public bool isBouncy = false;
	
	public GameObject explosion;
	
	private Body body;
	private FVector2 normalizedVelocity;
	
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
		
		normalizedVelocity = new FVector2(newDir.X, newDir.Y);
		body.LinearVelocity = new FVector2(projectileSpeed*newDir.X, projectileSpeed*newDir.Y);	
		
		float angleInRads = Mathf.Acos(newDir.X);
		if(newDir.Y < 0.0f)
			angleInRads = 2*Mathf.PI - angleInRads;
		body.Rotation = angleInRads + (0.5f)*Mathf.PI;
	}
	
	public void SetSpeed(float newSpeed) {
		
		projectileSpeed = newSpeed;
		
		if(normalizedVelocity == null || normalizedVelocity.Length() == 0.0f) {
			//if no direction, assume straight upwards
			normalizedVelocity = new FVector2(0.0f, 1.0f);
		}
		body.LinearVelocity = new FVector2(projectileSpeed*normalizedVelocity.X, projectileSpeed*normalizedVelocity.Y);	
	}
	
	public float GetDamage() {
		return damage;	
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		
			if(fixtureB.Body.UserFSBodyComponent.gameObject.tag == "Explosion") {
				return false;
			}
		
			if(explosion != null) {
				Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
			}
			GameObject.Destroy(gameObject);
		
			return true;
	}
	
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			body.FixedRotation = true;
			body.IsSensor = !isBouncy;

			if(!hasGravity)
				body.GravityScale = 0.0f;
			
			body.OnCollision += OnCollisionEvent;
		}
	}
	
}
