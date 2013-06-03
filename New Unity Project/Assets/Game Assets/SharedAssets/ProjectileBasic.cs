using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class ProjectileBasic : MonoBehaviour {
	
	public float damage = 1.0f;
	public float projectileSpeed = 1.0f;
	public float lifespan = 3.0f;
	public bool fixedRotation = true;
	public bool penetrating = false;
	public bool hasGravity = false;
	public int team = 0;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixedRotation = true;
		body.LinearVelocity = new FVector2(0.0f, projectileSpeed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		lifespan -= Time.fixedDeltaTime;
		
		if(lifespan <= 0.0f) {
			GameObject.Destroy(gameObject);	
		}
	}
}
