using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


public class PrimaryPowerUp : MonoBehaviour {
	
	
	public int PowerValue;
	public float size;
	public float mass;
	
	protected Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		
		PolygonShape shape = TriangleEqualateral.CreateEqlTriangle(size, mass);
		body.CreateFixture(shape);
		body.OnCollision += OnCollisionEvent;
	}
	
	protected bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		PlayerStats pStats = fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<PlayerStats>();
		if(pStats != null) {
			pStats.IncreasePower(PowerValue);
			GameObject.Destroy(gameObject);
			return false;
		} else if( fixtureB.Body.IsStatic == true) {
			return true;	
		} else {
			return false;	
		}
	}
}
