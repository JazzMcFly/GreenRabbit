using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;

public class AmmoPowerUp : MonoBehaviour {

	public int ammoValue;
	public WeaponPlayer.WeaponInput weaponType;
	public float size;
	public float mass;
	public float lifeSpan = 10.0f;
	
	protected Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
		StartCoroutine(SetupLifeSpan());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected IEnumerator SetupLifeSpan() {
		yield return new WaitForSeconds(lifeSpan);	
		Destroy(gameObject);
	}
	
	protected void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		
		Vertices vertices = PolygonTools.CreateRectangle(size/2.0f, size/2.0f);
		
		body.CreateFixture(new PolygonShape(vertices, mass));
		body.OnCollision += OnCollisionEvent;
		body.Restitution = 0.4f;
		body.AngularVelocity = Random.value * 50 - 25;
	}
	
	protected bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		PlayerStats pStats = fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<PlayerStats>();
		if(pStats != null) {
			pStats.AddAmmoCount(ammoValue, weaponType);
			GameObject.Destroy(gameObject);
			return false;
		} else if( fixtureB.Body.IsStatic == true) {
			return true;	
		} else {
			return false;	
		}
	}
}
