using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;

public class ArmorShard : MonoBehaviour {

	public float height;
	public float width;
	
	private Body body;
	
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
			body.OnCollision += OnCollisionEvent;
		}
	}
	
	public void Resize(float width, float height) {
		gameObject.transform.localScale = new Vector3(width, height, 1.0f);
		Shape temp = GetComponent<FSShapeComponent>().GetShape();
		PolygonShape polyTemp = (PolygonShape) temp;
		polyTemp.SetAsBox(width/2.0f, height/2.0f);
		body.DestroyFixture(body.FixtureList[0]);
		body.CreateFixture(polyTemp);
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		return true;	
	}
}
