using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class ProjectileBasic : MonoBehaviour {
	
	public float damage = 1.0f;
	public float projectileSpeed = 22.0f;
	public float minVelocity = 0.0f;
	private float minSquaredVelocity;
	public float lifespan = 1.5f;
	public bool fixedRotation = true;
	public bool penetrating = false;
	public bool hasGravity = false;
	public int bounceCount = 0;
	public bool destroysBullets = false;
	
	public GameObject explosion;
	
	[HideInInspector]
	public bool disarmed = false;
	
	private Body body;
	private FVector2 normalizedVelocity;
	
	// Use this for initialization
	void Start () {
		minSquaredVelocity = Mathf.Pow(minVelocity, 2.0f);
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		lifespan -= Time.fixedDeltaTime;
		
		if(lifespan <= 0.0f) {
			GameObject.Destroy(gameObject);	
		}
		
		if(body.LinearVelocity.LengthSquared() < minSquaredVelocity) {
			FVector2 temp = body.LinearVelocity;
			temp.Normalize();
			body.LinearVelocity = new FVector2(temp.X*minVelocity, temp.Y*minVelocity);
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
	
	public void Disarm() {
		disarmed = true;
		body.FixedRotation = false;
	}
	
	public float GetDamage() {
		if(!disarmed) {
			return damage;
		} else {
			return 0.0f;	
		}
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		
		//if(!disarmed) {
			//print ("NOT DISARMED");
			if(fixtureB.Body.UserFSBodyComponent.gameObject.tag == "Explosion") {
				return false;
			}
			Health objectHealth = fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<Health>();
			if(objectHealth != null) {
				objectHealth.Damage(GetDamage());
				if(!penetrating) {
					if(explosion != null) {
						Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
					}
					GameObject.Destroy(gameObject);
				} else {
					return false;
				}
			}
			if(fixtureB.Body.IsStatic) {
				if(bounceCount > 0) {
					bounceCount--;
				} else {
					if(explosion != null) {
						Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
					}
					GameObject.Destroy(gameObject);
				}
			} else if(destroysBullets && fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<ProjectileBasic>() != null) {
				//Should probably disarm rather than destroy the bullets.
				GameObject.Destroy(fixtureB.Body.UserFSBodyComponent.gameObject);
			}
		//}
		return true;
	}
	
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			body.FixedRotation = true;
			//body.IsSensor = !isBouncy;
			body.IsBullet = true;

			if(!hasGravity)
				body.GravityScale = 0.0f;
			
			body.OnCollision += OnCollisionEvent;
			Vector3 objectRotation = gameObject.transform.eulerAngles;
			//body.Rotation = objectRotation.z + (0.5f)*Mathf.PI;
			float a = objectRotation.z * Mathf.PI / 180.0f + Mathf.PI / 2.0f;
			normalizedVelocity = new FVector2(Mathf.Cos(a), Mathf.Sin(a));
			
			SetSpeed(projectileSpeed);
		}
	}
	
}
